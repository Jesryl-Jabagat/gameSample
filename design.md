# ECLIPSE REBORN: Chronicles of the Lost Sun
## Complete Game Design Document

---

## 1. PITCH

**Eclipse Reborn** is a free-to-play mobile 2D action RPG where players command heroes through fast-paced touch combat, rebuild humanity's lost Sanctuary, and uncover the mystery of the vanished sun. Featuring chibi/HD-2D pixel art, 3–7 minute session loops, real-time combat with skill combos, idle base-building progression, and fair F2P monetization with optional rewarded ads and cosmetic-focused IAP. Players can progress entirely for free while enjoying optional time-savers and exclusive cosmetics.

---

## 2. CORE LOOP

**Micro Loop (3–7 minutes per session):**
1. Launch game → collect offline production rewards
2. Deploy to combat stage (auto-find or manual select)
3. Touch-control hero through 2–4 waves of enemies
4. Defeat boss, earn loot (gear, XP, currency)
5. Return to Sanctuary → upgrade buildings, craft gear, level heroes
6. Optional: watch rewarded ad for production boost or bonus loot
7. Close game → offline production continues

**Macro Loop (daily/weekly):**
- Complete daily quests for currency and materials
- Progress story arcs to unlock new heroes and zones
- Upgrade Sanctuary buildings for passive resource generation
- Participate in limited-time events and Battle Pass seasons
- Collect and level 5+ heroes with different playstyles

**Retention Targets:**
- Day 1: 45% (tutorial completion, first hero unlock)
- Day 7: 25% (base established, 2+ heroes, first IAP window)
- Day 30: 12% (engaged core, Battle Pass purchasers)
- Average session: 5.5 minutes, 3–5 sessions/day for core players

---

## 3. WORLD & LORE

### Setting
A thousand years ago, the Eternal Sun vanished during a cosmic event called **The Eclipse**. The world plunged into twilight, overrun by shadow creatures called **Umbra**. Humanity retreated to the last bastion: **Sanctuary**, a mystical citadel powered by Solar Shards—crystallized fragments of the lost sun. You are the **Lightbringer**, commander of the Solaris Guard, tasked with reclaiming corrupted lands, gathering Solar Shards, and ultimately reigniting the sun.

### Three Main Story Arcs

**Arc 1: Awakening (Chapters 1–10)**
- Tutorial introduces Auron, the first hero, awakening from stasis
- Rebuild Sanctuary's core facilities (Barracks, Workshop, Shard Reactor)
- Investigate nearby corrupted forests; first boss: **Shade Warden Gorath**
- Unlock Lyra (healer) and discover ancient prophecy of Eclipse Reversal

**Arc 2: Factions Unite (Chapters 11–25)**
- Three factions emerge with conflicting visions for humanity's future
- Recruit Ronan (tank) from Iron Covenant, Cira (mage) from Twilight Circle
- Navigate faction politics; choose ally for major battle against Umbra Lord
- Mid-story twist: Eclipse was caused by humanity's ancient weapon, not natural event
- Unlock base PvP arena and crafting advanced gear

**Arc 3: Rekindling (Chapters 26–50+)**
- Journey to the Sunspire, the sun's former resting place
- Recruit Milo (rogue) who knows secret paths through Umbra strongholds
- Face moral choice: reignite sun (destroy Umbra but risk old weapon), or find balance
- Final boss: **Eclipse Sovereign** (multi-phase, requires all 5 heroes strategy)
- Post-game: endless dungeons, seasonal events, hero variants

### Three Factions

**1. Iron Covenant**
- Philosophy: Militaristic order, believes strength and discipline will survive Eclipse
- Leader: Marshal Theron (NPC quest giver)
- Base aesthetic: Bronze/steel fortresses, disciplined soldiers
- Rewards: Tank gear, defensive buffs, heavy weapons

**2. Twilight Circle**
- Philosophy: Mystics who study Umbra magic, seek to coexist with darkness
- Leader: Archon Selene (NPC quest giver)
- Base aesthetic: Floating crystals, arcane runes, purple/blue lighting
- Rewards: Mage gear, elemental skills, mana regeneration items

**3. Solaris Remnant**
- Philosophy: Scholars preserving old-world knowledge, seek to restore the sun
- Leader: Elder Caius (main story NPC, mentor figure)
- Base aesthetic: Libraries, solar panel aesthetics, gold/white colors
- Rewards: Support items, XP boosters, lore unlocks

Players gain reputation with all three, unlocking unique cosmetics and story branches.

---

## 4. FIVE SAMPLE HEROES

### Hero 1: Auron - Solaris Vanguard
- **Role:** Balanced DPS / Starter Hero
- **Weapon:** Solar Greatsword (two-handed)
- **Lore:** First Lightbringer, awakened from 1000-year stasis. Seeks redemption for failing to prevent Eclipse.
- **Playstyle:** Medium range, strong basic attacks, AoE ultimate
- **Stats (Level 1):** HP 500 | ATK 45 | DEF 20 | SPD 100 | CRIT 5%
- **Signature Skill:** **Solar Cleave** (3s CD) - Swing in 180° arc, 180% ATK damage to all enemies hit
- **Ultimate:** **Eclipse Breaker** (15s CD) - Leap and slam ground, 350% ATK AoE, stun 1.5s

### Hero 2: Lyra - Lightweaver
- **Role:** Healer / Support
- **Weapon:** Sacred Staff (channeling)
- **Lore:** Priestess of the Twilight Circle who defected to help Sanctuary. Believes in redemption through light.
- **Playstyle:** Ranged support, healing, buffs
- **Stats (Level 1):** HP 380 | ATK 30 | DEF 15 | SPD 95 | CRIT 3%
- **Signature Skill:** **Healing Radiance** (5s CD) - Heal nearest ally for 120% ATK, +10% DEF for 5s
- **Ultimate:** **Sanctuary Blessing** (20s CD) - AoE heal 80% ATK to all allies, cleanse debuffs

### Hero 3: Ronan - Ironclad Sentinel
- **Role:** Tank / Defender
- **Weapon:** Tower Shield + Mace
- **Lore:** Former Iron Covenant captain. Lost his squad to Umbra, now fights to protect what remains.
- **Playstyle:** Close-range, high HP, taunt mechanics
- **Stats (Level 1):** HP 700 | ATK 35 | DEF 35 | SPD 85 | CRIT 2%
- **Signature Skill:** **Shield Bash** (4s CD) - Charge forward 3m, 120% ATK, stun 1s
- **Ultimate:** **Unbreakable Bulwark** (18s CD) - Taunt all enemies, +50% DEF, reflect 20% damage for 6s

### Hero 4: Cira - Stormcaller
- **Role:** Burst Mage / AoE DPS
- **Weapon:** Elemental Orbs (dual-wield)
- **Lore:** Prodigy from Twilight Circle. Studies Umbra to turn their power against them. Volatile and ambitious.
- **Playstyle:** Long-range, high burst, fragile
- **Stats (Level 1):** HP 350 | ATK 55 | DEF 12 | SPD 90 | CRIT 8%
- **Signature Skill:** **Arcane Missiles** (3.5s CD) - Fire 5 homing projectiles, 60% ATK each
- **Ultimate:** **Meteor Storm** (16s CD) - Call 8 meteors in target area, 200% ATK total, burn 2% HP/s for 4s

### Hero 5: Milo - Shadowstep
- **Role:** Rogue / Single-Target DPS
- **Weapon:** Dual Daggers
- **Lore:** Ex-thief from underground. Knows Umbra-corrupted zones better than anyone. Cynical but loyal.
- **Playstyle:** High mobility, backstab crits, dodge-focused
- **Stats (Level 1):** HP 420 | ATK 50 | DEF 18 | SPD 120 | CRIT 12%
- **Signature Skill:** **Shadowstep** (2.5s CD) - Dash behind target, next attack +100% crit chance
- **Ultimate:** **Blade Dance** (14s CD) - 8 rapid strikes to single target, 150% ATK each, ignore 50% DEF

---

## 5. GAMEPLAY SYSTEMS

### 5.1 Combat System

**Real-Time Touch Controls:**
- Virtual joystick (left thumb) for 8-directional movement
- Basic attack button (auto-attacks when held, or tap for single strike)
- Two skill buttons (right side) with cooldown indicators
- Ultimate button (center-right) charges via dealing/taking damage
- Auto-battle toggle (AI controls hero; can be disabled mid-fight)

**Combat Mechanics:**
- Damage Formula: `Damage = (ATK * SkillMultiplier) * (100 / (100 + DEF)) * CritMultiplier`
- Critical Hit: Base crit chance (5%–15% depending on hero), deals 2x damage
- Status Effects: Burn (2% max HP/s), Stun (disable movement/attacks), Shield (absorb X damage)
- Dodge: Speed-based chance (SPD/10 = dodge %), i-frames during dash
- Combo System: Certain skill sequences increase damage (e.g., Auron Skill → Ultimate = +20%)

**Enemy AI:**
- Basic melee: Chase player, swing when in range
- Ranged: Keep distance, shoot projectiles
- Elite: Use skills on cooldown, call reinforcements
- Boss: Multi-phase HP bars, special mechanics (shield break, AoE warnings)

### 5.2 Stats & Progression

**Core Stats:**
- **HP (Hit Points):** Maximum health
- **ATK (Attack):** Base damage multiplier
- **DEF (Defense):** Damage reduction
- **SPD (Speed):** Movement speed, dodge chance
- **CRIT (Critical):** Critical hit chance %

**Stat Growth per Level:**
- Each hero has unique growth rates (see hero_templates.json)
- Example (Auron): HP +25/lvl, ATK +3.2/lvl, DEF +1.5/lvl, SPD +0.5/lvl, CRIT +0.15%/lvl

**Equipment System:**
- 4 Slots: Weapon, Armor, Accessory, Trinket
- Rarity: Common (white), Uncommon (green), Rare (blue), Epic (purple), Legendary (gold)
- Each piece adds flat stats + percentage bonuses
- Legendary items have unique passive effects (e.g., "10% chance to heal on crit")

### 5.3 XP Curve & Level Table

**XP Formula:** `XP_Required = 100 * (Level^1.8) + 50 * Level`

**Levels 1–20 XP Table:**

| Level | XP Required | Cumulative XP | Est. Stages to Level |
|-------|-------------|---------------|----------------------|
| 1     | 0           | 0             | Start                |
| 2     | 150         | 150           | 1 stage              |
| 3     | 336         | 486           | 2 stages             |
| 4     | 559         | 1,045         | 3 stages             |
| 5     | 820         | 1,865         | 5 stages             |
| 6     | 1,120       | 2,985         | 7 stages             |
| 7     | 1,461       | 4,446         | 10 stages            |
| 8     | 1,843       | 6,289         | 13 stages            |
| 9     | 2,267       | 8,556         | 17 stages            |
| 10    | 2,734       | 11,290        | 22 stages            |
| 11    | 3,245       | 14,535        | 28 stages            |
| 12    | 3,800       | 18,335        | 35 stages            |
| 13    | 4,401       | 22,736        | 43 stages            |
| 14    | 5,049       | 27,785        | 52 stages            |
| 15    | 5,744       | 33,529        | 62 stages            |
| 16    | 6,487       | 40,016        | 74 stages            |
| 17    | 7,279       | 47,295        | 86 stages            |
| 18    | 8,121       | 55,416        | 100 stages           |
| 19    | 9,013       | 64,429        | 115 stages           |
| 20    | 9,957       | 74,386        | 130 stages           |

Average stage rewards 50 XP (cleared in ~3 minutes). Level cap: 50 for launch, expandable.

**Full Level 1-50 table included in game_config.json**

### 5.4 Base Building (Sanctuary)

**Core Buildings:**

**1. Barracks** (Unlocks/upgrades heroes)
- Level 1: Unlock Auron
- Level 3: Unlock Lyra
- Level 5: Unlock Ronan
- Each level: +5% hero base stats
- Upgrade cost scales: 500g → 1,200g → 3,000g...

**2. Workshop** (Craft/upgrade gear)
- Level 1: Craft Common/Uncommon gear
- Level 4: Craft Rare gear
- Level 7: Craft Epic gear
- Level 10: Craft Legendary gear
- Produces 1 crafting material per hour (offline accumulates up to 12 hours)

**3. Shard Reactor** (Generates premium currency)
- Produces Solar Shards (premium currency)
- Base: 1 shard/4 hours (F2P players can earn ~6 shards/day)
- Upgrade: Reduce time and increase output (max: 1 shard/2 hours = 12/day)
- Watch ad bonus: +50% production speed for 1 hour (max 3 ads/day)

**4. Treasury** (Generates gold)
- Base: 200 gold/hour
- Upgrade: Increase production rate and offline cap
- Max offline accumulation: 12 hours (VIP extends to 24 hours)

**5. Training Grounds** (Passive XP for benched heroes)
- Assign up to 3 heroes to passively gain XP
- Base: 10 XP/hour per hero
- Upgrade: Increase XP rate and slots (max 5 heroes)

**Building Upgrade Formula:**
- Cost: `BaseCost * (1.5 ^ (Level - 1))`
- Time: `BaseTime * (1.3 ^ (Level - 1))` (can instant-finish with gems or IAP)
- Production: `BaseProduction * (1.2 ^ (Level - 1))`

### 5.5 Idle & Offline Production

**Offline Rewards:**
- Buildings continue producing for up to 12 hours offline (24h for VIP)
- On return, player gets summary screen showing accumulated resources
- Option to watch ad to double offline rewards (1x/day for free, unlimited for VIP)
- Anti-exploit: Server timestamp validation prevents time manipulation

**Idle Combat (Auto-Battle):**
- Unlocked after completing Chapter 3
- Sweep previously cleared stages for loot without playing
- Costs "Sweep Tickets" (earned 5/day, purchasable, or Battle Pass rewards)
- Instant results, reduced rewards (80% normal loot)

### 5.6 Crafting System

**Materials:**
- Iron Ore (common), Arcane Dust (uncommon), Sunstone (rare), Eclipse Shard (epic)
- Dropped from stages, chests, events, daily quests

**Crafting Process:**
1. Select gear blueprint (unlocked via story progression or shop)
2. Consume materials + gold
3. Instant craft or wait (5 min common → 2 hours legendary)
4. Optional: Use gems to instant-finish

**Gear Enhancement:**
- Spend duplicate gear or universal shards to level up equipment
- Max level = Player Level (prevents over-scaling)
- +10% stats per enhancement level (max +10 levels = +100% stats)

---

## 6. MONETIZATION

### 6.1 Philosophy
Eclipse Reborn is designed to be **fully playable without spending money**. All gameplay content (heroes, stages, story) is accessible to F2P players. Monetization focuses on:
- **Convenience:** Speed up timers, instant crafting, extra resources
- **Cosmetics:** Exclusive skins, mounts, UI themes, emotes
- **Time-savers:** Skip tickets, XP boosters, resource packs

**No Paywalls:** F2P players can reach max level and clear all content; it just takes longer (~3x slower progression).

### 6.2 Rewarded Ads Strategy

**Daily Limits:**
- Free players: 3 ads/day
- VIP players: Unlimited ads (but same rewards)

**Ad Reward Options:**

**1. Resource Pack Ad** (1x/day)
- Reward: 500 gold + 50 crafting materials
- Placement: Main menu button, "Claim Daily Bonus"

**2. Production Boost Ad** (1x/day)
- Reward: +50% production speed for 1 hour (all buildings)
- Placement: Sanctuary screen, "Watch Ad to Boost"

**3. Offline Doubler Ad** (1x/day)
- Reward: 2x offline rewards when returning from offline session
- Placement: Popup when logging in after 2+ hours offline

**Ad Flow:**
1. Player taps ad button
2. Confirmation dialog: "Watch 30s ad for [reward]?"
3. Play ad (Unity Ads SDK, 15–30s video)
4. On complete: Grant reward + show "Reward Claimed!" popup
5. Button disabled until next daily reset (UTC midnight)

**Anti-Exploit:**
- Ads generate server-side signed tokens before granting rewards
- Token expires in 60 seconds
- One reward per ad view (no retry spam)

### 6.3 IAP Products

**Currency Pricing:**
- Solar Shards (premium): $1 USD = 100 shards
- Gold (soft): Earned in-game, not directly purchasable

**IAP Bundles:**

**1. Starter Pack** - $4.99 (One-time purchase, 3x value)
- 1200 Solar Shards ($12 value)
- 3x XP Booster (1 hour each, +50% XP)
- Exclusive "Dawn Voyager" Auron skin (cosmetic only)
- 10x Sweep Tickets

**2. Progression Boost** - $9.99 (One-time purchase)
- Instant Base Upgrade Kit (upgrades all buildings +1 level, max level 5)
- 7-Day XP Buff (+10% XP gain)
- 2000 Solar Shards
- 50,000 Gold

**3. Gem Pack Tiers** (Repeatable)
- **Small Pouch:** $0.99 = 120 shards
- **Medium Sack:** $4.99 = 650 shards (+30 bonus)
- **Large Chest:** $9.99 = 1400 shards (+200 bonus)
- **Epic Hoard:** $19.99 = 3000 shards (+500 bonus)
- **Legendary Vault:** $49.99 = 8000 shards (+1500 bonus)

**4. Solar Blessing Pass** - $29.99/month (Subscription)
- Daily login: 100 shards (3000/month)
- VIP Perks:
  - -20% building upgrade timers
  - 24-hour offline accumulation (vs 12h)
  - Unlimited rewarded ads
  - Exclusive chat frame & title
  - Monthly exclusive mount skin (cosmetic)
- Auto-renews monthly

**5. Legendary Chest** - $49.99 (Weekly offer)
- 1x Guaranteed Legendary Weapon (random)
- 1x Legendary Hero Skin (player choice from 5 options)
- 3000 Solar Shards
- 100,000 Gold

**6. Battle Pass** (30-day season)
- **Free Track:** Small rewards every 5 levels (gold, materials, 1 rare skin at level 30)
- **Premium Track ($9.99):** 
  - Unlocks all 30 tiers immediately (can grind or pay to skip)
  - Rewards: 3 Epic skins, 1 Legendary skin, 2000 shards, 20 Sweep Tickets, exclusive emote
  - Tier skip: 150 shards per tier (encourage grinding, not skipping)

**Shard Spending (In-Game Uses):**
- Instant-finish building upgrade: 50–300 shards (scales with time remaining)
- Instant-craft gear: 20–200 shards
- Revive in combat (continues run): 50 shards
- Gacha pull (cosmetic only, no power): 300 shards/pull, 2700/10-pull
- Energy refill (stamina system): 100 shards = full refill

### 6.4 UX Flow for Purchases

**IAP Flow:**
1. Player taps "Shop" button (persistent UI element)
2. Shop tabs: Top-Up (gem packs), Bundles (curated offers), Battle Pass, Offers (limited-time)
3. Player selects product → confirmation dialog shows contents + price
4. Tap "Purchase" → platform payment dialog (Google Play / App Store)
5. On success: Server validates receipt → grant items → show reward popup
6. On failure: Show error, log for CS review

**Server-Side Validation:**
- Receipt forwarded to Google/Apple verification APIs
- Idempotency token prevents duplicate grants
- Transaction logged in database (player_id, product_id, timestamp, receipt_hash)
- If validation fails, retry 3x, then flag for manual review

**Anti-Duplication:**
- One-time bundles flagged in player profile (can't purchase again)
- Subscription checks active status before allowing renewal
- Refunds auto-revoke items if detected via platform callbacks

### 6.5 F2P vs Paid Progression Balance

**F2P Player (no spending):**
- Reach Level 20 in ~2 weeks (daily 30 min play)
- Unlock all 5 heroes by Chapter 15 (~3 weeks)
- Clear main story (Chapter 50) in ~8–10 weeks
- Earn ~200 shards/month (via Shard Reactor, events, Battle Pass free track)
- Sanctuary max level 10 in ~6 weeks

**Light Spender ($10–20/month):**
- Reach Level 20 in ~1 week (XP boosters + Battle Pass)
- Unlock heroes 50% faster (instant building upgrades)
- Clear main story in ~5 weeks
- Sanctuary max level 10 in ~3 weeks

**Whale ($100+/month):**
- Reach Level 20 in 3 days (constant XP boosters + instant crafting)
- Unlock all heroes in week 1
- Clear main story in 2–3 weeks
- Sanctuary maxed in 1 week
- Focus shifts to cosmetic collecting, PvP ranking, events

**Key Balance Notes:**
- Power ceiling is same for F2P and paid (level 50, max gear)
- Paid players reach endgame faster but don't have exclusive power
- PvP (if added) matchmakes by power level, not spending
- Events designed to reward skill + time, not just money

### 6.6 Anti-Exploit Measures

**Time Manipulation:**
- All timers validated against server time
- Offline rewards capped at 12/24 hours (can't fake 100-day offline)
- Building upgrades, crafting store server-side completion timestamps

**Purchase Exploits:**
- Receipt validation with Google/Apple servers
- Idempotency tokens prevent replay attacks
- One-time bundles stored server-side (can't clear cache to rebuy)

**Ad Fraud:**
- Ad SDK (Unity Ads) provides fraud detection
- Reward tokens signed with server secret, expire in 60s
- Daily ad limits enforced server-side (not client)

**Account Sharing:**
- Sessions invalidated if login from new device (requires re-auth)
- Suspicious activity (too many purchases, refunds) flags account for review

---

## 7. UX FLOWS

### 7.1 First-Time User Experience (FTUE)

**Tutorial (3 minutes):**
1. Cinematic: Eclipse event, Auron awakens
2. Forced combat tutorial (tap to move, tap to attack)
3. Win tutorial fight → level up popup → explain stats
4. Return to Sanctuary → guided building upgrade (Barracks to level 2)
5. "You can now unlock Lyra! Complete Chapter 2 to recruit her."
6. End tutorial → grant 500 gold, 200 shards, unlock auto-battle

**Onboarding Rewards:**
- Day 1: 300 shards, 1x Epic weapon
- Day 2: 10 Sweep Tickets
- Day 3: 1x Rare hero skin (cosmetic)
- Day 7: 500 shards, 1x Legendary weapon chest (choice of 3)

### 7.2 Daily Session Flow

**Login (15 seconds):**
- Splash screen → load save data
- Offline rewards popup (if offline >1 hour): "You earned 2,400 gold while away! [Claim] [Watch Ad to Double]"
- Daily quest popup: "3 new quests available!"
- Event banner (if active): "Winter Festival ends in 2 days!"

**Main Hub (Sanctuary Screen):**
- Center: Hero character standing in base
- Top bar: Gold, Shards, Player Level, Settings
- Bottom nav: [Battle] [Sanctuary] [Heroes] [Shop] [Quests]
- Persistent buttons: Daily quests (icon with "3"), Shop (icon), Ad offers (if available)

**Combat Session:**
- Select stage → confirm team (1 hero for now, multi-hero later)
- Loading tip: "Use Auron's ultimate to stun groups!"
- Combat → HUD overlay (HP bar, skill buttons, pause)
- Victory screen: Loot summary, XP gained, level up animation (if applicable)
- Return to hub or continue to next stage

**End Session:**
- Game auto-saves every 30 seconds + on major events (level up, purchase, building upgrade)
- Close app → offline production starts

---

## 8. TECHNICAL CONSIDERATIONS

### 8.1 Mobile Optimization

**Performance Targets:**
- 60 FPS on mid-range devices (2019+ Android, iPhone 8+)
- 30 FPS minimum on low-end (2017 devices)
- Memory budget: 300 MB RAM for game, 100 MB for assets

**Optimization Techniques:**
- Object pooling for projectiles, damage numbers, VFX (reuse instead of Instantiate/Destroy)
- Sprite atlasing (combine all UI into 2–3 atlases to reduce draw calls)
- Audio: Use compressed Vorbis for music, short wav for SFX
- LOD: Simplify animations at distance (reduce frame rate for off-screen enemies)
- Async loading: Load stages in background during previous stage completion screen

### 8.2 Platform-Specific Notes

**Android:**
- Target API 33+ (Google Play requirement)
- Use ARM64 builds (required for 2024+)
- Test on Samsung, Xiaomi, Google Pixel (cover 70% of market)
- Handle notch/cutout variations

**iOS:**
- Target iOS 13+ (covers 95% of devices)
- Handle Safe Area for iPhone X+ (notch)
- Use Metal graphics API (better performance than OpenGL)
- Request ATT (App Tracking Transparency) for ads

### 8.3 Server Architecture (Pseudocode in server/pseudo_api.txt)

**Backend Needs:**
- Player data persistence (PostgreSQL or Firebase)
- Receipt validation endpoints (Google, Apple)
- Anti-cheat validation (timestamp checks, hash verification)
- Event management (seasonal events, limited offers)
- Leaderboards (if PvP added)

---

## 9. ART & AUDIO DIRECTION

### 9.1 Art Style

**Visual Language:**
- Chibi proportions (2:1 head-to-body ratio) for heroes
- HD-2D pixel art for environments (16-bit style with modern lighting)
- Hand-painted texture feel (avoid pure pixel grid, add brush strokes)
- Color palette: Warm golds/oranges (Sanctuary), cool blues/purples (Umbra zones)

**Animation Requirements:**
- Hero idle: 4 frames, 0.5s loop
- Hero walk: 6 frames, 0.6s loop
- Hero attack: 8 frames, 0.4s (frame 4 = hit frame for animation event)
- Hero skill: 10–12 frames depending on complexity
- VFX: 8–16 frames for explosions, projectiles (alpha fade out)

**UI Style:**
- Clean, modern UI with subtle fantasy flourishes
- Gold trim for premium elements
- Large touch targets (minimum 64x64 dp for buttons)
- High contrast text (white on dark or dark on light, no mid-tone grays on mid-tone backgrounds)

### 9.2 Audio Direction

**Music:**
- Main theme: Orchestral with synth elements, hopeful but melancholic (2:30 loop)
- Combat: Upbeat, percussion-heavy (1:30 loop, seamless)
- Sanctuary: Calm, acoustic guitar + strings (3:00 loop)
- Boss: Intense, choir + drums (2:00 loop)

**SFX:**
- Sword swing: Sharp whoosh + metal ring
- Magic cast: Crystalline chime + bass thump
- UI button: Soft click
- Reward claim: Triumphant jingle (0.5s)
- Damage numbers: Quick pop/hit

**Voice (optional for launch):**
- Hero select: One-liner per hero ("For the light!" - Auron)
- Ultimate activation: Hero shout
- Victory: "We did it!" etc.

---

## 10. LIVE OPS & POST-LAUNCH

### 10.1 Content Roadmap (First 6 Months)

**Month 1 (Launch):**
- 50 story chapters, 5 heroes, base building
- Daily quests, ad integration, IAP shop
- Battle Pass Season 1: "Awakening"

**Month 2:**
- New hero: **Kael, the Frost Warden** (ice tank)
- 10 new chapters (51–60)
- Limited event: "Frozen Wastes Invasion" (earn exclusive frost weapon)
- QoL: Auto-battle speed toggle, skip cutscenes

**Month 3:**
- PvP Arena (async, AI-controlled opponent teams)
- Leaderboards with weekly rewards
- Battle Pass Season 2: "Frostbound"

**Month 4:**
- New hero: **Zara, the Emberkin** (fire DPS)
- Guild system (social, co-op raids unlocked month 5)
- 10 new chapters (61–70)

**Month 5:**
- Co-op raids (3-player, weekly boss)
- Guild vs Guild events
- Battle Pass Season 3: "Emberfall"

**Month 6:**
- Endgame: Endless Tower (100 floors, weekly resets, leaderboards)
- Hero Ascension system (level 50 → prestige for new abilities)
- Arc 4 story teaser

### 10.2 Seasonal Events (Every 4–6 Weeks)

**Event Structure:**
- 2-week duration
- Unique stages with themed enemies
- Event currency earned from stages
- Event shop: Limited skins, mounts, materials
- Leaderboard with top 100 rewards

**Example Event: "Shadow Moon Festival"**
- Lore: Umbra celebrate their own lunar event, strange gifts appear
- Stages: Night-themed battles with 1.5x enemy count
- Rewards: Moonlight hero skins, lunar mount, exclusive emote
- Leaderboard: Top 10 get legendary "Moonblade" weapon

### 10.3 KPI Targets

**Metrics to Track:**
- DAU/MAU (target: 0.25 = healthy engagement)
- ARPU (Average Revenue Per User): Target $2–5/month
- Retention: D1 45%, D7 25%, D30 12%
- Session length: 5–7 minutes average
- Ad revenue: 30–40% of total revenue (rest from IAP)
- Conversion rate (free to paid): 3–5%

**A/B Testing:**
- Ad placement timing (immediate popup vs button)
- Tutorial length (3 min vs 5 min)
- Starter pack pricing ($4.99 vs $2.99)
- Battle Pass track difficulty (grind time)

---

## 11. COMPETITIVE ANALYSIS

**Similar Games:**
- **AFK Arena:** Idle progression, hero collecting (Eclipse Reborn is more action-focused)
- **Tap Titans / Endless Frontier:** Incremental, less story (Eclipse has narrative)
- **Dragalia Lost:** Action combat (Eclipse has simpler controls for mobile)
- **Genshin Impact:** High production RPG (Eclipse is lower budget, faster sessions)

**Eclipse Reborn's Unique Positioning:**
- Faster sessions than Genshin (3–7 min vs 20–40 min)
- More active combat than AFK Arena (touch controls vs auto)
- Story-driven with episodic arcs (not just endless grind)
- Fair F2P (no stamina/energy system for main gameplay)
- No gacha for heroes (all unlockable via story)

---

## 12. SUCCESS METRICS

**Launch Goals (Month 1):**
- 100,000 downloads (organic + paid UA)
- 45% D1 retention, 25% D7 retention
- $50,000 revenue (mix of IAP and ads)
- 4.2+ app store rating

**6-Month Goals:**
- 1,000,000 downloads
- $500,000 cumulative revenue
- 30,000 DAU
- Top 50 in Action RPG category (US App Store)

**Longterm Vision (Year 1+):**
- Expand to 100+ heroes (seasonal releases)
- Cross-platform (mobile + PC Steam release)
- Esports potential (PvP tournaments with cash prizes)
- Merchandise (plushies, art books)

---

## APPENDIX: DESIGN PILLARS

1. **Accessible:** Anyone can pick up and play, no complex tutorials
2. **Respectful:** No predatory monetization, fair F2P progression
3. **Rewarding:** Daily login, offline rewards, constant sense of progress
4. **Stylish:** Cohesive art direction, memorable heroes, impactful VFX
5. **Expandable:** Designed for years of content updates, events, heroes

---

**Document Version:** 1.0  
**Last Updated:** November 2, 2025  
**Author:** Senior Game Designer, Eclipse Reborn Team  

---

**END OF DESIGN DOCUMENT**
