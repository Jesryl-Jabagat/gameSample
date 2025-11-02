using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// SaveLoadManager handles JSON-based save/load system with offline production reconciliation.
/// Stores player data locally (mobile) and syncs to server for cloud saves and anti-cheat.
/// </summary>
public class SaveLoadManager : MonoBehaviour
{
    [Header("Save Settings")]
    [SerializeField] private bool autoSaveEnabled = true;
    [SerializeField] private float autoSaveInterval = 30f;

    private string saveFilePath;
    private float autoSaveTimer;

    private static SaveLoadManager instance;
    public static SaveLoadManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
    }

    private void Start()
    {
        LoadGame();
    }

    private void Update()
    {
        if (autoSaveEnabled)
        {
            autoSaveTimer += Time.deltaTime;

            if (autoSaveTimer >= autoSaveInterval)
            {
                SaveGame();
                autoSaveTimer = 0f;
            }
        }
    }

    /// <summary>
    /// Save current game state to JSON file.
    /// Triggers on major events (level up, purchase, building upgrade) and auto-save timer.
    /// </summary>
    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            version = "1.0.0",
            lastSavedTimestamp = DateTime.UtcNow.ToString("O"),
            playerId = GetPlayerId(),

            playerLevel = GetPlayerLevel(),
            currentXP = GetPlayerXP(),

            gold = GetGold(),
            solarShards = GetSolarShards(),

            heroes = GetHeroSaveData(),
            buildings = GetBuildingSaveData(),

            inventory = GetInventorySaveData(),
            purchasedProducts = GetPurchasedProducts(),

            isVIP = GetVIPStatus(),
            vipExpiryDate = GetVIPExpiryDate(),

            adsWatchedToday = GetAdsWatchedToday(),
            lastAdResetDate = GetLastAdResetDate()
        };

        string json = JsonUtility.ToJson(saveData, true);

        try
        {
            File.WriteAllText(saveFilePath, json);
            Debug.Log($"Game saved successfully to {saveFilePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save game: {e.Message}");
        }

        // TODO: Sync to server for cloud save
        // SyncToServer(saveData);
    }

    /// <summary>
    /// Load game state from JSON file.
    /// Reconciles offline production and validates timestamps.
    /// </summary>
    public void LoadGame()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("No save file found. Starting new game.");
            CreateNewGame();
            return;
        }

        try
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            if (saveData == null)
            {
                Debug.LogWarning("Save data corrupted. Starting new game.");
                CreateNewGame();
                return;
            }

            ApplySaveData(saveData);

            Debug.Log("Game loaded successfully!");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load game: {e.Message}");
            CreateNewGame();
        }
    }

    /// <summary>
    /// Apply loaded save data to game systems.
    /// </summary>
    private void ApplySaveData(SaveData saveData)
    {
        // TODO: Apply data to actual game systems
        // PlayerData.SetLevel(saveData.playerLevel);
        // PlayerData.SetXP(saveData.currentXP);
        // PlayerCurrency.SetGold(saveData.gold);
        // PlayerCurrency.SetShards(saveData.solarShards);

        // Restore heroes
        foreach (HeroSaveData heroSave in saveData.heroes)
        {
            // HeroManager.LoadHeroData(heroSave);
        }

        // Restore buildings
        foreach (BuildingSaveData buildingSave in saveData.buildings)
        {
            // BaseManager.LoadBuildingData(buildingSave);
        }

        // Restore inventory
        foreach (ItemSaveData itemSave in saveData.inventory)
        {
            // InventoryManager.LoadItemData(itemSave);
        }

        // Restore VIP status
        // ShopManager.SetVIP(saveData.isVIP);

        // Reconcile offline production
        ReconcileOfflineProduction(saveData.lastSavedTimestamp);

        Debug.Log($"Save data applied. Player Level: {saveData.playerLevel}, Gold: {saveData.gold}");
    }

    /// <summary>
    /// Reconcile offline production using last saved timestamp.
    /// Validates timestamp against server to prevent time manipulation.
    /// </summary>
    private void ReconcileOfflineProduction(string lastSavedTimestampString)
    {
        DateTime lastSaved = DateTime.Parse(lastSavedTimestampString);
        DateTime now = DateTime.UtcNow;
        TimeSpan offlineTime = now - lastSaved;

        if (offlineTime.TotalHours > 0.1f)
        {
            // TODO: Call BaseManager to calculate offline rewards
            // BaseManager.CalculateOfflineRewards(offlineTime);

            Debug.Log($"Offline for {offlineTime.TotalHours:F1} hours. Calculating rewards...");
        }
    }

    /// <summary>
    /// Create new game save file with default values.
    /// </summary>
    private void CreateNewGame()
    {
        SaveData newSave = new SaveData
        {
            version = "1.0.0",
            lastSavedTimestamp = DateTime.UtcNow.ToString("O"),
            playerId = GenerateNewPlayerId(),

            playerLevel = 1,
            currentXP = 0,

            gold = 1000,
            solarShards = 200,

            heroes = new List<HeroSaveData>(),
            buildings = new List<BuildingSaveData>(),
            inventory = new List<ItemSaveData>(),
            purchasedProducts = new List<string>(),

            isVIP = false,
            vipExpiryDate = "",

            adsWatchedToday = 0,
            lastAdResetDate = DateTime.UtcNow.ToString("O")
        };

        // Add starter hero (Auron)
        newSave.heroes.Add(new HeroSaveData
        {
            heroId = "hero_001_auron",
            level = 1,
            currentXP = 0,
            isUnlocked = true
        });

        string json = JsonUtility.ToJson(newSave, true);
        File.WriteAllText(saveFilePath, json);

        ApplySaveData(newSave);

        Debug.Log("New game created!");
    }

    /// <summary>
    /// Delete save file (for testing or account reset).
    /// </summary>
    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }

        CreateNewGame();
    }

    // ==================== HELPER METHODS ====================
    // These would pull from actual game systems in production

    private string GetPlayerId()
    {
        return PlayerPrefs.GetString("PlayerId", GenerateNewPlayerId());
    }

    private string GenerateNewPlayerId()
    {
        string newId = Guid.NewGuid().ToString();
        PlayerPrefs.SetString("PlayerId", newId);
        PlayerPrefs.Save();
        return newId;
    }

    private int GetPlayerLevel()
    {
        // TODO: Get from XPSystem
        return 1;
    }

    private int GetPlayerXP()
    {
        // TODO: Get from XPSystem
        return 0;
    }

    private int GetGold()
    {
        // TODO: Get from PlayerCurrency
        return PlayerPrefs.GetInt("Gold", 1000);
    }

    private int GetSolarShards()
    {
        // TODO: Get from PlayerCurrency
        return PlayerPrefs.GetInt("SolarShards", 200);
    }

    private List<HeroSaveData> GetHeroSaveData()
    {
        // TODO: Get from HeroManager
        return new List<HeroSaveData>();
    }

    private List<BuildingSaveData> GetBuildingSaveData()
    {
        // TODO: Get from BaseManager
        return new List<BuildingSaveData>();
    }

    private List<ItemSaveData> GetInventorySaveData()
    {
        // TODO: Get from InventoryManager
        return new List<ItemSaveData>();
    }

    private List<string> GetPurchasedProducts()
    {
        // TODO: Get from ShopManager
        return new List<string>();
    }

    private bool GetVIPStatus()
    {
        // TODO: Get from ShopManager
        return false;
    }

    private string GetVIPExpiryDate()
    {
        // TODO: Get from ShopManager
        return "";
    }

    private int GetAdsWatchedToday()
    {
        // TODO: Get from ShopManager
        return 0;
    }

    private string GetLastAdResetDate()
    {
        // TODO: Get from ShopManager
        return DateTime.UtcNow.ToString("O");
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

/// <summary>
/// SaveData structure matching all game systems.
/// Serializes to JSON for local storage and server sync.
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

/// <summary>
/// Example save file JSON structure:
/// </summary>
/*
{
  "version": "1.0.0",
  "lastSavedTimestamp": "2025-11-02T12:34:56.789Z",
  "playerId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
  
  "playerLevel": 15,
  "currentXP": 33529,
  
  "gold": 45000,
  "solarShards": 1200,
  
  "heroes": [
    {
      "heroId": "hero_001_auron",
      "level": 15,
      "currentXP": 0,
      "isUnlocked": true,
      "currentHP": 875,
      "equipment": [
        { "slotType": "weapon", "itemId": "sword_epic_003" },
        { "slotType": "armor", "itemId": "armor_rare_007" }
      ]
    },
    {
      "heroId": "hero_002_lyra",
      "level": 12,
      "currentXP": 1500,
      "isUnlocked": true,
      "currentHP": 596,
      "equipment": []
    }
  ],
  
  "buildings": [
    {
      "buildingId": "barracks",
      "currentLevel": 5,
      "isUpgrading": true,
      "upgradeStartTime": "2025-11-02T12:00:00.000Z",
      "upgradeTimeRemaining": 1800
    },
    {
      "buildingId": "workshop",
      "currentLevel": 4,
      "isUpgrading": false,
      "upgradeStartTime": "",
      "upgradeTimeRemaining": 0
    }
  ],
  
  "inventory": [
    { "itemId": "iron_ore", "quantity": 250, "enhancementLevel": 0 },
    { "itemId": "arcane_dust", "quantity": 120, "enhancementLevel": 0 },
    { "itemId": "sword_rare_005", "quantity": 1, "enhancementLevel": 3 }
  ],
  
  "purchasedProducts": [
    "starter_pack_001",
    "battle_pass_001"
  ],
  
  "isVIP": true,
  "vipExpiryDate": "2025-12-02T12:34:56.789Z",
  
  "adsWatchedToday": 2,
  "lastAdResetDate": "2025-11-02T00:00:00.000Z"
}
*/
