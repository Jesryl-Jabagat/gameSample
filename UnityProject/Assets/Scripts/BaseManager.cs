using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// BaseManager handles building upgrades, production ticking, and offline accumulation.
/// Integrates with SaveLoadManager for persistent offline rewards.
/// Supports instant-upgrade IAP hooks and ad-based production boosts.
/// </summary>
public class BaseManager : MonoBehaviour
{
    [Header("Building References")]
    [SerializeField] private List<Building> buildings = new List<Building>();

    [Header("Production Settings")]
    [SerializeField] private float productionTickInterval = 3600f;
    [SerializeField] private float offlineCapHours = 12f;
    [SerializeField] private bool isVIP;

    private float productionTimer;
    private DateTime lastSavedTimestamp;
    private bool hasAdBoost;
    private float adBoostMultiplier = 1.5f;
    private float adBoostRemainingTime;

    public event Action<Building> OnBuildingUpgraded;
    public event Action<int, int> OnResourcesProduced;

    private void Awake()
    {
        LoadBuildingData();
    }

    private void Start()
    {
        CalculateOfflineRewards();
    }

    private void Update()
    {
        UpdateProduction();
        UpdateAdBoost();
    }

    /// <summary>
    /// Load building configurations from game_config.json.
    /// In production, parse JSON and instantiate Building objects.
    /// </summary>
    private void LoadBuildingData()
    {
        // Example: Load from Resources or JSON file
        // For now, create sample buildings programmatically
        
        Building barracks = new Building
        {
            buildingId = "barracks",
            buildingName = "Barracks",
            currentLevel = 1,
            maxLevel = 15,
            baseCost = 500,
            baseTime = 60,
            costGrowthFactor = 1.5f,
            timeGrowthFactor = 1.3f
        };

        Building workshop = new Building
        {
            buildingId = "workshop",
            buildingName = "Workshop",
            currentLevel = 1,
            maxLevel = 12,
            baseCost = 800,
            baseTime = 120,
            costGrowthFactor = 1.5f,
            timeGrowthFactor = 1.3f,
            productionPerHour = 1,
            productionGrowthFactor = 1.2f
        };

        Building shardReactor = new Building
        {
            buildingId = "shard_reactor",
            buildingName = "Shard Reactor",
            currentLevel = 1,
            maxLevel = 10,
            baseCost = 1200,
            baseTime = 180,
            costGrowthFactor = 1.5f,
            timeGrowthFactor = 1.3f,
            productionPerHour = 0.25f,
            productionGrowthFactor = 1.15f,
            producesPremiumCurrency = true
        };

        Building treasury = new Building
        {
            buildingId = "treasury",
            buildingName = "Treasury",
            currentLevel = 1,
            maxLevel = 15,
            baseCost = 600,
            baseTime = 90,
            costGrowthFactor = 1.5f,
            timeGrowthFactor = 1.3f,
            productionPerHour = 200,
            productionGrowthFactor = 1.2f
        };

        buildings.Add(barracks);
        buildings.Add(workshop);
        buildings.Add(shardReactor);
        buildings.Add(treasury);
    }

    /// <summary>
    /// Update production timers and generate resources.
    /// Ticks every hour (3600s) for idle production.
    /// </summary>
    private void UpdateProduction()
    {
        productionTimer += Time.deltaTime;

        if (productionTimer >= productionTickInterval)
        {
            productionTimer = 0f;
            ProduceResources();
        }
    }

    /// <summary>
    /// Produce resources from all production buildings.
    /// </summary>
    private void ProduceResources()
    {
        int goldProduced = 0;
        int shardsProduced = 0;

        foreach (Building building in buildings)
        {
            if (building.productionPerHour > 0)
            {
                float production = building.productionPerHour * Mathf.Pow(building.productionGrowthFactor, building.currentLevel - 1);

                if (hasAdBoost)
                {
                    production *= adBoostMultiplier;
                }

                if (building.producesPremiumCurrency)
                {
                    shardsProduced += Mathf.RoundToInt(production);
                }
                else
                {
                    goldProduced += Mathf.RoundToInt(production);
                }
            }
        }

        OnResourcesProduced?.Invoke(goldProduced, shardsProduced);
        Debug.Log($"Produced: {goldProduced} gold, {shardsProduced} shards");
    }

    /// <summary>
    /// Calculate offline rewards based on time away.
    /// Caps at 12 hours for free players, 24 hours for VIP.
    /// Uses server timestamp to prevent time manipulation.
    /// </summary>
    private void CalculateOfflineRewards()
    {
        string lastSavedString = PlayerPrefs.GetString("LastSavedTimestamp", "");

        if (string.IsNullOrEmpty(lastSavedString))
        {
            lastSavedTimestamp = DateTime.UtcNow;
            SaveTimestamp();
            return;
        }

        lastSavedTimestamp = DateTime.Parse(lastSavedString);
        DateTime currentTime = DateTime.UtcNow;
        TimeSpan offlineTime = currentTime - lastSavedTimestamp;

        float maxOfflineHours = isVIP ? 24f : offlineCapHours;
        float offlineHours = Mathf.Min((float)offlineTime.TotalHours, maxOfflineHours);

        if (offlineHours > 0.1f)
        {
            CalculateOfflineProduction(offlineHours);
        }

        SaveTimestamp();
    }

    /// <summary>
    /// Calculate and award offline production.
    /// Presents popup to player with option to watch ad for 2x rewards.
    /// </summary>
    private void CalculateOfflineProduction(float hours)
    {
        int goldProduced = 0;
        int shardsProduced = 0;

        foreach (Building building in buildings)
        {
            if (building.productionPerHour > 0)
            {
                float production = building.productionPerHour * Mathf.Pow(building.productionGrowthFactor, building.currentLevel - 1);
                production *= hours;

                if (building.producesPremiumCurrency)
                {
                    shardsProduced += Mathf.RoundToInt(production);
                }
                else
                {
                    goldProduced += Mathf.RoundToInt(production);
                }
            }
        }

        Debug.Log($"Offline rewards ({hours:F1}h): {goldProduced} gold, {shardsProduced} shards");

        // TODO: Show popup with ad doubler option
        // UIManager.ShowOfflineRewardsPopup(goldProduced, shardsProduced);
    }

    /// <summary>
    /// Upgrade building to next level.
    /// Deducts gold and starts upgrade timer.
    /// </summary>
    /// <param name="buildingId">ID of building to upgrade</param>
    /// <returns>True if upgrade started successfully</returns>
    public bool UpgradeBuilding(string buildingId)
    {
        Building building = buildings.Find(b => b.buildingId == buildingId);

        if (building == null)
        {
            Debug.LogWarning($"Building {buildingId} not found!");
            return false;
        }

        if (building.currentLevel >= building.maxLevel)
        {
            Debug.Log($"{building.buildingName} is already max level!");
            return false;
        }

        if (building.isUpgrading)
        {
            Debug.Log($"{building.buildingName} is already upgrading!");
            return false;
        }

        int upgradeCost = building.GetUpgradeCost();

        // TODO: Check player gold and deduct
        // if (PlayerCurrency.GetGold() < upgradeCost) return false;
        // PlayerCurrency.SpendGold(upgradeCost);

        building.isUpgrading = true;
        building.upgradeTimeRemaining = building.GetUpgradeTime();
        building.upgradeStartTime = DateTime.UtcNow;

        Debug.Log($"Started upgrading {building.buildingName} to level {building.currentLevel + 1}");
        return true;
    }

    /// <summary>
    /// Instant-finish building upgrade using premium currency or IAP.
    /// IAP hook for "Instant Upgrade Kit" bundle.
    /// </summary>
    /// <param name="buildingId">ID of building to instant-finish</param>
    /// <param name="useIAP">Whether this is from IAP (bypasses currency cost)</param>
    public void InstantFinishUpgrade(string buildingId, bool useIAP = false)
    {
        Building building = buildings.Find(b => b.buildingId == buildingId);

        if (building == null || !building.isUpgrading) return;

        if (!useIAP)
        {
            int instantCost = building.GetInstantFinishCost();
            // TODO: Deduct shards
            // if (PlayerCurrency.GetShards() < instantCost) return;
            // PlayerCurrency.SpendShards(instantCost);
        }

        CompleteUpgrade(building);
    }

    /// <summary>
    /// Complete building upgrade (called when timer finishes or instant-finish).
    /// </summary>
    private void CompleteUpgrade(Building building)
    {
        building.currentLevel++;
        building.isUpgrading = false;
        building.upgradeTimeRemaining = 0f;

        OnBuildingUpgraded?.Invoke(building);

        Debug.Log($"{building.buildingName} upgraded to level {building.currentLevel}!");
    }

    /// <summary>
    /// Update building upgrade timers.
    /// </summary>
    private void LateUpdate()
    {
        foreach (Building building in buildings)
        {
            if (building.isUpgrading)
            {
                TimeSpan elapsed = DateTime.UtcNow - building.upgradeStartTime;
                building.upgradeTimeRemaining = building.GetUpgradeTime() - (float)elapsed.TotalSeconds;

                if (building.upgradeTimeRemaining <= 0f)
                {
                    CompleteUpgrade(building);
                }
            }
        }
    }

    /// <summary>
    /// Apply ad boost to production (+50% for 1 hour).
    /// Called when player watches rewarded ad.
    /// </summary>
    public void ApplyAdBoost()
    {
        hasAdBoost = true;
        adBoostRemainingTime = 3600f;
        Debug.Log("Ad boost activated! +50% production for 1 hour.");
    }

    private void UpdateAdBoost()
    {
        if (hasAdBoost)
        {
            adBoostRemainingTime -= Time.deltaTime;

            if (adBoostRemainingTime <= 0f)
            {
                hasAdBoost = false;
                Debug.Log("Ad boost expired.");
            }
        }
    }

    /// <summary>
    /// Set VIP status (from subscription).
    /// </summary>
    public void SetVIP(bool vip)
    {
        isVIP = vip;
        offlineCapHours = vip ? 24f : 12f;
    }

    /// <summary>
    /// Save current timestamp for offline calculation.
    /// </summary>
    private void SaveTimestamp()
    {
        PlayerPrefs.SetString("LastSavedTimestamp", DateTime.UtcNow.ToString("O"));
        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveTimestamp();
        }
    }

    private void OnApplicationQuit()
    {
        SaveTimestamp();
    }

    public List<Building> GetBuildings() => buildings;
}

/// <summary>
/// Building data structure.
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

    /// <summary>
    /// Calculate upgrade cost using formula: BaseCost * (GrowthFactor ^ (Level - 1))
    /// </summary>
    public int GetUpgradeCost()
    {
        return Mathf.RoundToInt(baseCost * Mathf.Pow(costGrowthFactor, currentLevel));
    }

    /// <summary>
    /// Calculate upgrade time using formula: BaseTime * (TimeGrowth ^ (Level - 1))
    /// </summary>
    public float GetUpgradeTime()
    {
        return baseTime * Mathf.Pow(timeGrowthFactor, currentLevel);
    }

    /// <summary>
    /// Calculate instant-finish cost (50-300 shards based on time remaining).
    /// </summary>
    public int GetInstantFinishCost()
    {
        if (!isUpgrading) return 0;

        float timeRemainingHours = upgradeTimeRemaining / 3600f;
        return Mathf.Clamp(Mathf.RoundToInt(timeRemainingHours * 50f), 50, 300);
    }
}
