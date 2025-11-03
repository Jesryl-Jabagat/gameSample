# MONETIZATION PLAN
## Eclipse Reborn: Chronicles of the Lost Sun

---

## PHILOSOPHY & FAIRNESS

Eclipse Reborn is designed as a **fair free-to-play** game where:
- All gameplay content is accessible without spending
- Monetization focuses on **convenience** and **cosmetics**, not power
- F2P players can reach endgame; it just takes longer
- No "pay-to-win" mechanics; whales can't dominate F2P players
- Transparent pricing with clear value propositions

**Target Revenue Mix:**
- IAP: 60-70% of revenue
- Rewarded Ads: 30-40% of revenue
- Target ARPU: $2-5/month (healthy for mobile F2P)

---

## 1. REWARDED ADS STRATEGY

### 1.1 Daily Limits

| Player Type | Ads Per Day | Notes |
|-------------|-------------|-------|
| Free Player | 3 ads/day | Resets at UTC midnight |
| VIP Member | Unlimited | No cap, same rewards per ad |

### 1.2 Ad Reward Types

**AD #1: Resource Pack** (1x per day)
- **Reward:** 500 Gold + 50 Crafting Materials
- **Value:** ~$0.50 equivalent in game currency
- **Placement:** Main menu "Claim Daily Bonus" button
- **Duration:** 15-30 second video ad
- **Cooldown:** 24 hours (resets at UTC midnight)

**AD #2: Production Boost** (1x per day)
- **Reward:** +50% production speed for 1 hour (all buildings)
- **Value:** Accelerates ~200 gold/hour → 300 gold/hour
- **Placement:** Sanctuary screen "Watch Ad to Boost" button
- **Duration:** 15-30 second video ad
- **Cooldown:** 24 hours
- **Stack:** Does not stack with itself; refreshes timer if watched again

**AD #3: Offline Doubler** (1x per day)
- **Reward:** 2x all offline rewards when returning after 2+ hours
- **Value:** Variable (depends on offline time, 2-12 hours capped)
- **Placement:** Popup when logging in after offline session
- **Duration:** 15-30 second video ad
- **Cooldown:** 24 hours
- **Example:** 4 hours offline = 800 gold → watch ad = 1,600 gold

### 1.3 Ad Integration (Unity Ads SDK)

```csharp
// Pseudocode for ad flow
if (PlayerCanWatchAd())
{
    ShowRewardedAd("resource_pack", OnAdComplete);
}

void OnAdComplete(bool adWatched)
{
    if (adWatched)
    {
        string rewardToken = RequestServerToken("ad_resource_pack");
        if (ValidateToken(rewardToken))
        {
            GrantReward(500_gold, 50_materials);
            IncrementAdCount();
        }
    }
}
```

### 1.4 Ad Revenue Projections

**Assumptions:**
- 30% of DAU watch at least 1 ad/day
- eCPM (effective cost per thousand impressions): $5-10
- Average 2 ads/day per engaged user

**Monthly Projection (10,000 DAU):**
- Ads watched/month: 10,000 * 0.30 * 2 ads * 30 days = 180,000 impressions
- Revenue: 180 * $7.50 (avg eCPM) = **$1,350/month from ads**

---

## 2. IAP PRODUCTS & PRICING

### 2.1 Currency Conversion

- **Solar Shards (Premium Currency):** $1 USD = 100 shards
- **Gold (Soft Currency):** Not directly purchasable (earned in-game)

### 2.2 Starter Pack (ONE-TIME, $4.99)

**Product ID:** `starter_pack_001`  
**Price:** $4.99 USD  
**Purchasable:** Once per account  

**Contents:**
- 1,200 Solar Shards (equiv. $12 value)
- 3x XP Booster (1 hour each, +50% XP)
- Exclusive "Dawn Voyager" Auron Skin (cosmetic only)
- 10x Sweep Tickets

**Value Proposition:** 3x value ($24 equivalent for $4.99 = 80% savings)

**Target Audience:** New players in first 48 hours

**Conversion Rate Target:** 5-8% of new players

**Monthly Revenue (1,000 new users):** 70 purchases * $4.99 = **$349/month**

---

### 2.3 Progression Boost (ONE-TIME, $9.99)

**Product ID:** `progression_boost_001`  
**Price:** $9.99 USD  
**Purchasable:** Once per account  

**Contents:**
- Instant Base Upgrade Kit (upgrades all buildings +1 level, max level 5)
- 7-Day XP Buff (+10% XP gain, stacks with boosters)
- 2,000 Solar Shards (equiv. $20 value)
- 50,000 Gold

**Value Proposition:** 2.5x value ($40+ equivalent for $9.99 = 75% savings)

**Target Audience:** Players at level 10-15 who are engaged but progressing slowly

**Conversion Rate Target:** 3-5% of week 2 players

**Monthly Revenue (300 engaged users):** 12 purchases * $9.99 = **$120/month**

---

### 2.4 Solar Shards Gem Packs (REPEATABLE)

| Pack Name | Product ID | Price | Shards | Bonus | Total | $/100 Shards |
|-----------|-----------|-------|--------|-------|-------|--------------|
| Small Pouch | `gems_tier1` | $0.99 | 100 | +20 | 120 | $0.83 |
| Medium Sack | `gems_tier2` | $4.99 | 500 | +150 | 650 | $0.77 |
| Large Chest | `gems_tier3` | $9.99 | 1,200 | +200 | 1,400 | $0.71 |
| Epic Hoard | `gems_tier4` | $19.99 | 2,500 | +500 | 3,000 | $0.67 |
| **Legendary Vault** | `gems_tier5` | $49.99 | 6,500 | +1,500 | **8,000** | **$0.62** ✅ BEST VALUE |

**Design Notes:**
- Bonus scales with price (encourages larger purchases)
- Legendary Vault is "Best Value" (lowest $/100 shards ratio)
- No artificial scarcity (always available)

**Shards Usage (In-Game):**
- Instant-finish building: 50-300 shards (scales with time)
- Instant-craft gear: 20-200 shards
- Revive in combat: 50 shards
- Energy refill: 100 shards (if stamina added later)
- Cosmetic gacha: 300 shards/pull (cosmetic only, no power)

**Target Conversion:** 2-3% of monthly active users

**Monthly Revenue (10,000 MAU):**
- 250 purchases * $15 avg = **$3,750/month**

---

### 2.5 Solar Blessing Pass (SUBSCRIPTION, $29.99/month)

**Product ID:** `monthly_sub_001`  
**Price:** $29.99 USD/month  
**Type:** Auto-renewing subscription (cancel anytime)  

**Daily Benefits:**
- 100 Solar Shards on login (3,000 shards/month = $30 value)

**VIP Perks:**
- -20% building upgrade timers (e.g., 1 hour → 48 minutes)
- 24-hour offline accumulation cap (vs 12 hours for free)
- Unlimited rewarded ads (no 3/day limit)
- Exclusive VIP chat frame + title (cosmetic)
- Monthly exclusive mount skin (cosmetic, changes each month)

**Total Value:** $90+ per month (shards + time savings + cosmetics)

**Target Audience:** Dedicated players (Level 20+, play daily)

**Conversion Rate Target:** 0.5-1% of MAU (healthy for premium subscription)

**Monthly Revenue (10,000 MAU):** 75 subs * $29.99 = **$2,249/month**

**Retention Strategy:**
- Day 1 claim bonus: Extra 200 shards on first day
- Streak rewards: Claim all 30 days = bonus legendary skin
- Reminder: Push notification "Don't forget to claim today's 100 shards!"

---

### 2.6 Legendary Chest (WEEKLY OFFER, $49.99)

**Product ID:** `legendary_chest_001`  
**Price:** $49.99 USD  
**Availability:** Weekly rotation (refreshes every 7 days)  
**Purchase Limit:** 5 per week (prevents whale dominance)  

**Contents:**
- 1x Legendary Weapon (random from pool of 10)
- 1x Legendary Hero Skin (player choice from 5 options)
- 3,000 Solar Shards (equiv. $30 value)
- 100,000 Gold

**Value Proposition:** $100+ value for $49.99 (50% savings)

**Target Audience:** Whales, collectors, players seeking specific legendary items

**Conversion Rate:** 0.1-0.2% of MAU (premium whale offer)

**Monthly Revenue (10,000 MAU):** 12 purchases * $49.99 = **$600/month**

---

### 2.7 Battle Pass (SEASONAL, $9.99 per season)

**Product ID:** `battle_pass_001`  
**Price:** $9.99 USD per 30-day season  
**Type:** Non-consumable (one-time per season)  

**Free Track Rewards (All Players):**
- Tiers 1-30: Small gold amounts, common materials, 1 rare skin (tier 30)

**Premium Track Rewards (Paid):**
- Tiers 1-30: 3 Epic skins, 1 Legendary skin, 2,000 shards, 20 Sweep Tickets, exclusive emote
- Instant unlock of all tiers (no grind required if paid)

**XP Progression:**
- Earn Battle Pass XP from daily quests, stage completions, events
- Free: ~20 hours of gameplay to reach tier 30
- Premium: Same grind, but better rewards at each tier

**Tier Skip Option:**
- Cost: 150 shards per tier
- Total cost to skip all 30 tiers: 4,500 shards (~$45)
- Discouraged by design (grinding is more economical)

**Target Audience:** Engaged players who play daily and want cosmetics

**Conversion Rate:** 3-5% of active players (industry standard)

**Monthly Revenue (10,000 MAU):** 400 purchases * $9.99 = **$3,996/month**

**Retention Boost:** Battle Pass increases daily login retention by 15-20%

---

## 3. F2P VS PAID PROGRESSION BALANCE

### 3.1 F2P Player Journey (No Spending)

| Milestone | F2P Time | How Achieved |
|-----------|----------|--------------|
| Reach Level 20 | 14 days | Daily 30-min sessions, 50 XP/stage |
| Unlock All 5 Heroes | 21 days | Story progression (Chapters 1-20) |
| Clear Main Story (Ch. 50) | 70 days | ~2.5 months of regular play |
| Max Sanctuary Buildings | 42 days | ~6 weeks of upgrades + offline production |
| Earn 200 Shards/Month | Ongoing | Shard Reactor (6/day) + events + Battle Pass free track |

**F2P Earning Rates:**
- Gold: 200/hour (Treasury) + 50/stage = ~6,000 gold/day (active play)
- Shards: 6/day (Shard Reactor) + 50/month (events) = ~200 shards/month
- Crafting Materials: 1/hour (Workshop) + stage drops

**F2P Bottlenecks:**
- Building timers (1-3 hours per upgrade)
- Limited instant-finish options (must wait or spend shards)
- Slower hero unlocks (gated by story chapters)

---

### 3.2 Light Spender Journey ($10-20/month)

| Milestone | Light Spender Time | Improvement vs F2P |
|-----------|-------------------|---------------------|
| Reach Level 20 | 7 days | 50% faster (XP boosters) |
| Unlock All 5 Heroes | 14 days | 33% faster (instant upgrades) |
| Clear Main Story | 35 days | 50% faster (Battle Pass + boosters) |
| Max Sanctuary Buildings | 21 days | 50% faster (instant-finish some upgrades) |

**Spending Breakdown:**
- Starter Pack ($4.99) + Battle Pass ($9.99) = $14.98
- Occasional gem pack ($4.99) = ~$20/month total

**Benefits:**
- XP boosters speed up leveling
- Battle Pass gives extra shards for instant-finishing
- Cosmetic skins for favorite heroes

---

### 3.3 Whale Journey ($100+/month)

| Milestone | Whale Time | Improvement vs F2P |
|-----------|------------|---------------------|
| Reach Level 20 | 3 days | 80% faster (constant boosters) |
| Unlock All 5 Heroes | 7 days | 66% faster (instant everything) |
| Clear Main Story | 14 days | 80% faster (unlimited resources) |
| Max Sanctuary Buildings | 7 days | 83% faster (instant all upgrades) |

**Spending Breakdown:**
- VIP Subscription ($29.99/month)
- Gem packs ($50-100/month)
- Battle Pass ($9.99)
- Legendary Chests ($50-200/month)
- Total: $150-400/month

**Benefits:**
- Reach endgame in 2-3 weeks
- Collect all cosmetics
- Focus on endgame content (PvP, endless dungeons, events)

**Power Ceiling:**
- Same as F2P (Level 50, max gear)
- No exclusive power items
- Advantage: Reached ceiling faster, more time to enjoy endgame

---

## 4. ANTI-EXPLOIT & FAIRNESS MEASURES

### 4.1 Anti-Whale Dominance

| Measure | Description |
|---------|-------------|
| Power Ceiling | All players cap at Level 50, same max gear stats |
| No Exclusive Power | Paid items are cosmetic or time-savers, not stat boosts |
| Weekly Purchase Limits | Legendary Chest limited to 5/week |
| PvP Matchmaking | (If added) Match by power level, not spending |

### 4.2 Anti-Exploit (Technical)

| Exploit Vector | Prevention |
|---------------|------------|
| Time Manipulation | Server-side timestamp validation for offline rewards |
| Duplicate Purchases | Idempotency tokens, receipt validation with Google/Apple |
| Ad Fraud | Unity Ads fraud detection, server-signed reward tokens |
| Refund Abuse | Auto-revoke items if refund detected via platform callback |
| Account Sharing | Invalidate sessions on new device login |

### 4.3 One-Time Purchase Enforcement

```csharp
// Pseudocode for one-time purchase check
bool CanPurchase(string productId)
{
    if (IsOneTimePurchase(productId))
    {
        bool ownedLocally = PlayerPrefs.GetInt($"Purchased_{productId}") == 1;
        bool ownedOnServer = ServerAPI.CheckPurchaseStatus(productId);
        
        return !ownedLocally && !ownedOnServer;
    }
    return true; // Consumables always purchasable
}
```

### 4.4 Server-Side Receipt Validation

**Flow:**
1. Player initiates IAP → Unity IAP SDK → Platform payment dialog
2. Payment succeeds → Receipt returned to client
3. Client sends receipt to game server with idempotency token
4. Server forwards receipt to Google Play / App Store API for validation
5. If valid → Grant rewards + log transaction in database
6. If invalid → Retry 3x, flag for CS review, do not grant rewards

**Idempotency Token:**
- Prevents duplicate grants if network fails during step 5
- Format: `{playerId}_{productId}_{timestamp}_{random_uuid}`
- Stored server-side for 7 days, rejected if duplicate detected

---

## 5. UX FLOW FOR PURCHASES

### 5.1 IAP Purchase Flow

```
User taps "BUY" button in Shop
  ↓
Confirmation popup: "Confirm purchase of {product} for ${price}?"
  ↓
User taps "CONFIRM"
  ↓
Unity IAP initiates platform payment dialog (Google Play / App Store)
  ↓
User completes payment (fingerprint, face ID, password, etc.)
  ↓
Platform returns receipt to Unity IAP
  ↓
Unity IAP ProcessPurchase() called
  ↓
Client sends receipt to server for validation
  ↓
Server validates receipt with Google/Apple API
  ↓
IF VALID:
  - Server grants rewards to player account
  - Server logs transaction (player_id, product_id, timestamp, receipt_hash)
  - Server returns success to client
  - Client shows "Purchase Successful!" popup with rewards
  - Client plays success SFX + confetti VFX
  - Client updates UI (currency counters, inventory)
IF INVALID:
  - Server logs failed validation attempt
  - Server returns error to client
  - Client shows "Purchase Failed" popup with error message
  - Client offers "Retry" or "Contact Support"
```

### 5.2 Rewarded Ad Flow

```
User taps "Watch Ad" button
  ↓
Confirmation popup: "Watch 30s ad for {reward}?"
  ↓
User taps "WATCH"
  ↓
Unity Ads SDK shows video ad (15-30s)
  ↓
User watches ad to completion (or ad ends early if skippable)
  ↓
Unity Ads callback: ShowResult.Finished
  ↓
Client requests signed reward token from server
  ↓
Server generates token: HMAC_SHA256(reward_type + player_id + timestamp + secret)
  ↓
Server returns token to client (expires in 60 seconds)
  ↓
Client sends token + reward_type to server
  ↓
Server validates token signature + expiry
  ↓
IF VALID:
  - Server grants reward to player
  - Server increments daily ad count
  - Server returns success
  - Client shows "Reward Claimed!" popup
IF INVALID:
  - Server rejects (expired or tampered token)
  - Client shows error: "Reward already claimed or expired"
```

### 5.3 Error Handling

| Error | User-Facing Message | Recovery |
|-------|---------------------|----------|
| Payment Declined | "Payment declined by your bank. Please check your payment method." | Offer "Retry" or "Contact Support" |
| Network Timeout | "Network error. Your purchase may still process. Check your receipt and contact support if charged but no items received." | Log transaction for CS review |
| Receipt Validation Failed | "Purchase verification failed. Please try again or contact support with transaction ID: {id}" | Retry validation 3x, then manual CS review |
| One-Time Already Owned | "You already own this item!" | Disable purchase button, show "OWNED" label |
| Ad Failed to Load | "No ads available right now. Please try again later!" | Allow retry after 5 minutes |

---

## 6. REVENUE PROJECTIONS

### 6.1 Monthly Revenue Breakdown (10,000 MAU)

| Revenue Source | Monthly Revenue | % of Total |
|----------------|-----------------|------------|
| Gem Packs (Consumable) | $3,750 | 31% |
| Battle Pass | $3,996 | 33% |
| VIP Subscription | $2,249 | 19% |
| One-Time Bundles (Starter + Progression) | $469 | 4% |
| Legendary Chests | $600 | 5% |
| Rewarded Ads | $1,350 | 11% |
| **TOTAL** | **$12,414** | **100%** |

**ARPU (Average Revenue Per User):** $12,414 / 10,000 MAU = **$1.24/month**

*(Note: Healthy F2P mobile games target $1-3 ARPU; Eclipse Reborn is on track)*

### 6.2 6-Month Revenue Forecast

| Month | MAU | Revenue | Notes |
|-------|-----|---------|-------|
| Month 1 (Launch) | 30,000 | $25,000 | High new player bundles, lower VIP |
| Month 2 | 20,000 | $18,000 | Retention dip, steady IAP |
| Month 3 | 15,000 | $20,000 | PvP launch drives engagement |
| Month 4 | 15,000 | $22,000 | Guild system, steady growth |
| Month 5 | 18,000 | $25,000 | Co-op raids, event surge |
| Month 6 | 20,000 | $30,000 | Endgame content, loyal whales |
| **Total** | — | **$140,000** | **6-month cumulative** |

---

## 7. A/B TESTING PLAN

### 7.1 Pricing Tests

| Test | Variant A | Variant B | Measure |
|------|-----------|-----------|---------|
| Starter Pack Price | $4.99 | $2.99 | Conversion rate, total revenue |
| Battle Pass Price | $9.99 | $7.99 | Purchase rate, retention |
| VIP Subscription | $29.99 | $24.99 | Sub rate, LTV |

### 7.2 Ad Placement Tests

| Test | Variant A | Variant B | Measure |
|------|-----------|-----------|---------|
| Offline Doubler Popup | Show immediately on login | Show after 5 seconds | Ad completion rate |
| Production Boost Button | Persistent in Sanctuary | Only when production ready | Click-through rate |

### 7.3 Conversion Funnel Optimization

**Funnel:** Shop Visit → Product View → Purchase Confirm → Payment Complete

| Stage | Metric | Target | Current | Action |
|-------|--------|--------|---------|--------|
| Shop Visit | % of DAU visiting shop | 40% | TBD | A/B test shop button placement |
| Product View | % clicking product | 60% | TBD | A/B test product card design |
| Purchase Confirm | % confirming purchase | 70% | TBD | Simplify confirmation popup |
| Payment Complete | % completing payment | 85% | TBD | Investigate payment failures |

---

## 8. COMPLIANCE & LEGAL

### 8.1 Age Rating & Restrictions

- **ESRB:** E10+ (Everyone 10+, Fantasy Violence)
- **PEGI:** 7 (Mild Fantasy Violence)
- **Google Play / App Store:** 9+ rating

### 8.2 Regulatory Compliance

| Regulation | Requirement | Implementation |
|------------|-------------|----------------|
| COPPA (USA) | No data collection from <13 | Age gate on first launch |
| GDPR (EU) | User consent for data processing | Privacy policy + consent checkbox |
| Loot Box Laws (Belgium, Netherlands) | No randomized paid rewards | Gacha is cosmetic-only, earned currency |
| Apple App Store | Transparent IAP pricing | All prices shown in local currency |
| Google Play | Prominent IAP disclosures | "In-App Purchases" label on store listing |

### 8.3 Subscription Disclosures

**Required Text (Apple + Google):**
- "Auto-renews monthly at $29.99/month"
- "Cancel anytime in your platform settings"
- "Payment charged to your account upon confirmation"
- "Subscription renews unless canceled at least 24 hours before end of current period"

---

## 9. PLAYER SUPPORT & REFUNDS

### 9.1 Refund Policy

**Platform-Managed Refunds:**
- Google Play: 48-hour refund window (platform handles)
- Apple App Store: Case-by-case via Apple Support (platform handles)

**Game-Side Response:**
- If refund detected via platform callback → auto-revoke items
- If player contacts CS → refer to platform refund process

### 9.2 Purchase Issues

| Issue | Resolution |
|-------|------------|
| Charged but no items received | CS checks transaction log → manually grant if validated |
| Duplicate charge | Refund via platform, log bug |
| Wrong item granted | CS reverts + grants correct item |

---

## 10. SUMMARY & FAIRNESS COMMITMENT

**Eclipse Reborn is designed to be fun and fair for ALL players:**

✅ **F2P players can experience 100% of gameplay content**  
✅ **IAP is optional and focused on convenience/cosmetics**  
✅ **No paywalls, no stamina/energy systems for core gameplay**  
✅ **Transparent pricing with clear value propositions**  
✅ **Anti-whale measures (purchase limits, power ceiling)**  
✅ **Rewarded ads provide meaningful F2P progression**  

**Our Commitment:**
- No bait-and-switch (game stays fair post-launch)
- Regular balance reviews (adjust if F2P too grindy)
- Community feedback integration (monthly surveys)
- Ethical monetization (no predatory tactics)

---

**Document Version:** 1.0  
**Last Updated:** November 2, 2025  
**Author:** Monetization Team, Eclipse Reborn  

---
