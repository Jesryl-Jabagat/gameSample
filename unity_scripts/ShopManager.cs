using UnityEngine;
using UnityEngine.Purchasing;
using System;
using System.Collections.Generic;

/// <summary>
/// ShopManager handles IAP purchases, rewarded ads, and shop UI.
/// Integrates with Unity IAP and Unity Ads SDKs.
/// Includes server-sync pseudocode and anti-duplication checks.
/// </summary>
public class ShopManager : MonoBehaviour, IStoreListener
{
    [Header("IAP Settings")]
    [SerializeField] private bool iapEnabled = true;

    [Header("Ad Settings")]
    [SerializeField] private int dailyAdLimit = 3;
    [SerializeField] private bool isVIP;

    private IStoreController storeController;
    private IExtensionProvider storeExtensionProvider;

    private int adsWatchedToday;
    private DateTime lastAdResetDate;

    public event Action<string, bool> OnPurchaseComplete;
    public event Action<int, int> OnAdRewardClaimed;

    private void Start()
    {
        InitializeIAP();
        LoadAdProgress();
    }

    /// <summary>
    /// Initialize Unity IAP with product catalog.
    /// Product IDs must match game_config.json and platform store listings.
    /// </summary>
    private void InitializeIAP()
    {
        if (!iapEnabled) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add IAP products from game_config.json
        builder.AddProduct("starter_pack_001", ProductType.NonConsumable);
        builder.AddProduct("progression_boost_001", ProductType.NonConsumable);
        builder.AddProduct("gems_tier1", ProductType.Consumable);
        builder.AddProduct("gems_tier2", ProductType.Consumable);
        builder.AddProduct("gems_tier3", ProductType.Consumable);
        builder.AddProduct("gems_tier4", ProductType.Consumable);
        builder.AddProduct("gems_tier5", ProductType.Consumable);
        builder.AddProduct("monthly_sub_001", ProductType.Subscription);
        builder.AddProduct("legendary_chest_001", ProductType.Consumable);
        builder.AddProduct("battle_pass_001", ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// IStoreListener callback: IAP initialization successful.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
        Debug.Log("IAP Initialized successfully!");
    }

    /// <summary>
    /// IStoreListener callback: IAP initialization failed.
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"IAP Initialization failed: {error}");
    }

    /// <summary>
    /// Purchase IAP product by ID.
    /// </summary>
    /// <param name="productId">Product ID from game_config.json</param>
    public void PurchaseProduct(string productId)
    {
        if (storeController == null)
        {
            Debug.LogError("IAP not initialized!");
            return;
        }

        Product product = storeController.products.WithID(productId);

        if (product == null || !product.availableToPurchase)
        {
            Debug.LogWarning($"Product {productId} not available for purchase!");
            return;
        }

        Debug.Log($"Initiating purchase: {productId}");
        storeController.InitiatePurchase(product);
    }

    /// <summary>
    /// IStoreListener callback: Purchase processing.
    /// Validates receipt with server before granting rewards.
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        string productId = args.purchasedProduct.definition.id;
        string receipt = args.purchasedProduct.receipt;

        Debug.Log($"Processing purchase: {productId}");

        // SERVER-SIDE VALIDATION (pseudocode)
        // In production, send receipt to backend for validation
        bool validationSuccess = ValidateReceiptWithServer(productId, receipt);

        if (validationSuccess)
        {
            GrantPurchaseRewards(productId);
            OnPurchaseComplete?.Invoke(productId, true);
            return PurchaseProcessingResult.Complete;
        }
        else
        {
            Debug.LogError("Receipt validation failed!");
            OnPurchaseComplete?.Invoke(productId, false);
            return PurchaseProcessingResult.Pending;
        }
    }

    /// <summary>
    /// Validate receipt with backend server.
    /// PSEUDOCODE: Replace with actual HTTP request to your server.
    /// Server should verify receipt with Google Play / App Store.
    /// </summary>
    private bool ValidateReceiptWithServer(string productId, string receipt)
    {
        // PSEUDOCODE for server validation:
        /*
        string serverUrl = "https://yourgameserver.com/api/purchase";
        
        WWWForm form = new WWWForm();
        form.AddField("product_id", productId);
        form.AddField("receipt", receipt);
        form.AddField("player_id", PlayerData.GetPlayerId());
        form.AddField("idempotency_token", GenerateIdempotencyToken());
        
        UnityWebRequest request = UnityWebRequest.Post(serverUrl, form);
        await request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            ValidationResponse response = JsonUtility.FromJson<ValidationResponse>(request.downloadHandler.text);
            return response.valid;
        }
        
        return false;
        */

        // For now, return true (DEVELOPMENT ONLY - NEVER IN PRODUCTION)
        Debug.LogWarning("Using client-side validation (DEVELOPMENT ONLY)");
        return true;
    }

    /// <summary>
    /// Grant rewards to player after successful purchase validation.
    /// Updates player currency, unlocks items, applies buffs.
    /// </summary>
    private void GrantPurchaseRewards(string productId)
    {
        // Check if one-time purchase already owned
        if (IsOneTimePurchase(productId) && HasPurchased(productId))
        {
            Debug.LogWarning($"One-time product {productId} already owned!");
            return;
        }

        switch (productId)
        {
            case "starter_pack_001":
                GrantStarterPack();
                break;

            case "progression_boost_001":
                GrantProgressionBoost();
                break;

            case "gems_tier1":
                GrantShards(120);
                break;

            case "gems_tier2":
                GrantShards(650);
                break;

            case "gems_tier3":
                GrantShards(1400);
                break;

            case "gems_tier4":
                GrantShards(3000);
                break;

            case "gems_tier5":
                GrantShards(8000);
                break;

            case "monthly_sub_001":
                ActivateVIPSubscription();
                break;

            case "legendary_chest_001":
                GrantLegendaryChest();
                break;

            case "battle_pass_001":
                UnlockBattlePass();
                break;

            default:
                Debug.LogWarning($"Unknown product ID: {productId}");
                break;
        }

        // Mark one-time purchases as owned
        if (IsOneTimePurchase(productId))
        {
            MarkAsPurchased(productId);
        }

        Debug.Log($"Rewards granted for {productId}");
    }

    private void GrantStarterPack()
    {
        GrantShards(1200);
        // TODO: Grant XP boosters, exclusive skin, sweep tickets
        Debug.Log("Starter Pack rewards granted!");
    }

    private void GrantProgressionBoost()
    {
        GrantShards(2000);
        // TODO: Instant base upgrade kit, 7-day XP buff, gold
        Debug.Log("Progression Boost rewards granted!");
    }

    private void GrantShards(int amount)
    {
        // TODO: Integrate with PlayerCurrency system
        // PlayerCurrency.AddShards(amount);
        Debug.Log($"Granted {amount} Solar Shards!");
    }

    private void ActivateVIPSubscription()
    {
        isVIP = true;
        // TODO: Apply VIP perks (building timer reduction, offline cap, etc.)
        Debug.Log("VIP Subscription activated!");
    }

    private void GrantLegendaryChest()
    {
        // TODO: Award legendary weapon + skin
        Debug.Log("Legendary Chest opened!");
    }

    private void UnlockBattlePass()
    {
        // TODO: Unlock premium Battle Pass track
        Debug.Log("Battle Pass premium unlocked!");
    }

    /// <summary>
    /// Check if product is one-time purchase.
    /// </summary>
    private bool IsOneTimePurchase(string productId)
    {
        return productId.Contains("starter_pack") || 
               productId.Contains("progression_boost") || 
               productId.Contains("battle_pass");
    }

    /// <summary>
    /// Check if one-time product already purchased.
    /// Stored locally and server-side for anti-exploit.
    /// </summary>
    private bool HasPurchased(string productId)
    {
        return PlayerPrefs.GetInt($"Purchased_{productId}", 0) == 1;
    }

    /// <summary>
    /// Mark one-time product as purchased.
    /// </summary>
    private void MarkAsPurchased(string productId)
    {
        PlayerPrefs.SetInt($"Purchased_{productId}", 1);
        PlayerPrefs.Save();

        // TODO: Sync to server
        // ServerSync.MarkProductPurchased(productId);
    }

    /// <summary>
    /// IStoreListener callback: Purchase failed.
    /// </summary>
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"Purchase failed: {product.definition.id}, Reason: {failureReason}");
        OnPurchaseComplete?.Invoke(product.definition.id, false);
    }

    // ==================== REWARDED ADS ====================

    /// <summary>
    /// Show rewarded ad for daily resource pack.
    /// Limited to 3 ads/day for free players, unlimited for VIP.
    /// </summary>
    public void ShowRewardedAd_ResourcePack()
    {
        if (!CanWatchAd())
        {
            Debug.Log("Daily ad limit reached!");
            return;
        }

        // Unity Ads integration (pseudocode)
        /*
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", new ShowOptions
            {
                resultCallback = result =>
                {
                    if (result == ShowResult.Finished)
                    {
                        GrantAdReward_ResourcePack();
                    }
                }
            });
        }
        */

        // For development, grant reward directly
        GrantAdReward_ResourcePack();
    }

    /// <summary>
    /// Grant resource pack reward after watching ad.
    /// Generates server-signed token to prevent replay attacks.
    /// </summary>
    private void GrantAdReward_ResourcePack()
    {
        // SERVER VALIDATION (pseudocode):
        // string rewardToken = RequestRewardTokenFromServer("resource_pack");
        // if (!ValidateRewardToken(rewardToken)) return;

        int goldReward = 500;
        int materialsReward = 50;

        // TODO: Grant rewards
        // PlayerCurrency.AddGold(goldReward);
        // PlayerInventory.AddCraftingMaterials(materialsReward);

        IncrementAdCount();
        OnAdRewardClaimed?.Invoke(goldReward, materialsReward);

        Debug.Log($"Ad reward granted: {goldReward} gold, {materialsReward} materials");
    }

    /// <summary>
    /// Show rewarded ad for production boost.
    /// </summary>
    public void ShowRewardedAd_ProductionBoost()
    {
        if (!CanWatchAd()) return;

        // TODO: Show Unity Ad
        GrantAdReward_ProductionBoost();
    }

    private void GrantAdReward_ProductionBoost()
    {
        // Apply +50% production for 1 hour
        BaseManager baseManager = FindObjectOfType<BaseManager>();
        if (baseManager != null)
        {
            baseManager.ApplyAdBoost();
        }

        IncrementAdCount();
        Debug.Log("Production boost activated!");
    }

    /// <summary>
    /// Show rewarded ad for offline doubler.
    /// </summary>
    public void ShowRewardedAd_OfflineDoubler()
    {
        if (!CanWatchAd()) return;

        // TODO: Show Unity Ad
        GrantAdReward_OfflineDoubler();
    }

    private void GrantAdReward_OfflineDoubler()
    {
        // TODO: Double offline rewards (handled in BaseManager)
        IncrementAdCount();
        Debug.Log("Offline rewards doubled!");
    }

    /// <summary>
    /// Check if player can watch more ads today.
    /// </summary>
    private bool CanWatchAd()
    {
        CheckAdResetDate();

        if (isVIP) return true;

        return adsWatchedToday < dailyAdLimit;
    }

    /// <summary>
    /// Increment daily ad count.
    /// </summary>
    private void IncrementAdCount()
    {
        adsWatchedToday++;
        SaveAdProgress();
    }

    /// <summary>
    /// Check if daily ad counter should reset (UTC midnight).
    /// </summary>
    private void CheckAdResetDate()
    {
        DateTime now = DateTime.UtcNow;

        if (lastAdResetDate.Date != now.Date)
        {
            adsWatchedToday = 0;
            lastAdResetDate = now;
            SaveAdProgress();
        }
    }

    /// <summary>
    /// Save ad progress to PlayerPrefs.
    /// </summary>
    private void SaveAdProgress()
    {
        PlayerPrefs.SetInt("AdsWatchedToday", adsWatchedToday);
        PlayerPrefs.SetString("LastAdResetDate", lastAdResetDate.ToString("O"));
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Load ad progress from PlayerPrefs.
    /// </summary>
    private void LoadAdProgress()
    {
        adsWatchedToday = PlayerPrefs.GetInt("AdsWatchedToday", 0);
        string lastResetString = PlayerPrefs.GetString("LastAdResetDate", DateTime.UtcNow.ToString("O"));
        lastAdResetDate = DateTime.Parse(lastResetString);

        CheckAdResetDate();
    }

    /// <summary>
    /// Get remaining ads for today.
    /// </summary>
    public int GetRemainingAds()
    {
        CheckAdResetDate();
        return isVIP ? 999 : Mathf.Max(0, dailyAdLimit - adsWatchedToday);
    }

    /// <summary>
    /// Set VIP status (from subscription purchase).
    /// </summary>
    public void SetVIP(bool vip)
    {
        isVIP = vip;
    }
}
