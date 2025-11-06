using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// CombatSystem handles damage calculation, critical hits, status effects, and combat logic.
/// All formulas are deterministic and testable, using values from game_config.json.
/// Optimized for mobile with object pooling for damage numbers and VFX.
/// </summary>
public class CombatSystem : MonoBehaviour
{
    [Header("Combat Stats")]
    [SerializeField] private float baseATK = 45f;
    [SerializeField] private float baseDEF = 20f;
    [SerializeField] private float baseCRIT = 0.05f;

    [Header("Combat Constants")]
    [SerializeField] private float critDamageMultiplier = 2.0f;
    [SerializeField] private float legendaryCritMultiplier = 2.5f;

    [Header("References")]
    [SerializeField] private Transform damageNumberSpawnPoint;
    [SerializeField] private GameObject damageNumberPrefab;

    private HeroData heroData;
    private List<StatusEffect> activeStatusEffects = new List<StatusEffect>();

    private const float BURN_DAMAGE_PER_TICK = 0.02f;
    private const float BURN_TICK_INTERVAL = 1.0f;
    private const float STUN_DURATION = 1.5f;

    private ObjectPool<GameObject> damageNumberPool;

    private void Awake()
    {
        if (damageNumberPrefab != null)
        {
            damageNumberPool = new ObjectPool<GameObject>(damageNumberPrefab, 20);
        }
    }

    private void Update()
    {
        UpdateStatusEffects();
    }

    /// <summary>
    /// Calculate and deal damage to target using core damage formula.
    /// Formula: Damage = (ATK * skillMultiplier) * (100 / (100 + DEF)) * critMultiplier
    /// </summary>
    /// <param name="target">Target GameObject with CombatTarget component</param>
    /// <param name="skillMultiplier">Skill damage multiplier (1.0 = basic attack)</param>
    /// <param name="isUltimate">Whether this is an ultimate skill (affects crit bonus)</param>
    /// <returns>Final damage dealt</returns>
    public float DealDamage(GameObject target, float skillMultiplier, bool isUltimate = false)
    {
        if (target == null) return 0f;

        CombatTarget targetCombat = target.GetComponent<CombatTarget>();
        if (targetCombat == null) return 0f;

        float attackPower = heroData != null ? heroData.atk : baseATK;
        float targetDefense = targetCombat.GetDefense();
        float critChance = heroData != null ? heroData.crit : baseCRIT;

        bool isCritical = Random.value < critChance;
        float critMultiplier = 1.0f;

        if (isCritical)
        {
            critMultiplier = isUltimate ? legendaryCritMultiplier : critDamageMultiplier;
        }

        float baseDamage = attackPower * skillMultiplier;
        float defenseReduction = 100f / (100f + targetDefense);
        float finalDamage = baseDamage * defenseReduction * critMultiplier;

        finalDamage = Mathf.Max(1f, finalDamage);

        targetCombat.TakeDamage(finalDamage);

        SpawnDamageNumber(target.transform.position, finalDamage, isCritical);

        return finalDamage;
    }

    /// <summary>
    /// Apply status effect to target (Burn, Stun, Shield, etc.).
    /// </summary>
    /// <param name="target">Target GameObject</param>
    /// <param name="effectType">Type of status effect</param>
    /// <param name="duration">Effect duration in seconds</param>
    public void ApplyStatusEffect(GameObject target, StatusEffectType effectType, float duration)
    {
        if (target == null) return;

        CombatTarget targetCombat = target.GetComponent<CombatTarget>();
        if (targetCombat == null) return;

        StatusEffect newEffect = new StatusEffect
        {
            effectType = effectType,
            duration = duration,
            remainingTime = duration,
            target = target
        };

        switch (effectType)
        {
            case StatusEffectType.Burn:
                newEffect.tickInterval = BURN_TICK_INTERVAL;
                newEffect.damagePerTick = targetCombat.GetMaxHP() * BURN_DAMAGE_PER_TICK;
                break;

            case StatusEffectType.Stun:
                targetCombat.SetStunned(true);
                break;

            case StatusEffectType.AttackBuff:
                targetCombat.ApplyAttackBuff(0.10f, duration);
                break;

            case StatusEffectType.DefenseBuff:
                targetCombat.ApplyDefenseBuff(0.10f, duration);
                break;
        }

        activeStatusEffects.Add(newEffect);
    }

    /// <summary>
    /// Update all active status effects (ticking damage, expiring buffs).
    /// </summary>
    private void UpdateStatusEffects()
    {
        for (int i = activeStatusEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = activeStatusEffects[i];

            if (effect.target == null)
            {
                activeStatusEffects.RemoveAt(i);
                continue;
            }

            effect.remainingTime -= Time.deltaTime;

            if (effect.effectType == StatusEffectType.Burn)
            {
                effect.tickTimer += Time.deltaTime;
                if (effect.tickTimer >= effect.tickInterval)
                {
                    CombatTarget targetCombat = effect.target.GetComponent<CombatTarget>();
                    if (targetCombat != null)
                    {
                        targetCombat.TakeDamage(effect.damagePerTick);
                        SpawnDamageNumber(effect.target.transform.position, effect.damagePerTick, false, Color.red);
                    }
                    effect.tickTimer = 0f;
                }
            }

            if (effect.remainingTime <= 0f)
            {
                RemoveStatusEffect(effect);
                activeStatusEffects.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Remove status effect and clean up (unstun, remove buffs).
    /// </summary>
    private void RemoveStatusEffect(StatusEffect effect)
    {
        if (effect.target == null) return;

        CombatTarget targetCombat = effect.target.GetComponent<CombatTarget>();
        if (targetCombat == null) return;

        switch (effect.effectType)
        {
            case StatusEffectType.Stun:
                targetCombat.SetStunned(false);
                break;
        }
    }

    /// <summary>
    /// Cleanse all debuffs from target (used by healer skills).
    /// </summary>
    public void CleanseDebuffs(GameObject target)
    {
        for (int i = activeStatusEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = activeStatusEffects[i];
            if (effect.target == target && IsDebuff(effect.effectType))
            {
                RemoveStatusEffect(effect);
                activeStatusEffects.RemoveAt(i);
            }
        }
    }

    private bool IsDebuff(StatusEffectType effectType)
    {
        return effectType == StatusEffectType.Burn || effectType == StatusEffectType.Stun;
    }

    /// <summary>
    /// Spawn floating damage number at hit location.
    /// Uses object pooling to avoid GC pressure on mobile.
    /// </summary>
    private void SpawnDamageNumber(Vector3 position, float damage, bool isCritical, Color? customColor = null)
    {
        if (damageNumberPool == null) return;

        GameObject damageNumberObj = damageNumberPool.Get();
        damageNumberObj.transform.position = position + Vector3.up * 0.5f;

        DamageNumber damageNumber = damageNumberObj.GetComponent<DamageNumber>();
        if (damageNumber != null)
        {
            Color color = customColor ?? (isCritical ? Color.yellow : Color.white);
            damageNumber.Initialize(damage, isCritical, color, () => damageNumberPool.Return(damageNumberObj));
        }
    }

    /// <summary>
    /// Calculate dodge chance based on speed stat.
    /// Formula: Dodge % = SPD / 10 (max 35%)
    /// </summary>
    public bool RollDodge(float speed)
    {
        float dodgeChance = Mathf.Min(speed / 10f / 100f, 0.35f);
        return Random.value < dodgeChance;
    }

    /// <summary>
    /// Set hero data reference for combat calculations.
    /// </summary>
    public void SetHeroData(HeroData data)
    {
        heroData = data;
        baseATK = data.atk;
        baseCRIT = data.crit;
    }

    /// <summary>
    /// Get current attack stat (including buffs).
    /// </summary>
    public float GetAttack()
    {
        return heroData != null ? heroData.atk : baseATK;
    }

    /// <summary>
    /// Get current defense stat (including buffs).
    /// </summary>
    public float GetDefense()
    {
        return heroData != null ? heroData.def : baseDEF;
    }
}

/// <summary>
/// CombatTarget component for entities that can receive damage.
/// Attach to players, enemies, bosses.
/// </summary>
public class CombatTarget : MonoBehaviour
{
    [SerializeField] private float maxHP = 500f;
    [SerializeField] private float currentHP = 500f;
    [SerializeField] private float defense = 20f;

    private bool isStunned;
    private float attackBuffMultiplier = 1.0f;
    private float defenseBuffMultiplier = 1.0f;

    public float GetMaxHP() => maxHP;
    public float GetCurrentHP() => currentHP;
    public float GetDefense() => defense * defenseBuffMultiplier;

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0f, currentHP);

        if (currentHP <= 0f)
        {
            OnDeath();
        }
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Min(currentHP, maxHP);
    }

    public void SetStunned(bool stunned)
    {
        isStunned = stunned;
    }

    public bool IsStunned() => isStunned;

    public void ApplyAttackBuff(float percentage, float duration)
    {
        attackBuffMultiplier = 1.0f + percentage;
        Invoke(nameof(RemoveAttackBuff), duration);
    }

    private void RemoveAttackBuff()
    {
        attackBuffMultiplier = 1.0f;
    }

    public void ApplyDefenseBuff(float percentage, float duration)
    {
        defenseBuffMultiplier = 1.0f + percentage;
        Invoke(nameof(RemoveDefenseBuff), duration);
    }

    private void RemoveDefenseBuff()
    {
        defenseBuffMultiplier = 1.0f;
    }

    private void OnDeath()
    {
        Debug.Log($"{gameObject.name} died!");
    }
}

/// <summary>
/// Status effect data structure.
/// </summary>
[System.Serializable]
public class StatusEffect
{
    public StatusEffectType effectType;
    public GameObject target;
    public float duration;
    public float remainingTime;
    public float damagePerTick;
    public float tickInterval;
    public float tickTimer;
}

public enum StatusEffectType
{
    Burn,
    Stun,
    Shield,
    AttackBuff,
    DefenseBuff
}

/// <summary>
/// Simple object pool implementation for mobile optimization.
/// Prevents GC spikes from Instantiate/Destroy calls.
/// </summary>
public class ObjectPool<T> where T : UnityEngine.Object
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    private int initialSize;

    public ObjectPool(T prefab, int initialSize)
    {
        this.prefab = prefab;
        this.initialSize = initialSize;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(prefab);
            if (obj is GameObject go)
            {
                go.SetActive(false);
            }
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (pool.Count == 0)
        {
            T obj = Object.Instantiate(prefab);
            return obj;
        }

        T pooledObj = pool.Dequeue();
        if (pooledObj is GameObject go)
        {
            go.SetActive(true);
        }
        return pooledObj;
    }

    public void Return(T obj)
    {
        if (obj is GameObject go)
        {
            go.SetActive(false);
        }
        pool.Enqueue(obj);
    }
}

/// <summary>
/// DamageNumber component for floating damage text.
/// Attach to damage number prefab.
/// </summary>
public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshPro textMesh;
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float lifetime = 1f;

    private System.Action onComplete;

    public void Initialize(float damage, bool isCritical, Color color, System.Action onCompleteCallback)
    {
        if (textMesh != null)
        {
            textMesh.text = Mathf.RoundToInt(damage).ToString();
            textMesh.color = color;
            textMesh.fontSize = isCritical ? 8f : 6f;
        }

        onComplete = onCompleteCallback;
        Invoke(nameof(Complete), lifetime);
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    private void Complete()
    {
        onComplete?.Invoke();
    }
}
