# Playable Demo Assembly Guide
## Eclipse Reborn: Chronicles of the Lost Sun

This guide walks you through assembling a minimal playable prototype in Unity to test core mechanics: movement, combat, hero skills, and base UI.

---

## PREREQUISITES

**Unity Version:** 2021.3 LTS or newer  
**Platform:** Android / iOS (test with Unity Remote or device)  
**Required Packages:**
- Unity UI (com.unity.ugui)
- 2D Sprite (com.unity.2d.sprite)
- Unity IAP (com.unity.purchasing) - for shop integration
- Unity Ads (com.unity.ads) - for rewarded ads

**Install via Package Manager:**
1. Window → Package Manager
2. Search for "Unity UI", "2D Sprite", "In App Purchasing", "Advertisement"
3. Install each package

---

## STEP 1: PROJECT SETUP

### 1.1 Create New Unity Project

1. Open Unity Hub → New Project
2. Template: **2D (URP)** or **2D Core**
3. Project Name: `EclipseReborn`
4. Location: Choose your workspace
5. Click **Create Project**

### 1.2 Import Game Configuration

1. Create folder structure:
   ```
   Assets/
   ├── Resources/
   │   └── data/
   │       ├── game_config.json
   │       └── hero_templates.json
   ├── Scripts/
   │   ├── PlayerController.cs
   │   ├── CombatSystem.cs
   │   ├── XPSystem.cs
   │   ├── BaseManager.cs
   │   ├── ShopManager.cs
   │   └── SaveLoadManager.cs
   ├── Sprites/
   │   ├── Heroes/
   │   ├── Enemies/
   │   ├── VFX/
   │   └── UI/
   ├── Animations/
   │   └── Animators/
   └── Scenes/
       ├── MainMenu.unity
       ├── CombatDemo.unity
       └── Sanctuary.unity
   ```

2. Copy JSON files from `/data/` to `Assets/Resources/data/`
3. Copy C# scripts from `/unity_scripts/` to `Assets/Scripts/`

### 1.3 Configure Build Settings

1. File → Build Settings
2. Platform: **Android** or **iOS**
3. Switch Platform
4. Player Settings → Other Settings:
   - Package Name: `com.yourcompany.eclipsereborn`
   - Minimum API Level: Android 7.0 (API 24) or iOS 13+
   - Scripting Backend: IL2CPP (required for iOS)
   - Target Architectures: ARM64 (required)

---

## STEP 2: CREATE COMBAT DEMO SCENE

### 2.1 Scene Setup

1. Create new scene: File → New Scene → 2D
2. Save as `Assets/Scenes/CombatDemo.unity`
3. Delete default Main Camera (we'll add UI Camera)

### 2.2 Create Main Camera

1. GameObject → Camera
2. Rename to "Main Camera"
3. Set Position: (0, 0, -10)
4. Set Size: 5 (orthographic)
5. Set Background: #1A1A2E (dark blue)

### 2.3 Create Ground

1. GameObject → 2D Object → Sprite → Square
2. Rename to "Ground"
3. Transform: Position (0, -3, 0), Scale (20, 1, 1)
4. Sprite Renderer: Color = #2A2A3E (gray)
5. Add Component → Box Collider 2D

### 2.4 Create Player (Auron)

1. GameObject → 2D Object → Sprite → Square
2. Rename to "Player_Auron"
3. Transform: Position (0, 0, 0), Scale (1, 1.5, 1)
4. Sprite Renderer: Color = #FFD700 (gold - placeholder)
5. Add Component → Rigidbody 2D:
   - Body Type: Dynamic
   - Gravity Scale: 0 (top-down movement)
   - Constraints: Freeze Rotation Z
6. Add Component → Capsule Collider 2D
7. Add Component → PlayerController (script)
8. Add Component → CombatSystem (script)
9. Add Component → XPSystem (script)

### 2.5 Create Attack Point

1. GameObject → Create Empty (as child of Player_Auron)
2. Rename to "AttackPoint"
3. Position: (1, 0, 0) - in front of player
4. Add Component → Gizmos (for visualization)
5. In PlayerController script: Assign AttackPoint to attackPoint field

### 2.6 Create Enemy

1. Duplicate Player_Auron → Rename to "Enemy_Shade"
2. Position: (5, 0, 0)
3. Sprite Renderer: Color = #8B00FF (purple - placeholder)
4. Remove PlayerController, XPSystem
5. Keep CombatSystem, add CombatTarget component
6. CombatTarget settings:
   - Max HP: 200
   - Defense: 10
7. Tag as "Enemy" (Create new tag if needed)
8. Layer: "Enemy" (Create new layer)

### 2.7 Create Animator for Player

1. Select Player_Auron
2. Add Component → Animator
3. Create → Animator Controller → "Auron_Animator"
4. Save in `Assets/Animations/Animators/`
5. Assign to Animator component
6. Open Animator window (Window → Animation → Animator)
7. Create parameters (see `AnimatorSetup.txt`):
   - Float: Speed
   - Trigger: Attack, Skill1, Skill2, Dash, Hit, Death

**Note:** For this demo, use placeholder animations (static sprite with 1 frame).  
In production, import sprite sheets and create frame animations.

---

## STEP 3: CREATE UI CANVAS

### 3.1 Create UI Canvas

1. GameObject → UI → Canvas
2. Rename to "UI_Canvas"
3. Canvas settings:
   - Render Mode: Screen Space - Camera
   - Render Camera: Drag Main Camera here
   - Plane Distance: 10
4. Canvas Scaler:
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080
   - Match: Width 0.5, Height 0.5

### 3.2 Create Virtual Joystick

**Option A: Use Asset (Recommended)**
1. Download free joystick asset (e.g., "Simple Input System" from Unity Asset Store)
2. Import and configure

**Option B: Manual Setup**
1. GameObject → UI → Image (as child of Canvas)
2. Rename to "Joystick_Background"
3. Position: Bottom-left (-600, -300)
4. Size: 200 x 200
5. Color: White, Alpha 50%
6. Add child Image: "Joystick_Knob" (100x100, white)
7. Add script: `Joystick.cs` (simple joystick implementation)

### 3.3 Create Skill Buttons

1. GameObject → UI → Button (as child of Canvas)
2. Rename to "Button_Skill1"
3. Position: Bottom-right (550, -300)
4. Size: 100 x 100
5. Add Text child: "SK1" (white, bold, 20pt)
6. Duplicate for Skill2: Position (400, -300)
7. Create Ultimate button: Position (475, -150), Size 120x120

### 3.4 Create HP/XP Bars

1. GameObject → UI → Image → "HP_Bar_Background"
2. Position: Top-left (-600, 450)
3. Size: 300 x 30
4. Color: Dark red (#3A0000)
5. Add child Image: "HP_Bar_Fill"
6. Anchor: Left-stretch (stretches with value)
7. Color: Red (#FF0000)
8. Image Type: Filled (Fill Method: Horizontal, Fill Origin: Left)
9. Repeat for XP bar below HP bar

### 3.5 Wire UI to PlayerController

1. Select Player_Auron
2. In PlayerController component:
   - Assign Joystick to `movementJoystick` field
   - Assign Button_Skill1 to `skill1Button`
   - Assign Button_Skill2 to `skill2Button`
   - Assign Button_Dash to `dashButton` (create if missing)

---

## STEP 4: CONFIGURE LAYERS & PHYSICS

### 4.1 Create Layers

1. Edit → Project Settings → Tags and Layers
2. Add layers:
   - Layer 8: "Player"
   - Layer 9: "Enemy"
   - Layer 10: "Projectile"

### 4.2 Configure Physics2D

1. Edit → Project Settings → Physics 2D
2. Layer Collision Matrix:
   - Player collides with: Enemy, Ground
   - Enemy collides with: Player, Ground
   - Projectile collides with: Enemy

---

## STEP 5: TEST THE DEMO

### 5.1 Play in Editor

1. Click Play button in Unity Editor
2. Use virtual joystick to move Auron
3. Click skill buttons to trigger animations
4. Walk near enemy and attack

**Expected Behavior:**
- Auron moves with joystick
- Attack button triggers attack animation
- Damage numbers appear above enemy
- Enemy HP decreases
- XP gained when enemy dies

### 5.2 Debug Common Issues

| Issue | Solution |
|-------|----------|
| Joystick doesn't move player | Check joystick is assigned in PlayerController |
| No damage dealt | Verify AttackPoint position, check Enemy layer |
| Enemy doesn't die | Check CombatTarget component on enemy |
| Buttons don't work | Verify EventSystem exists (auto-created with Canvas) |
| Animation doesn't play | Check Animator parameters match code |

---

## STEP 6: BUILD FOR MOBILE

### 6.1 Configure Player Settings

1. File → Build Settings → Player Settings
2. Android:
   - Minimum API: 24 (Android 7.0)
   - Target API: 33+ (latest)
   - Scripting Backend: IL2CPP
   - ARM64: Checked
3. iOS:
   - Minimum iOS Version: 13.0
   - Architecture: ARM64
   - Requires ARKit: Unchecked

### 6.2 Test with Unity Remote (Quick Test)

**Android:**
1. Install Unity Remote 5 app on Android device
2. USB connect device to PC
3. Unity → Edit → Project Settings → Editor → Device: "Any Android Device"
4. Play in editor → Touch controls work on device

**iOS:**
1. Install Unity Remote 5 on iPhone
2. Same process as Android

### 6.3 Build APK/IPA

**Android:**
1. File → Build Settings → Build
2. Save as `EclipseReborn.apk`
3. Transfer to device and install

**iOS:**
1. File → Build Settings → Build
2. Save as `EclipseReborn-iOS`
3. Open in Xcode, sign with Apple Developer account
4. Build to device

---

## STEP 7: ADD SANCTUARY SCENE (OPTIONAL)

### 7.1 Create Sanctuary Scene

1. File → New Scene → 2D
2. Save as `Assets/Scenes/Sanctuary.unity`
3. Add background sprites (placeholder: solid color #3A5A40 green)
4. Create UI panels for buildings (see `base_screen_layout.txt`)

### 7.2 Add Building GameObjects

1. GameObject → 2D Object → Sprite
2. Rename: "Building_Barracks"
3. Position: (-4, 0, 0), Scale (2, 2, 1)
4. Sprite: Placeholder square (color: #8B4513 brown)
5. Add Component → Button (for tap interaction)
6. Repeat for Workshop, Reactor, Treasury, Training Grounds

### 7.3 Link to Combat Scene

1. Create UI Button: "Go to Battle"
2. Add onClick listener → SceneManager.LoadScene("CombatDemo")
3. Add SceneManager code to button script

---

## STEP 8: MINIMAL INPUT MAPPING

### 8.1 Keyboard Testing (Editor Only)

Add fallback in PlayerController.cs for keyboard input:

```csharp
// In HandleMovementInput()
if (movementJoystick != null)
{
    moveInput = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
}
else
{
    // Keyboard fallback for editor testing
    moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
}
```

Keyboard controls for testing:
- WASD / Arrow Keys: Move
- Space: Attack
- Q: Skill 1
- E: Skill 2
- Shift: Dash

### 8.2 Touch Input (Mobile)

PlayerController already supports touch via virtual joystick and UI buttons.  
No additional setup needed.

---

## STEP 9: DEMO CHECKLIST

Before showing the demo, verify:

- [ ] Player moves smoothly with joystick
- [ ] Attack button triggers attack animation
- [ ] Damage numbers appear and are readable
- [ ] Enemy takes damage and dies at 0 HP
- [ ] XP bar fills after killing enemy
- [ ] Level up popup appears (if implemented)
- [ ] Skill buttons trigger skills (even if placeholder)
- [ ] HP bar updates when player takes damage
- [ ] UI scales correctly on different screen sizes
- [ ] No console errors or warnings
- [ ] Framerate: 60 FPS on device (30 FPS minimum)

---

## STEP 10: NEXT STEPS FOR PRODUCTION

1. **Replace Placeholder Art:**
   - Import chibi character sprites (see `asset_list.csv`)
   - Create sprite sheet animations (idle, walk, attack, skills)
   - Import VFX particle systems (sword slashes, magic effects)

2. **Add Audio:**
   - Import SFX (sword swing, hit, magic cast)
   - Import background music (combat theme)
   - Add AudioSource components and trigger via animation events

3. **Implement All Heroes:**
   - Create prefabs for Lyra, Ronan, Cira, Milo
   - Set up unique Animators for each hero
   - Implement hero-specific skill logic

4. **Add Enemy AI:**
   - Create EnemyAI.cs script
   - Implement chase/attack behavior
   - Add variety (melee, ranged, elite enemies)

5. **Expand Sanctuary:**
   - Implement building upgrade logic (BaseManager.cs)
   - Add production timers and UI updates
   - Integrate shop and IAP (ShopManager.cs)

6. **Server Integration:**
   - Set up backend (see `server/pseudo_api.txt`)
   - Implement cloud save (SaveLoadManager.cs → server sync)
   - Add IAP receipt validation

7. **Polish & Optimization:**
   - Object pooling for damage numbers and VFX
   - Reduce draw calls (sprite atlasing)
   - Profile on low-end devices (60 FPS target)

8. **QA Testing:**
   - Test on 5+ device models (Samsung, Google Pixel, iPhone 8+, iPhone 14+)
   - Test all screen sizes (notch handling, safe area)
   - Test IAP purchases (sandbox environment)
   - Test offline/online mode switching

---

## TROUBLESHOOTING

### Issue: Unity Remote not connecting

**Solution:**
- Enable USB debugging on Android device
- iOS: Trust computer when prompted
- Unity → Edit → Project Settings → Editor → Device: Select correct device

### Issue: Build fails (Android)

**Solution:**
- Install Android SDK via Unity Hub → Installs → Android Module
- Verify JDK, SDK, NDK paths in Unity Preferences → External Tools
- Increase heap size: Preferences → External Tools → JVM Heap Size: 4096MB

### Issue: Build fails (iOS)

**Solution:**
- Xcode must be installed (Mac only)
- Apple Developer account required (free or paid)
- Sign with team in Xcode → Signing & Capabilities

### Issue: Low framerate on device

**Solution:**
- Profile with Unity Profiler (Window → Analysis → Profiler)
- Reduce draw calls (combine sprites into atlases)
- Disable shadows, reduce particle count
- Lower camera resolution in Quality Settings

---

## DEMO SCENE SUMMARY

**What's playable:**
- 1 hero (Auron) with basic movement and attack
- 1 enemy (Shade) that takes damage and dies
- Virtual joystick + skill buttons
- HP/XP bars with visual feedback
- Damage numbers (pooled for performance)

**What's next:**
- Full hero roster (5 heroes)
- Complete stage progression (50 chapters)
- Sanctuary building system
- Shop + IAP integration
- Server backend for cloud saves

---

**Estimated Assembly Time:** 2-4 hours (experienced Unity dev)  
**Estimated Time to MVP:** 2-4 sprints (2-week sprints, 4-person team)

---

**Document Version:** 1.0  
**Last Updated:** November 2, 2025  
**Author:** Technical Lead, Eclipse Reborn Team
