using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Core data classes for Eclipse Reborn - Final Clean Version
/// </summary>

[Serializable]
public class HeroData
{
    public string heroId = "auron";
    public string heroName = "Auron";
    public HeroClass heroClass = HeroClass.Warrior;
    public string description = "Golden Blade Guardian";
    
    public int baseHealth = 100;
    public int baseAttack = 25;
    public int baseDefense = 10;
    public float baseAttackSpeed = 1.0f;
    public float baseMoveSpeed = 3.0f;
    
    public int currentLevel = 1;
    public int currentXP = 0;
    public int maxXP = 100;
    
    public float critChance = 0.05f;
    public float critMultiplier = 2.0f;
    public int currentHealth;
    public int maxHealth;
    
    public List<HeroSkill> skills = new List<HeroSkill>();
    
    public void Initialize()
    {
        maxHealth = baseHealth + (currentLevel - 1) * 10;
        currentHealth = maxHealth;
    }
}

public enum HeroClass
{
    Warrior,
    Mage,
    Healer,
    Rogue,
    Tank
}

[Serializable]
public class HeroSkill
{
    public string skillId;
    public string skillName;
    public SkillType skillType;
    public int manaCost;
    public float cooldown;
    public float damage;
    public string description;
    public bool isUnlocked;
}

public enum SkillType
{
    Attack,
    Heal,
    Buff,
    Debuff,
    Ultimate
}

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

public enum StatusEffectType
{
    Poison,
    Burn,
    Freeze,
    Stun,
    Heal,
    Buff,
    Debuff
}

public class SimpleEnemyAI : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP = 50;
    public int attackDamage = 15;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    
    private Transform target;
    private float lastAttackTime;
    
    void Update()
    {
        if (currentHP <= 0) return;
        
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
        
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            
            if (distance > attackRange)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            else if (Time.time - lastAttackTime > attackCooldown)
            {
                AttackTarget();
                lastAttackTime = Time.time;
            }
        }
    }
    
    void AttackTarget()
    {
        if (target != null)
        {
            Debug.Log($"Enemy attacks for {attackDamage} damage!");
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Enemy defeated!");
        gameObject.SetActive(false);
    }
}

public class ObjectPool<T> where T : UnityEngine.Object
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    
    public ObjectPool(T prefab, int initialSize)
    {
        this.prefab = prefab;
        
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
            return UnityEngine.Object.Instantiate(prefab);
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

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float lifetime = 1f;
    
    private System.Action onComplete;
    
    public void Initialize(float damage, bool isCritical, Color color, System.Action onCompleteCallback)
    {
        if (textMesh != null)
        {
            textMesh.text = Mathf.RoundToInt(damage).ToString();
            textMesh.color = color;
            textMesh.fontSize = isCritical ? 8 : 6;
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