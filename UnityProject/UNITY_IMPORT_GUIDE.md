# Eclipse Reborn - Unity Import Guide

## 📦 Quick Start: Import This Project Into Unity

This folder contains a **complete Unity-ready project structure** for Eclipse Reborn. Follow these steps to get started.

---

## ✅ Prerequisites

Before you begin, ensure you have:

1. **Unity 2021.3 LTS** or newer installed
   - Download from: https://unity.com/download
   - Recommended: Unity 2022.3 LTS (latest stable)

2. **Required Unity Packages** (install via Package Manager):
   - Unity UI (com.unity.ugui)
   - 2D Sprite (com.unity.2d.sprite)
   - TextMeshPro (com.unity.textmeshpro)
   - Unity IAP (com.unity.purchasing) - for monetization
   - Unity Ads (com.unity.ads) - for rewarded ads

3. **Platform SDKs** (for building):
   - **Android**: Android SDK API 24+ (Android Studio recommended)
   - **iOS**: Xcode 13+ (macOS only)

---

## 🚀 Step-by-Step Import Process

### Step 1: Create a New Unity Project

1. Open **Unity Hub**
2. Click **"New Project"**
3. Select **"2D Core"** template
4. Name it: `Eclipse Reborn`
5. Choose a location on your computer
6. Click **"Create Project"**

### Step 2: Import This Folder Structure

**Option A: Copy-Paste Method (Recommended)**

1. Close Unity Editor (important!)
2. Navigate to your Unity project folder on your computer
3. **Delete** the default `Assets` folder in your Unity project
4. **Copy** the entire `UnityProject/Assets` folder from this package
5. **Paste** it into your Unity project root directory
6. Reopen Unity - it will automatically import all assets

**Option B: Drag-and-Drop Method**

1. Keep Unity Editor open
2. Open your file explorer/finder
3. Navigate to `UnityProject/Assets` from this package
4. Drag all folders (`Scripts`, `Resources`, `Sprites`, etc.) into Unity's Project window
5. Wait for Unity to import everything

### Step 3: Configure Project Settings

1. **Player Settings** (Edit → Project Settings → Player):
   - Company Name: `Your Studio Name`
   - Product Name: `Eclipse Reborn`
   - Version: `1.0.0`
   - Default Icon: Use `Sprites/UI/Game_UI_elements_pack_*.png`

2. **Quality Settings** (Edit → Project Settings → Quality):
   - Remove unnecessary quality levels (keep Medium and High)
   - Set target frame rate: 60 FPS

3. **Graphics Settings**:
   - Scriptable Render Pipeline: None (using built-in)
   - Color Space: Linear (for better lighting)

### Step 4: Set Up Sprite Import Settings

1. In Unity Project window, navigate to `Assets/Sprites/Characters/`
2. Select **all character sprite sheets**
3. In Inspector:
   - **Texture Type**: `Sprite (2D and UI)`
   - **Sprite Mode**: `Multiple` (for animation sheets)
   - **Pixels Per Unit**: `100`
   - **Filter Mode**: `Point (no filter)` for pixel art OR `Bilinear` for smooth
   - **Compression**: `None` for best quality
   - Click **"Apply"**

4. Click **"Sprite Editor"** button
5. Use **"Slice"** → **"Grid By Cell Count"** to split animation frames
   - Refer to `Assets/asset_list.csv` for frame counts per animation

6. Repeat for Enemies and Environment sprites

### Step 5: Create Game Scenes

#### Combat Scene

1. **Create Scene**: File → New Scene → Save as `Combat.unity` in `Assets/Scenes/`
2. **Set up Camera**:
   - Main Camera → Projection: Orthographic
   - Size: 5 (adjust for mobile aspect ratio)
   - Background: Dark blue (#16213e)

3. **Create Player GameObject**:
   - Right-click in Hierarchy → Create Empty → Name: `Player`
   - Add Component → **PlayerController** script (from Assets/Scripts/)
   - Add Component → Animator
   - Drag character sprites to create Animator Controller

4. **Create Enemy GameObject**:
   - Create Empty → Name: `Enemy`
   - Add Component → Animator
   - Position to the right of player

5. **Add Background**:
   - Create Sprite → Assign `Combat_forest_background`
   - Set Sorting Layer: Background (-10)

6. **Create UI Canvas**:
   - Right-click → UI → Canvas
   - Add virtual joystick, skill buttons, HP/XP bars
   - Reference: `ui/mockups/mobile_hud_layout.txt`

#### Sanctuary (Base) Scene

1. **Create Scene**: File → New Scene → Save as `Sanctuary.unity`
2. **Add Background**: `Sanctuary_base_background`
3. **Create Building GameObjects** (5 buildings):
   - Use UI buttons for each building
   - Attach **BaseManager** script to a manager GameObject
4. **Reference**: `ui/mockups/base_screen_layout.txt`

### Step 6: Set Up Animators

1. **Create Animator Controller**: Assets → Create → Animator Controller → `PlayerAnimator`
2. **Open Animator Window**: Window → Animation → Animator
3. **Follow Setup Instructions**: `Assets/Scripts/AnimatorSetup.txt`
4. **Create States**:
   - Idle, Walk, Attack, Skill1, Skill2, Dash, Hit, Death
5. **Add Parameters**:
   - `Speed` (Float)
   - `Attack` (Trigger)
   - `Skill1` (Trigger)
   - `Skill2` (Trigger)
   - `Dash` (Trigger)
   - `Hit` (Trigger)
   - `Death` (Trigger)

6. **Create Animation Clips**:
   - Select sprite frames from sprite sheets
   - Drag into Animation window
   - Set frame rate: 12-15 FPS for smooth animation

7. Repeat for each hero and enemy

### Step 7: Attach Scripts to GameObjects

1. **Player GameObject**:
   - Add `PlayerController.cs`
   - Add `XPSystem.cs`
   - Add `CombatSystem.cs` component

2. **Create Game Manager** (Empty GameObject):
   - Add `SaveLoadManager.cs`
   - Add `ShopManager.cs`
   - Add `BaseManager.cs`

3. **Configure Script References**:
   - Assign public fields in Inspector
   - Link UI elements to scripts

### Step 8: Configure Game Data

1. **Verify JSON Files** are in `Assets/Resources/Data/`:
   - ✅ `game_config.json` (XP tables, combat formulas)
   - ✅ `hero_templates.json` (5 hero definitions)

2. **Test Data Loading**:
   - Scripts use `Resources.Load<TextAsset>("Data/game_config")`
   - Verify paths are correct

### Step 9: Build Settings

1. **Add Scenes to Build** (File → Build Settings):
   - Add `Combat.unity`
   - Add `Sanctuary.unity`
   - Set Combat as Scene 0 (first to load)

2. **Platform-Specific Setup**:

   **For Android:**
   - Switch Platform → Android
   - Bundle Identifier: `com.yourstudio.eclipsereborn`
   - Minimum API Level: 24 (Android 7.0)
   - Target API Level: 33 (Android 13)
   - IL2CPP (required for ARM64)

   **For iOS:**
   - Switch Platform → iOS
   - Bundle Identifier: `com.yourstudio.eclipsereborn`
   - Target SDK: iOS 13.0
   - Architecture: ARM64

### Step 10: Test the Game

1. **Play Mode Testing**:
   - Click Play button
   - Test player movement (use WASD for testing, will be touch on mobile)
   - Test combat system
   - Test XP gain and leveling

2. **Mobile Testing**:
   - Use Unity Remote app on your device
   - Or build APK/IPA and deploy to device

3. **Performance Testing**:
   - Check frame rate (should be 60 FPS)
   - Monitor memory usage
   - Test on low-end devices if possible

---

## 📁 Project Structure Overview

```
UnityProject/
├── Assets/
│   ├── Scripts/              # All C# game logic scripts
│   │   ├── PlayerController.cs
│   │   ├── CombatSystem.cs
│   │   ├── XPSystem.cs
│   │   ├── BaseManager.cs
│   │   ├── ShopManager.cs
│   │   ├── SaveLoadManager.cs
│   │   ├── DataClasses.cs
│   │   └── AnimatorSetup.txt
│   │
│   ├── Resources/
│   │   └── Data/             # JSON configuration files
│   │       ├── game_config.json
│   │       └── hero_templates.json
│   │
│   ├── Sprites/              # All visual assets
│   │   ├── Characters/       # 5 heroes (Auron, Lyra, Ronan, Cira, Milo)
│   │   ├── Enemies/          # Shade, Gorath boss
│   │   ├── Environment/      # Backgrounds
│   │   └── UI/               # UI elements
│   │
│   ├── Prefabs/              # Empty (create your own)
│   ├── Scenes/               # Empty (create Combat and Sanctuary scenes)
│   └── asset_list.csv        # Complete asset inventory
│
├── ui/                       # UI mockups for reference
├── monetization/             # Monetization plan
├── server/                   # Backend API pseudocode
├── playable_demo/            # Additional assembly guide
├── design.md                 # Complete game design (150+ pages)
├── README.md                 # Project overview
└── UNITY_IMPORT_GUIDE.md     # This file
```

---

## 🎮 Hero Characters Included

All heroes have complete sprite sheets with animations:

1. **Auron - Solaris Vanguard** (DPS)
   - HP: 500, ATK: 45, DEF: 20, SPD: 100
   - Weapon: Greatsword
   - Skill: Solar Cleave

2. **Lyra - Lightweaver** (Healer)
   - HP: 380, ATK: 30, DEF: 15, SPD: 95
   - Weapon: Staff
   - Skill: Healing Radiance

3. **Ronan - Ironclad Sentinel** (Tank)
   - HP: 700, ATK: 35, DEF: 35, SPD: 85
   - Weapon: Shield & Mace
   - Skill: Unbreakable Bulwark

4. **Cira - Stormcaller** (Burst Mage)
   - HP: 350, ATK: 55, DEF: 12, SPD: 90
   - Weapon: Elemental Orbs
   - Skill: Arcane Missiles

5. **Milo - Shadowstep** (Rogue)
   - HP: 420, ATK: 50, DEF: 18, SPD: 120
   - Weapon: Dual Daggers
   - Skill: Shadowstep Teleport

Each hero includes:
- Character portrait
- Walk animation (6 frames)
- Attack animation (6-8 frames)
- Skill animation (6-10 frames)
- Hit reaction (3 frames)
- Death animation (8 frames)

---

## 🐛 Troubleshooting

### Scripts Have Compilation Errors

**Problem**: Missing namespaces or syntax errors  
**Solution**: Ensure you're using Unity 2021.3+ with .NET 4.x equivalent

### Sprites Look Blurry

**Problem**: Wrong import settings  
**Solution**: Set Texture Type to "Sprite (2D and UI)", Filter Mode to "Point"

### Animations Don't Play

**Problem**: Animator not set up correctly  
**Solution**: Follow `AnimatorSetup.txt` carefully, ensure sprite slicing is correct

### JSON Files Not Loading

**Problem**: Wrong file path  
**Solution**: Files MUST be in `Assets/Resources/Data/` folder

### Touch Controls Don't Work

**Problem**: Testing in editor with mouse  
**Solution**: Use Unity Remote app or build to device for touch testing

---

## 📚 Additional Resources

- **Complete Design**: `design.md` (150+ pages of lore, mechanics, progression)
- **Monetization Plan**: `monetization/monetization_plan.md` (IAP pricing, ads strategy)
- **UI Mockups**: `ui/mockups/` (HUD, base screen, shop layouts)
- **Backend API**: `server/pseudo_api.txt` (for future server integration)
- **Asset List**: `Assets/asset_list.csv` (complete inventory)

---

## 🎯 Next Steps After Import

1. ✅ Complete Combat scene setup
2. ✅ Create all 5 hero Animator Controllers
3. ✅ Implement UI (joystick, skill buttons, HP/XP bars)
4. ✅ Test combat with 1 hero vs 1 enemy
5. ✅ Build APK and test on Android device
6. ✅ Expand to full game (more stages, all heroes, monetization)

---

## 💡 Tips for Success

- **Start small**: Get 1 hero working first, then expand
- **Test often**: Play mode testing after each feature
- **Use prefabs**: Convert working GameObjects to prefabs
- **Mobile first**: Design for mobile from the start (touch controls, UI scaling)
- **Performance**: Use object pooling (already in CombatSystem.cs)
- **Version control**: Use Git or Unity Collaborate for backups

---

## 🆘 Need Help?

- **Unity Documentation**: https://docs.unity3d.com
- **Unity Forums**: https://forum.unity.com
- **Project Documentation**: Check `design.md` for complete game design
- **Scripts Documentation**: All C# scripts have inline comments

---

**🌟 Ready to build Eclipse Reborn! Start with Step 1 and work through each step carefully. Good luck! 🌟**

---

**Last Updated**: November 3, 2025  
**Unity Version**: 2021.3 LTS or newer  
**Platform**: Android/iOS Mobile
