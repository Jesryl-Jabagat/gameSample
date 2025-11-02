using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Core data classes for Eclipse Reborn game systems.
/// These classes define the structure for heroes, enemies, buildings, items, and save data.
/// All classes are Serializable to support Unity's JsonUtility and Inspector display.
/// </summary>

// ==================== HERO DATA ====================

/// <summary>
/// HeroData holds all stats and progression data for a playable hero.
/// Loaded from hero_templates.json at runtime.
/// </summary>
[Serializable]
public class HeroData
{
    public string heroId;
    public string heroName;
    public string title;
    public string role;
    
    // Current stats
    public float hp;
    public float maxHp;
    public float atk;
    public float def;
    public float spd;
    public float crit;
    
    // Level and XP
    public int level;
    public int currentXP;
    
    // Skill data
    public float skill1Multiplier;
    public float skill1Range;
    public float skill1CastTime;
    public float skill1CooldownMax;
    public float skill1Cooldown;
    
    public float skill2Multiplier;
    public float skill2Range;
    public float skill2CastTime;
    public float skill2CooldownMax;
    public float skill2Cooldown;
    
    // Stat growth per level (loaded from template)
    public float hpGrowth;
    public float atkGrowth;
    public float defGrowth;
    public float spdGrowth;
    public float critGrowth;
}

// ==================== BUILDING DATA ====================

/// <summary>
/// Building represents a Sanctuary structure with upgrade and production capabilities.
/// </summary>
[Serializable]
public class Building
{
    public string buildingId;
    public string buildingName;
    public int currentLevel;
    public int maxLevel;
    
    public int baseCost;
    public float baseTime;
    public float costGrowthFactor;
    public float timeGrowthFactor;
    
    public float productionPerHour;
    public float productionGrowthFactor;
    public bool producesPremiumCurrency;
    
    public bool isUpgrading;
    public float upgradeTimeRemaining;
    public DateTime upgradeStartTime;
    
    public int GetUpgradeCost()
    {
        return Mathf.RoundToInt(baseCost * Mathf.Pow(costGrowthFactor, currentLevel));
    }
    
    public float GetUpgradeTime()
    {
        return baseTime * Mathf.Pow(timeGrowthFactor, currentLevel);
    }
    
    public int GetInstantFinishCost()
    {
        if (!isUpgrading) return 0;
        float timeRemainingHours = upgradeTimeRemaining / 3600f;
        return Mathf.Clamp(Mathf.RoundToInt(timeRemainingHours * 50f), 50, 300);
    }
}

// ==================== GAME CONFIG ====================

/// <summary>
/// GameConfig structure matching game_config.json.
/// Contains XP tables, combat formulas, building data, and currency definitions.
/// </summary>
[Serializable]
public class GameConfig
{
    public XPSystemConfig xp_system;
    public CombatFormulas combat_formulas;
    public BuildingsConfig buildings;
    public CurrenciesConfig currencies;
    public MonetizationConfig monetization;
}

[Serializable]
public class XPSystemConfig
{
    public string formula;
    public int max_level;
    public int stage_xp_reward_base;
    public float boss_xp_multiplier;
    
    // Note: Dictionary not directly supported by JsonUtility
    // Use Newtonsoft.Json or parse manually
    public List<XPLevelEntry> level_table_list;
}

[Serializable]
public class XPLevelEntry
{
    public int level;
    public int xp_required;
    public int cumulative_xp;
}

[Serializable]
public class CombatFormulas
{
    public string damage_formula;
    public float base_crit_damage;
    public float legendary_crit_damage;
    public string dodge_formula;
    public float max_dodge_chance;
}

[Serializable]
public class BuildingsConfig
{
    public BuildingTemplate barracks;
    public BuildingTemplate workshop;
    public BuildingTemplate shard_reactor;
    public BuildingTemplate treasury;
    public BuildingTemplate training_grounds;
}

[Serializable]
public class BuildingTemplate
{
    public int base_cost;
    public int base_time_seconds;
    public float cost_growth_factor;
    public float time_growth_factor;
    public int max_level;
    public float production_per_hour;
    public float production_growth_factor;
}

[Serializable]
public class CurrenciesConfig
{
    public CurrencyDefinition gold;
    public CurrencyDefinition solar_shards;
}

[Serializable]
public class CurrencyDefinition
{
    public string name;
    public string description;
    public int max_cap;
}

[Serializable]
public class MonetizationConfig
{
    public RewardedAdsConfig rewarded_ads;
}

[Serializable]
public class RewardedAdsConfig
{
    public int daily_limit_free;
    public int daily_limit_vip;
}

// ==================== SAVE DATA ====================

/// <summary>
/// SaveData structure for JSON serialization.
/// Contains all player progress data.
/// </summary>
[Serializable]
public class SaveData
{
    public string version;
    public string lastSavedTimestamp;
    public string playerId;
    
    public int playerLevel;
    public int currentXP;
    
    public int gold;
    public int solarShards;
    
    public List<HeroSaveData> heroes;
    public List<BuildingSaveData> buildings;
    public List<ItemSaveData> inventory;
    
    public List<string> purchasedProducts;
    
    public bool isVIP;
    public string vipExpiryDate;
    
    public int adsWatchedToday;
    public string lastAdResetDate;
}

[Serializable]
public class HeroSaveData
{
    public string heroId;
    public int level;
    public int currentXP;
    public bool isUnlocked;
    public float currentHP;
    public List<EquipmentSlot> equipment;
}

[Serializable]
public class BuildingSaveData
{
    public string buildingId;
    public int currentLevel;
    public bool isUpgrading;
    public string upgradeStartTime;
    public float upgradeTimeRemaining;
}

[Serializable]
public class ItemSaveData
{
    public string itemId;
    public int quantity;
    public int enhancementLevel;
}

[Serializable]
public class EquipmentSlot
{
    public string slotType;
    public string itemId;
}

// ==================== STATUS EFFECTS ====================

/// <summary>
/// Status effect types for combat system.
/// </summary>
public enum StatusEffectType
{
    Burn,
    Stun,
    Shield,
    AttackBuff,
    DefenseBuff
}

/// <summary>
/// StatusEffect data structure for tracking active effects.
/// </summary>
[Serializable]
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

// ==================== ENEMY DATA ====================

/// <summary>
/// Enemy component for AI-controlled hostiles.
/// Minimal implementation - expand with EnemyAI script.
/// </summary>
public class Enemy : MonoBehaviour
{
    public string enemyId;
    public string enemyName;
    public int level;
    
    public float maxHP;
    public float currentHP;
    public float atk;
    public float def;
    public float spd;
    
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    
    private Transform target;
    private float lastAttackTime;
    
    void Update()
    {
        if (currentHP <= 0) return;
        
        // Simple chase AI
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                float dist = Vector3.Distance(transform.position, player.transform.position);
                if (dist < detectionRange)
                {
                    target = player.transform;
                }
            }
        }
        
        if (target != null)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            
            if (dist > attackRange)
            {
                // Move towards player
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * spd * Time.deltaTime;
            }
            else
            {
                // Attack if cooldown ready
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
        }
    }
    
    void Attack()
    {
        // Deal damage to player
        CombatTarget playerTarget = target.GetComponent<CombatTarget>();
        if (playerTarget != null)
        {
            float damage = atk * (100f / (100f + playerTarget.GetDefense()));
            playerTarget.TakeDamage(damage);
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        // Drop loot, grant XP
        XPSystem xpSystem = FindObjectOfType<XPSystem>();
        if (xpSystem != null)
        {
            xpSystem.AddXP(50 * level);
        }
        
        Destroy(gameObject, 0.5f);
    }
}

// ==================== UTILITY CLASSES ====================

/// <summary>
/// Simple object pool for mobile optimization.
/// Prevents GC spikes from Instantiate/Destroy.
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
            T obj = UnityEngine.Object.Instantiate(prefab);
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
            T obj = UnityEngine.Object.Instantiate(prefab);
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
/// Damage number floating text component.
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

/// <summary>
/// Joystick component for virtual touch controls.
/// Simple radial joystick implementation.
/// </summary>
public class Joystick : MonoBehaviour
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform knob;
    [SerializeField] private float handleRange = 1f;
    
    private Vector2 input = Vector2.zero;
    
    public float Horizontal => input.x;
    public float Vertical => input.y;
    
    void Update()
    {
        // Touch/mouse input handling
        if (Input.GetMouseButton(0))
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background, 
                Input.mousePosition, 
                null, 
                out localPoint
            );
            
            localPoint /= background.sizeDelta / 2f;
            input = Vector2.ClampMagnitude(localPoint, 1f);
            
            knob.anchoredPosition = input * background.sizeDelta * handleRange / 2f;
        }
        else
        {
            input = Vector2.zero;
            knob.anchoredPosition = Vector2.zero;
        }
    }
}
