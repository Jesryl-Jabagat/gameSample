using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Simplified ShopManager without Unity IAP dependencies.
/// Handles shop UI and basic purchase simulation for testing.
/// </summary>
public class ShopManager : MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private bool enablePurchases = true;
    [SerializeField] private int playerGems = 100;
    [SerializeField] private int playerGold = 1000;

    [Header("Shop Items")]
    [SerializeField] private List<ShopItem> shopItems = new List<ShopItem>();

    public event Action<string, bool> OnPurchaseComplete;
    public event Action<int, int> OnRewardClaimed;

    [System.Serializable]
    public class ShopItem
    {
        public string itemId;
        public string itemName;
        public int gemCost;
        public int goldCost;
        public string description;
    }

    private void Start()
    {
        InitializeShop();
    }

    private void InitializeShop()
    {
        // Add default shop items
        if (shopItems.Count == 0)
        {
            shopItems.Add(new ShopItem 
            { 
                itemId = "health_potion", 
                itemName = "Health Potion", 
                gemCost = 10, 
                goldCost = 0, 
                description = "Restores 50 HP" 
            });
            
            shopItems.Add(new ShopItem 
            { 
                itemId = "gem_pack_small", 
                itemName = "Small Gem Pack", 
                gemCost = 0, 
                goldCost = 100, 
                description = "Get 50 gems" 
            });
            
            shopItems.Add(new ShopItem 
            { 
                itemId = "xp_boost", 
                itemName = "XP Boost", 
                gemCost = 25, 
                goldCost = 0, 
                description = "Double XP for 1 hour" 
            });
        }
        
        Debug.Log("Shop initialized with " + shopItems.Count + " items");
    }

    public bool PurchaseItem(string itemId)
    {
        if (!enablePurchases)
        {
            Debug.Log("Purchases are disabled");
            return false;
        }

        ShopItem item = shopItems.Find(x => x.itemId == itemId);
        if (item == null)
        {
            Debug.LogError("Item not found: " + itemId);
            OnPurchaseComplete?.Invoke(itemId, false);
            return false;
        }

        // Check if player has enough currency
        if (item.gemCost > 0 && playerGems < item.gemCost)
        {
            Debug.Log("Not enough gems for " + item.itemName);
            OnPurchaseComplete?.Invoke(itemId, false);
            return false;
        }

        if (item.goldCost > 0 && playerGold < item.goldCost)
        {
            Debug.Log("Not enough gold for " + item.itemName);
            OnPurchaseComplete?.Invoke(itemId, false);
            return false;
        }

        // Process purchase
        if (item.gemCost > 0) playerGems -= item.gemCost;
        if (item.goldCost > 0) playerGold -= item.goldCost;

        Debug.Log($"Purchased {item.itemName}! Gems: {playerGems}, Gold: {playerGold}");
        
        // Give item to player
        GiveItemToPlayer(itemId);
        
        OnPurchaseComplete?.Invoke(itemId, true);
        return true;
    }

    private void GiveItemToPlayer(string itemId)
    {
        switch (itemId)
        {
            case "health_potion":
                Debug.Log("Added Health Potion to inventory");
                break;
            case "gem_pack_small":
                playerGems += 50;
                Debug.Log("Added 50 gems to player");
                break;
            case "xp_boost":
                Debug.Log("Activated XP boost");
                break;
        }
    }

    public void WatchRewardedAd()
    {
        // Simulate watching an ad
        Debug.Log("Watching rewarded ad...");
        
        // Give reward after "watching" ad
        int gemReward = 10;
        playerGems += gemReward;
        
        Debug.Log($"Ad watched! Gained {gemReward} gems. Total: {playerGems}");
        OnRewardClaimed?.Invoke(gemReward, playerGems);
    }

    public int GetPlayerGems()
    {
        return playerGems;
    }

    public int GetPlayerGold()
    {
        return playerGold;
    }

    public List<ShopItem> GetShopItems()
    {
        return shopItems;
    }

    // For testing purposes
    [ContextMenu("Add Test Gems")]
    public void AddTestGems()
    {
        playerGems += 100;
        Debug.Log("Added 100 test gems. Total: " + playerGems);
    }

    [ContextMenu("Add Test Gold")]
    public void AddTestGold()
    {
        playerGold += 500;
        Debug.Log("Added 500 test gold. Total: " + playerGold);
    }
}