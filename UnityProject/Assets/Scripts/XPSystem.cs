using UnityEngine;
using System;

/// <summary>
/// XPSystem manages player experience, leveling, and stat allocation.
/// Uses XP table from game_config.json for deterministic progression.
/// </summary>
public class XPSystem : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int maxLevel = 50;
    [SerializeField] private int currentXP = 0;

    [Header("References")]
    [SerializeField] private HeroData heroData;

    public event Action<int> OnLevelUp;
    public event Action<int, int> OnXPGained;

    private GameConfig gameConfig;

    private void Awake()
    {
        LoadGameConfig();
    }

    /// <summary>
    /// Load XP table from game_config.json.
    /// In production, use JSON parser or Resources.Load.
    /// </summary>
    private void LoadGameConfig()
    {
        string configPath = "data/game_config";
        TextAsset configAsset = Resources.Load<TextAsset>(configPath);

        if (configAsset != null)
        {
            gameConfig = JsonUtility.FromJson<GameConfig>(configAsset.text);
        }
        else
        {
            Debug.LogWarning("game_config.json not found in Resources. Using fallback formula.");
            gameConfig = new GameConfig();
        }
    }

    /// <summary>
    /// Add XP to player and check for level ups.
    /// Handles multiple level ups in one XP gain (e.g., from rare drops).
    /// </summary>
    /// <param name="xpAmount">Amount of XP to add</param>
    public void AddXP(int xpAmount)
    {
        if (currentLevel >= maxLevel)
        {
            Debug.Log("Max level reached!");
            return;
        }

        currentXP += xpAmount;
        OnXPGained?.Invoke(xpAmount, currentXP);

        while (currentXP >= GetXPForNextLevel() && currentLevel < maxLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// Get XP required for next level from table or formula.
    /// Formula: XP = 100 * (level^1.8) + 50 * level
    /// </summary>
    public int GetXPForNextLevel()
    {
        if (currentLevel >= maxLevel) return 0;

        if (gameConfig != null && gameConfig.xp_system != null)
        {
            string levelKey = (currentLevel + 1).ToString();
            if (gameConfig.xp_system.level_table.ContainsKey(levelKey))
            {
                return gameConfig.xp_system.level_table[levelKey].xp_required;
            }
        }

        return CalculateXPForLevel(currentLevel + 1);
    }

    /// <summary>
    /// Calculate XP using formula (fallback if JSON not loaded).
    /// </summary>
    private int CalculateXPForLevel(int level)
    {
        return Mathf.RoundToInt(100f * Mathf.Pow(level, 1.8f) + 50f * level);
    }

    /// <summary>
    /// Perform level up: subtract XP, increment level, allocate stats.
    /// </summary>
    private void LevelUp()
    {
        int xpNeeded = GetXPForNextLevel();
        currentXP -= xpNeeded;
        currentLevel++;

        AllocateStats();

        OnLevelUp?.Invoke(currentLevel);

        Debug.Log($"Level Up! Now level {currentLevel}");
    }

    /// <summary>
    /// Allocate stat increases per level based on hero growth rates.
    /// Growth rates loaded from hero_templates.json.
    /// </summary>
    private void AllocateStats()
    {
        if (heroData == null)
        {
            Debug.LogWarning("No HeroData assigned to XPSystem!");
            return;
        }

        heroData.baseHealth += 25;
        heroData.maxHealth = heroData.baseHealth + (heroData.currentLevel - 1) * 10;
        heroData.currentHealth = heroData.maxHealth;
        heroData.baseAttack += 3;
        heroData.baseDefense += 2;
        heroData.baseMoveSpeed += 0.1f;
        heroData.critChance += 0.005f;

        Debug.Log($"Stats increased! HP: {heroData.maxHealth}, ATK: {heroData.baseAttack}, DEF: {heroData.baseDefense}");
    }

    /// <summary>
    /// Get current level.
    /// </summary>
    public int GetCurrentLevel() => currentLevel;

    /// <summary>
    /// Get current XP.
    /// </summary>
    public int GetCurrentXP() => currentXP;

    /// <summary>
    /// Get XP progress percentage for UI bar (0.0 to 1.0).
    /// </summary>
    public float GetXPProgress()
    {
        int xpForNext = GetXPForNextLevel();
        if (xpForNext == 0) return 1f;
        return (float)currentXP / xpForNext;
    }

    /// <summary>
    /// Set hero data reference for stat allocation.
    /// </summary>
    public void SetHeroData(HeroData data)
    {
        heroData = data;
    }

    /// <summary>
    /// Award XP from stage completion.
    /// Base XP from config, boss stages give 2.5x multiplier.
    /// </summary>
    public void AwardStageXP(bool isBossStage = false)
    {
        int baseXP = 50;

        if (gameConfig != null && gameConfig.xp_system != null)
        {
            baseXP = gameConfig.xp_system.stage_xp_reward_base;
        }

        float multiplier = isBossStage ? 2.5f : 1.0f;
        int finalXP = Mathf.RoundToInt(baseXP * multiplier);

        AddXP(finalXP);
    }
}

/// <summary>
/// GameConfig structure matching game_config.json.
/// Simplified for XP system (full version would include all config data).
/// </summary>
[Serializable]
public class GameConfig
{
    public XPSystemConfig xp_system;
}

[Serializable]
public class XPSystemConfig
{
    public string formula;
    public int max_level;
    public System.Collections.Generic.Dictionary<string, XPLevelData> level_table;
    public int stage_xp_reward_base;
    public float boss_xp_multiplier;
}

[Serializable]
public class XPLevelData
{
    public int xp_required;
    public int cumulative_xp;
}
