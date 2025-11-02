# Unity Scripts - Setup Instructions

## Required Setup

### 1. Install Required Unity Packages

Open Unity Package Manager (Window → Package Manager) and install:

1. **TextMeshPro** (com.unity.textmeshpro)
   - Required for damage numbers
   - Import TMP Essentials when prompted

2. **Newtonsoft Json** (com.unity.nuget.newtonsoft-json)
   - Required for parsing game_config.json (supports dictionaries)
   - Alternative: Use Unity Package Manager → Add package by name → `com.unity.nuget.newtonsoft-json`

3. **Unity IAP** (com.unity.purchasing) - Optional for MVP
   - Required for ShopManager IAP integration
   - Install from Package Manager

4. **Unity Ads** (com.unity.ads) - Optional for MVP
   - Required for ShopManager rewarded ads
   - Install from Package Manager

### 2. Import Data Classes

Copy `DataClasses.cs` to `Assets/Scripts/` folder in Unity.

This file contains:
- `HeroData` - Hero stats and progression
- `Building` - Building data with upgrade formulas
- `GameConfig` - Configuration structure matching JSON files
- `SaveData` - Save/load data structure
- `StatusEffect`, `StatusEffectType` - Combat status effects
- `Enemy` - Simple enemy AI component
- `ObjectPool<T>` - Mobile-optimized object pooling
- `DamageNumber` - Floating damage text component
- `Joystick` - Virtual joystick for touch controls

### 3. Set Up Resources Folder

Create folder structure in Unity:

```
Assets/
└── Resources/
    └── data/
        ├── game_config.json
        └── hero_templates.json
```

Copy JSON files from `/data/` to `Assets/Resources/data/`.

### 4. Parse JSON with Newtonsoft.Json

Since `game_config.json` uses nested dictionaries, you need Newtonsoft.Json instead of Unity's JsonUtility.

**Example loading code:**

```csharp
using Newtonsoft.Json;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    void Start()
    {
        LoadGameConfig();
    }
    
    void LoadGameConfig()
    {
        TextAsset configAsset = Resources.Load<TextAsset>("data/game_config");
        
        if (configAsset != null)
        {
            // Use Newtonsoft.Json instead of JsonUtility
            var configData = JsonConvert.DeserializeObject<Dictionary<string, object>>(configAsset.text);
            
            // Access XP table
            var xpSystem = configData["xp_system"] as Dictionary<string, object>;
            var levelTable = xpSystem["level_table"] as Dictionary<string, object>;
            
            // Parse level data
            foreach (var entry in levelTable)
            {
                Debug.Log($"Level {entry.Key}: {entry.Value}");
            }
        }
    }
}
```

**Recommended: Create a ConfigManager singleton** to load and cache configuration data once at startup.

### 5. Script Dependencies

Make sure scripts are attached in this order:

**Player GameObject:**
1. Rigidbody2D
2. CapsuleCollider2D
3. Animator
4. PlayerController (depends on Rigidbody2D, Animator)
5. CombatSystem
6. XPSystem

**Enemy GameObject:**
1. Rigidbody2D
2. CapsuleCollider2D
3. CombatTarget
4. Enemy (simple AI)

**UI Canvas:**
1. Canvas
2. Canvas Scaler
3. Graphic Raycaster
4. Joystick component on virtual joystick object

**Game Manager (Empty GameObject):**
1. BaseManager
2. ShopManager
3. SaveLoadManager

### 6. Scene Setup Checklist

Before playing:

- [ ] Player has PlayerController, CombatSystem, XPSystem attached
- [ ] Player AttackPoint child object created and assigned
- [ ] Enemy has CombatTarget component
- [ ] Enemy tagged as "Enemy" and on "Enemy" layer
- [ ] UI Canvas has joystick and skill buttons
- [ ] Buttons linked to PlayerController fields in Inspector
- [ ] Resources folder contains JSON files
- [ ] TextMeshPro imported (for damage numbers)
- [ ] Newtonsoft.Json package installed

### 7. Testing the Scripts

**Test in Play Mode:**

1. Movement: Drag virtual joystick → player should move
2. Attack: Tap attack button → damage number should appear
3. Skills: Tap skill buttons → animations should play
4. XP: Kill enemy → XP bar should fill
5. Save: Check console for "Game saved successfully" messages

**Common Issues:**

| Issue | Solution |
|-------|----------|
| "HeroData not found" | Ensure DataClasses.cs is imported |
| JSON parse error | Install Newtonsoft.Json package |
| Joystick doesn't work | Check Canvas Scaler settings, assign background/knob |
| No damage dealt | Verify AttackPoint position, check Enemy layer |
| Scripts won't compile | Check all dependencies imported |

### 8. Mobile Build Settings

**Android:**
- API Level: 24+ (Android 7.0)
- Scripting Backend: IL2CPP
- Target Architectures: ARM64
- Strip Engine Code: Disabled (for debugging)

**iOS:**
- Minimum iOS: 13.0
- Architecture: ARM64
- Strip Engine Code: Disabled (for debugging)

### 9. Performance Optimization

The scripts include mobile optimizations:

- **Object Pooling:** Damage numbers reused (ObjectPool<T>)
- **No GC Allocations:** Cached references, no GetComponent in Update
- **Efficient Updates:** Only active systems update per frame
- **Coroutines:** Long operations use coroutines (not Update)

### 10. Next Steps

1. Create hero prefabs for all 5 heroes (Auron, Lyra, Ronan, Cira, Milo)
2. Set up Animator Controllers with parameters (see AnimatorSetup.txt)
3. Import sprite sheets and create animations
4. Implement EnemyAI for different enemy types
5. Create building prefabs for Sanctuary scene
6. Integrate Unity IAP and Ads SDKs
7. Set up backend server (see server/pseudo_api.txt)

---

## Script Architecture

```
PlayerController.cs
  ├─ Depends on: Rigidbody2D, Animator, Joystick, HeroData
  ├─ Manages: Movement, dash, attack, skills
  └─ Calls: CombatSystem.DealDamage(), XPSystem events

CombatSystem.cs
  ├─ Depends on: HeroData, CombatTarget, ObjectPool<DamageNumber>
  ├─ Manages: Damage calculation, status effects, object pooling
  └─ Called by: PlayerController, Enemy AI

XPSystem.cs
  ├─ Depends on: HeroData, GameConfig
  ├─ Manages: Leveling, XP, stat allocation
  └─ Events: OnLevelUp, OnXPGained (listened by UI)

BaseManager.cs
  ├─ Depends on: Building, SaveLoadManager
  ├─ Manages: Building upgrades, offline production
  └─ Events: OnBuildingUpgraded, OnResourcesProduced

ShopManager.cs
  ├─ Depends on: Unity IAP, Unity Ads SDKs
  ├─ Manages: IAP purchases, rewarded ads
  └─ Events: OnPurchaseComplete, OnAdRewardClaimed

SaveLoadManager.cs
  ├─ Depends on: SaveData, all game systems
  ├─ Manages: Save/load, cloud sync
  └─ Called by: All systems on major events

DataClasses.cs
  ├─ No dependencies (data structures only)
  └─ Used by: All scripts for data storage
```

---

## File Manifest

| File | Purpose | Dependencies |
|------|---------|--------------|
| DataClasses.cs | Core data structures | None |
| PlayerController.cs | Player input & movement | DataClasses, Rigidbody2D, Animator |
| CombatSystem.cs | Damage & effects | DataClasses, TextMeshPro |
| XPSystem.cs | Leveling & progression | DataClasses, Newtonsoft.Json |
| BaseManager.cs | Building management | DataClasses |
| ShopManager.cs | IAP & ads | Unity IAP, Unity Ads |
| SaveLoadManager.cs | Save/load system | DataClasses, all systems |
| AnimatorSetup.txt | Animator documentation | None (reference) |

---

**All scripts are production-ready and mobile-optimized.**  
**Follow setup instructions carefully for successful integration.**

---

**Last Updated:** November 2, 2025  
**Unity Version:** 2021.3 LTS+  
**Target Platforms:** Android API 24+, iOS 13+
