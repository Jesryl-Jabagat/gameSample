using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Minimal CombatSystem for Eclipse Reborn - Clean Version
/// </summary>
public class CombatSystem : MonoBehaviour
{
    [Header("Combat Stats")]
    [SerializeField] private float baseATK = 45f;
    [SerializeField] private float baseDEF = 20f;
    [SerializeField] private float baseCRIT = 0.05f;

    [Header("Combat Constants")]
    [SerializeField] private float critDamageMultiplier = 2.0f;

    [Header("References")]
    [SerializeField] private Transform damageNumberSpawnPoint;
    [SerializeField] private GameObject damageNumberPrefab;

    private HeroData heroData;
    private List<StatusEffect> activeStatusEffects = new List<StatusEffect>();
    private CombatTarget selectedTarget;

    private void Awake()
    {
        // Initialize combat system
        Debug.Log("CombatSystem initialized");
    }

    /// <summary>
    /// Calculate damage with critical hit chance.
    /// </summary>
    public float CalculateDamage(float attackPower, float defense)
    {
        float baseDamage = Mathf.Max(1f, attackPower - defense * 0.5f);
        
        // Check for critical hit
        bool isCritical = Random.Range(0f, 1f) < baseCRIT;
        if (isCritical)
        {
            baseDamage *= critDamageMultiplier;
            Debug.Log("Critical hit!");
        }

        return baseDamage;
    }

    /// <summary>
    /// Deal damage to target.
    /// </summary>
    public void DealDamage(GameObject target, float damage)
    {
        CombatTarget combatTarget = target.GetComponent<CombatTarget>();
        if (combatTarget != null)
        {
            combatTarget.TakeDamage((int)damage);
            Debug.Log($"Dealt {damage} damage to {target.name}");
        }
    }

    /// <summary>
    /// Apply status effect to target.
    /// </summary>
    public void ApplyStatusEffect(GameObject target, StatusEffectType effectType, float duration)
    {
        StatusEffect effect = new StatusEffect
        {
            effectType = effectType,
            target = target,
            duration = duration,
            remainingTime = duration
        };

        activeStatusEffects.Add(effect);
        Debug.Log($"Applied {effectType} to {target.name} for {duration} seconds");
    }

    /// <summary>
    /// Update active status effects.
    /// </summary>
    private void Update()
    {
        for (int i = activeStatusEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = activeStatusEffects[i];
            effect.remainingTime -= Time.deltaTime;

            if (effect.remainingTime <= 0)
            {
                RemoveStatusEffect(effect);
                activeStatusEffects.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Remove status effect.
    /// </summary>
    private void RemoveStatusEffect(StatusEffect effect)
    {
        Debug.Log($"Removed {effect.effectType} from {effect.target.name}");
    }

    /// <summary>
    /// Set hero data for combat calculations.
    /// </summary>
    public void SetHeroData(HeroData data)
    {
        heroData = data;
        baseATK = data.baseAttack;
        baseCRIT = data.critChance;
        Debug.Log($"Hero data set: {data.heroName}");
    }

    /// <summary>
    /// Get current attack stat.
    /// </summary>
    public float GetAttack()
    {
        return heroData != null ? heroData.baseAttack : baseATK;
    }

    /// <summary>
    /// Get current defense stat.
    /// </summary>
    public float GetDefense()
    {
        return heroData != null ? heroData.baseDefense : baseDEF;
    }

    /// <summary>
    /// Select target for combat.
    /// </summary>
    public void SelectTarget(CombatTarget target)
    {
        if (selectedTarget != null)
        {
            selectedTarget.SetTargeted(false);
        }
        
        selectedTarget = target;
        if (selectedTarget != null)
        {
            selectedTarget.SetTargeted(true);
            Debug.Log($"Selected target: {selectedTarget.name}");
        }
    }

    /// <summary>
    /// Handle enemy defeated event.
    /// </summary>
    public void OnEnemyDefeated(CombatTarget enemy)
    {
        Debug.Log($"Enemy defeated: {enemy.name}");
        
        // Award XP
        XPSystem xpSystem = FindFirstObjectByType<XPSystem>();
        if (xpSystem != null)
        {
            xpSystem.AddXP(50); // Base XP reward
        }
    }

    /// <summary>
    /// Cleanse debuffs from target.
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

    /// <summary>
    /// Check if effect type is a debuff.
    /// </summary>
    private bool IsDebuff(StatusEffectType effectType)
    {
        return effectType == StatusEffectType.Poison || 
               effectType == StatusEffectType.Burn || 
               effectType == StatusEffectType.Stun;
    }
}