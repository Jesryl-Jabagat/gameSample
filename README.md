# Eclipse Reborn: Chronicles of the Lost Sun
### Mobile 2D Action RPG - Complete Development Bundle

---

## PROJECT OVERVIEW

**Eclipse Reborn** is a free-to-play mobile 2D action RPG designed for Android and iOS. Players command heroes through fast-paced touch combat, rebuild humanity's lost Sanctuary, and uncover the mystery of the vanished sun. Featuring chibi/HD-2D pixel art, 3–7 minute session loops, real-time combat with skill combos, idle base-building progression, and fair F2P monetization with optional rewarded ads and cosmetic-focused IAP.

**Genre:** Action RPG / Base-Building / Idle Game  
**Platform:** Android (API 24+), iOS (13+)  
**Target Audience:** Casual to mid-core mobile gamers (ages 10+)  
**Monetization:** F2P with IAP and Rewarded Ads  
**Session Length:** 3-7 minutes (mobile-optimized)  

---

## WHAT'S INCLUDED IN THIS BUNDLE

This repository contains a complete development bundle ready for a small team to assemble a playable prototype in 2-4 sprints:

### 📄 Documentation

1. **`design.md`** - Complete game design document (150+ pages)
   - World lore, story arcs, factions, 5 heroes
   - Combat systems, XP progression (levels 1-50), base-building
   - Monetization strategy (rewarded ads, IAP, Battle Pass)
   - Session loops, retention targets, KPIs

2. **`monetization/monetization_plan.md`** - Detailed monetization breakdown
   - Exact IAP pricing ($0.99 - $49.99 tiers)
   - Rewarded ad limits and rewards
   - F2P vs paid progression balance
   - Anti-exploit measures and fairness commitments

3. **`server/pseudo_api.txt`** - Backend server pseudocode
   - IAP receipt validation (Google Play, App Store)
   - Rewarded ad token system
   - Cloud save sync
   - Offline rewards calculation
   - Security best practices

4. **`playable_demo/README.md`** - Unity assembly instructions
   - Step-by-step scene setup
   - Animator configuration
   - Mobile build guide
   - Testing checklist

5. **`ui/mockups/`** - UI layout documentation
   - `mobile_hud_layout.txt` - Combat HUD with joystick, skills, HP/XP bars
   - `base_screen_layout.txt` - Sanctuary building management screen
   - `shop_layout.txt` - Shop UI with tabs (Top-Up, Bundles, Battle Pass, Offers, VIP)

### 📊 Data & Configuration

6. **`data/game_config.json`** - Core numeric tuning
   - XP formula and level table (1-50)
   - Damage/defense formulas, crit multipliers
   - Building costs, production rates, growth factors
   - Currency definitions (Gold, Solar Shards, crafting materials)

7. **`data/hero_templates.json`** - 5 hero definitions
   - Auron (Balanced DPS), Lyra (Healer), Ronan (Tank), Cira (Burst Mage), Milo (Rogue)
   - Stats per level, skills with multipliers/cooldowns, animation frame counts

8. **`assets/asset_list.csv`** - Complete asset inventory (150+ entries)
   - Hero sprites (idle, walk, attack, skills, death)
   - Enemy sprites (3 types + 2 bosses)
   - VFX (10 effects), SFX (30 sounds), UI (40 elements)
   - Specifications: resolution, frame counts, pivot points, layers

### 💻 Unity C# Scripts (Production-Ready)

9. **`unity_scripts/PlayerController.cs`** - Touch-based hero control
   - Virtual joystick movement (8-directional)
   - Dash mechanic with i-frames
   - Basic attack and skill input mapping
   - Animation parameter management
   - Mobile-optimized with object pooling

10. **`unity_scripts/CombatSystem.cs`** - Damage calculation and status effects
    - Deterministic damage formula (ATK, DEF, crit)
    - Status effects (Burn, Stun, Shield, Buffs)
    - Object pooling for damage numbers (prevents GC spikes)
    - DOT (Damage Over Time) ticking system

11. **`unity_scripts/XPSystem.cs`** - Leveling and stat allocation
    - XP table loader (from game_config.json)
    - Multi-level-up handling
    - Hero stat growth per level
    - Progress tracking for UI

12. **`unity_scripts/BaseManager.cs`** - Base-building and offline production
    - Building upgrade system with timers
    - Offline reward calculation (capped at 12/24 hours)
    - Timestamp validation (anti-time-manipulation)
    - IAP instant-finish hooks
    - Ad-based production boost (+50% for 1 hour)

13. **`unity_scripts/ShopManager.cs`** - IAP and rewarded ads integration
    - Unity IAP integration (Google Play, App Store)
    - Server-side receipt validation stubs
    - Rewarded ad flow (Unity Ads SDK)
    - Daily ad limit enforcement (3/day free, unlimited VIP)
    - One-time purchase tracking
    - Anti-duplication checks

14. **`unity_scripts/SaveLoadManager.cs`** - JSON save/load with cloud sync
    - Local JSON save (player data, heroes, buildings, inventory)
    - Offline production reconciliation
    - Server sync pseudocode
    - Auto-save system (every 30s + on major events)

15. **`unity_scripts/AnimatorSetup.txt`** - Animator controller documentation
    - State machine diagram
    - Parameter definitions (Speed, Attack, Skill1, Skill2, Dash, Hit, Death)
    - Animation event setup (OnAttackHit, OnSkill1Hit, etc.)
    - Frame-by-frame timings for all 5 heroes

---

## QUICK START GUIDE

### For Developers

1. **Clone or download this repository**
2. **Open Unity 2021.3 LTS or newer**
3. **Follow instructions in `/playable_demo/README.md`**
   - Import JSON files to `Assets/Resources/data/`
   - Copy C# scripts to `Assets/Scripts/`
   - Set up combat demo scene
   - Build for Android/iOS
4. **Test on device** (Unity Remote or physical device)

### For Designers

1. **Read `/design.md`** for full game vision
2. **Review `/monetization/monetization_plan.md`** for monetization strategy
3. **Check `/ui/mockups/`** for UI layout references
4. **Reference `/data/game_config.json`** for numeric balance

### For Artists

1. **Open `/assets/asset_list.csv`** for complete asset list
2. **Create sprites** matching specifications (resolution, frames, pivot)
3. **Export sprite sheets** to `Assets/Sprites/`
4. **Follow layer naming** in CSV (Characters, Enemies, VFX, UI)

### For Backend Engineers

1. **Read `/server/pseudo_api.txt`** for API pseudocode
2. **Set up Node.js/Python backend** with PostgreSQL database
3. **Implement receipt validation** (Google Play, App Store APIs)
4. **Deploy to AWS/Google Cloud** with HTTPS/TLS

---

## DEVELOPMENT ROADMAP (2-4 Sprints)

### Sprint 1 (Weeks 1-2): Core Mechanics

**Developer Tasks:**
- [ ] Set up Unity project with folder structure
- [ ] Import all C# scripts and configure Animator
- [ ] Implement PlayerController with virtual joystick
- [ ] Create combat demo scene (1 hero, 1 enemy)
- [ ] Test damage calculation and XP leveling
- [ ] Build APK/IPA and test on device

**Artist Tasks:**
- [ ] Create Auron sprite sheet (idle, walk, attack, 2 skills)
- [ ] Create basic enemy sprite sheet
- [ ] Design 5 VFX effects (sword slash, magic, heal)
- [ ] Create UI mockups for HUD (joystick, buttons, bars)

**QA Tasks:**
- [ ] Test touch controls on 3 device models
- [ ] Verify combat formula accuracy (damage, crit, defense)
- [ ] Check UI scaling on different screen sizes

---

### Sprint 2 (Weeks 3-4): Base-Building & Economy

**Developer Tasks:**
- [ ] Implement BaseManager with building upgrades
- [ ] Create Sanctuary scene with 5 buildings
- [ ] Integrate offline production with timestamp validation
- [ ] Implement SaveLoadManager with JSON persistence
- [ ] Add shop UI with IAP stubs

**Artist Tasks:**
- [ ] Create Sanctuary background art (parallax layers)
- [ ] Design 5 building sprites (Barracks, Workshop, Reactor, Treasury, Training)
- [ ] Create shop UI panels and buttons
- [ ] Design currency icons (Gold, Solar Shards)

**QA Tasks:**
- [ ] Test building upgrade timers
- [ ] Verify offline rewards calculation (cap at 12 hours)
- [ ] Test save/load persistence across sessions

---

### Sprint 3 (Weeks 5-6): All Heroes & Story

**Developer Tasks:**
- [ ] Implement 4 additional heroes (Lyra, Ronan, Cira, Milo)
- [ ] Create 10 story stages (Chapters 1-10)
- [ ] Add stage select UI with progression
- [ ] Implement hero switching system
- [ ] Add tutorial (first-time user experience)

**Artist Tasks:**
- [ ] Create sprite sheets for Lyra, Ronan, Cira, Milo
- [ ] Design 10 stage backgrounds (forest, caves, ruins)
- [ ] Create hero portrait art for UI
- [ ] Design level-up and victory screen VFX

**Audio Tasks:**
- [ ] Compose main theme (2:30 loop, orchestral)
- [ ] Create combat music (1:30 loop, upbeat)
- [ ] Record 30 SFX (sword, magic, UI clicks)
- [ ] Add voice lines for hero selection (optional)

**QA Tasks:**
- [ ] Playtest all 5 heroes for balance
- [ ] Complete story Chapters 1-10 as F2P player
- [ ] Test hero unlock progression

---

### Sprint 4 (Weeks 7-8): Monetization & Polish

**Developer Tasks:**
- [ ] Integrate Unity IAP (Google Play, App Store)
- [ ] Implement rewarded ads (Unity Ads SDK)
- [ ] Set up backend server (receipt validation, cloud save)
- [ ] Add Battle Pass UI and progression
- [ ] Implement VIP subscription benefits
- [ ] Final optimization pass (object pooling, atlasing)

**Artist Tasks:**
- [ ] Create shop product images (gem packs, bundles)
- [ ] Design Battle Pass track UI
- [ ] Create exclusive skins (Dawn Voyager Auron, VIP skins)
- [ ] Polish UI animations and transitions

**Backend Tasks:**
- [ ] Deploy server to production (AWS/Google Cloud)
- [ ] Configure IAP products in Google Play Console & App Store Connect
- [ ] Set up analytics (Unity Analytics, Firebase)
- [ ] Implement anti-cheat measures (timestamp validation)

**QA Tasks:**
- [ ] Test IAP purchases in sandbox (Google, Apple)
- [ ] Verify rewarded ads grant correct rewards
- [ ] Test F2P progression (reach Level 20 in 14 days)
- [ ] Performance testing on low-end devices (60 FPS target)
- [ ] Security testing (time manipulation, receipt tampering)

---

## TEAM STRUCTURE (RECOMMENDED)

**4-Person Core Team:**
- 1x Unity Developer (C# scripting, systems integration)
- 1x 2D Artist (character sprites, UI, VFX)
- 1x Game Designer (balance, progression, monetization)
- 1x QA Tester (device testing, bug reporting)

**Optional Extended Team:**
- 1x Backend Engineer (server, IAP validation, cloud save)
- 1x Audio Designer (music, SFX, voice)
- 1x Marketing (ASO, ads, community)

**Total Estimated Effort:** 320-640 hours (2-4 sprints @ 40 hours/week per person)

---

## TECHNICAL REQUIREMENTS

### Unity Version
- **Minimum:** Unity 2021.3 LTS
- **Recommended:** Unity 2022.3 LTS (latest stable)

### Unity Packages
- Unity UI (com.unity.ugui)
- 2D Sprite (com.unity.2d.sprite)
- Unity IAP (com.unity.purchasing)
- Unity Ads (com.unity.ads)
- TextMeshPro (com.unity.textmeshpro)
- (Optional) Unity Analytics, Firebase SDK

### Platform SDKs
- **Android:** Android SDK API 24+ (Android 7.0+), NDK r21+
- **iOS:** Xcode 13+, iOS SDK 13+
- **Build:** IL2CPP (required for iOS, recommended for Android)

### Device Requirements
- **Android:** 2GB RAM, OpenGL ES 3.0+, ARM64
- **iOS:** iPhone 8+ / iPad 5+, iOS 13+

### Performance Targets
- **High-end devices:** 60 FPS (iPhone 13+, Samsung Galaxy S21+)
- **Mid-range:** 60 FPS (iPhone 11, Google Pixel 5)
- **Low-end:** 30 FPS minimum (2019 budget phones)

---

## MONETIZATION SUMMARY

### Revenue Streams

| Source | Target % | Monthly Revenue (10K MAU) |
|--------|----------|---------------------------|
| Gem Packs (Consumable) | 31% | $3,750 |
| Battle Pass (Seasonal) | 33% | $3,996 |
| VIP Subscription | 19% | $2,249 |
| One-Time Bundles | 4% | $469 |
| Legendary Chests | 5% | $600 |
| Rewarded Ads | 11% | $1,350 |
| **TOTAL** | **100%** | **$12,414** |

**ARPU Target:** $1.24/month (healthy for F2P mobile)

### Fairness Commitments
✅ **100% of gameplay content accessible for free**  
✅ **No pay-to-win mechanics**  
✅ **Transparent pricing (no hidden costs)**  
✅ **F2P can reach max level (Level 50)**  
✅ **Rewarded ads provide meaningful progression**  

See `/monetization/monetization_plan.md` for full breakdown.

---

## FILE STRUCTURE

```
eclipse-reborn/
├── README.md                          # This file
├── design.md                          # Complete game design document
├── data/
│   ├── game_config.json               # Core numeric configuration
│   └── hero_templates.json            # 5 hero definitions
├── assets/
│   └── asset_list.csv                 # Complete asset inventory (150+ entries)
├── unity_scripts/
│   ├── PlayerController.cs            # Touch movement, dash, skills
│   ├── CombatSystem.cs                # Damage calc, status effects
│   ├── XPSystem.cs                    # Leveling, stat allocation
│   ├── BaseManager.cs                 # Building upgrades, offline production
│   ├── ShopManager.cs                 # IAP, rewarded ads
│   ├── SaveLoadManager.cs             # JSON save/load, cloud sync
│   └── AnimatorSetup.txt              # Animator parameters & events
├── ui/mockups/
│   ├── mobile_hud_layout.txt          # Combat HUD layout
│   ├── base_screen_layout.txt         # Sanctuary UI layout
│   └── shop_layout.txt                # Shop UI with tabs
├── monetization/
│   └── monetization_plan.md           # Detailed monetization strategy
├── server/
│   └── pseudo_api.txt                 # Backend API pseudocode
└── playable_demo/
    └── README.md                      # Unity assembly guide
```

---

## SUPPORT & CONTACT

**For Questions:**
- Review documentation in this repository first
- Check Unity forums for engine-specific issues
- Refer to Unity IAP/Ads SDK documentation for integration help

**For Bug Reports:**
- Open an issue with detailed steps to reproduce
- Include Unity version, device model, OS version
- Attach logs if applicable (Player.log)

---

## LICENSE

**This development bundle is provided as a reference implementation.**

You are free to:
- Use this code in your own commercial or non-commercial projects
- Modify and adapt the code to fit your needs
- Use the design documents as inspiration for your own games

**Attribution appreciated but not required.**

**Third-Party Assets:**
- Unity IAP, Unity Ads: Unity Technologies license
- Platform SDKs: Google Play, App Store licenses apply

---

## VERSION HISTORY

**v1.0.0** (November 2, 2025)
- Initial release
- Complete design documentation
- 6 production-ready Unity C# scripts
- JSON configuration files (XP, combat, heroes)
- Asset inventory (150+ entries)
- Monetization plan with exact pricing
- Server API pseudocode
- UI mockups (HUD, Sanctuary, Shop)
- Unity assembly guide

---

## CREDITS

**Design:** Senior Game Designer  
**Code:** Unity C# Engineer  
**Documentation:** Technical Writer  
**Monetization:** F2P Economy Specialist  

**Special Thanks:**
- Unity Technologies (Unity Engine)
- Mobile game development community
- Players who support fair F2P games

---

**🌟 Ready to build Eclipse Reborn? Start with `/playable_demo/README.md`! 🌟**

---

**Last Updated:** November 2, 2025  
**Bundle Version:** 1.0.0  
**License:** Open for commercial and non-commercial use
