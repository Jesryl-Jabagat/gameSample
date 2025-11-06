# Character Sprites - Eclipse Reborn

## Overview
This folder contains **30 high-quality sprite sheets** for all 5 playable heroes in Eclipse Reborn, each with **transparent backgrounds** ready for Unity import.

## Character List

### 1. Auron - Solaris Vanguard
**Role:** Balanced DPS | **Weapon:** Greatsword  
**Files:**
- `Auron_idle_sprite_transparent_*.png` - Idle/standing pose
- `Auron_walk_animation_sheet_*.png` - 6-frame walk cycle
- `Auron_attack_animation_sheet_*.png` - 8-frame greatsword attack
- `Auron_skill_animation_sheet_*.png` - 10-frame Solar Cleave ultimate skill
- `Auron_hit_reaction_animation_*.png` - 3-frame damage reaction
- `Auron_death_animation_sheet_*.png` - 8-frame defeat sequence

### 2. Lyra - Lightweaver
**Role:** Healer/Support | **Weapon:** Staff  
**Files:**
- `Lyra_idle_sprite_transparent_*.png` - Idle/standing pose
- `Lyra_walk_animation_sheet_*.png` - 6-frame graceful walk
- `Lyra_attack_animation_sheet_*.png` - 6-frame staff attack
- `Lyra_healing_skill_animation_*.png` - 8-frame Healing Radiance spell
- `Lyra_hit_reaction_animation_*.png` - 3-frame damage reaction
- `Lyra_death_animation_sheet_*.png` - 8-frame defeat sequence

### 3. Ronan - Ironclad Sentinel
**Role:** Tank/Defender | **Weapon:** Shield & Mace  
**Files:**
- `Ronan_idle_sprite_transparent_*.png` - Idle/standing pose
- `Ronan_walk_animation_sheet_*.png` - 6-frame heavy armored walk
- `Ronan_attack_animation_sheet_*.png` - 8-frame shield bash
- `Ronan_bulwark_skill_animation_*.png` - 10-frame Unbreakable Bulwark defensive buff
- `Ronan_hit_reaction_animation_*.png` - 3-frame damage absorption
- `Ronan_death_animation_sheet_*.png` - 8-frame defeat sequence

### 4. Cira - Stormcaller
**Role:** Burst Mage/AoE | **Weapon:** Elemental Orbs  
**Files:**
- `Cira_idle_sprite_transparent_*.png` - Idle/standing pose with floating orbs
- `Cira_walk_animation_sheet_*.png` - 6-frame mystical walk
- `Cira_attack_animation_sheet_*.png` - 6-frame orb projectile attack
- `Cira_arcane_missiles_skill_*.png` - 10-frame Arcane Missiles barrage
- `Cira_hit_reaction_animation_*.png` - 3-frame damage reaction
- `Cira_death_animation_sheet_*.png` - 8-frame defeat with magic dispersal

### 5. Milo - Shadowstep
**Role:** Rogue/Single-Target DPS | **Weapon:** Dual Daggers  
**Files:**
- `Milo_idle_sprite_transparent_*.png` - Idle/crouched pose
- `Milo_walk_animation_sheet_*.png` - 6-frame stealthy walk
- `Milo_attack_animation_sheet_*.png` - 8-frame rapid dagger slashes
- `Milo_shadowstep_skill_animation_*.png` - 6-frame teleport-dash ability
- `Milo_hit_reaction_animation_*.png` - 3-frame damage reaction
- `Milo_death_animation_sheet_*.png` - 8-frame defeat with shadow dispersal

## Technical Specifications

### Format
- **File Type:** PNG with alpha transparency
- **Background:** Fully transparent (no background)
- **Style:** Pixel art / Chibi proportions suitable for mobile 2D RPG
- **Aspect Ratios:**
  - Idle sprites: 1:1 (square)
  - Walk/Attack/Skill/Death: 16:9 (horizontal strip for frame sequences)
  - Hit reactions: 4:3 (short sequences)

### Unity Import Guide

1. **Import to Unity:**
   ```
   Copy all files to: Assets/Sprites/Characters/
   ```

2. **Configure Sprite Settings:**
   - Texture Type: Sprite (2D and UI)
   - Sprite Mode: Multiple (for animation sheets)
   - Pixels Per Unit: 100 (adjust based on your game scale)
   - Filter Mode: Point (for crisp pixel art)
   - Compression: None or High Quality

3. **Slice Animation Sheets:**
   - Open Sprite Editor
   - Use Automatic or Grid slicing
   - Set appropriate frame counts per animation
   - Apply pivot points (typically bottom-center for characters)

4. **Create Animator Controllers:**
   - Reference `/unity_scripts/AnimatorSetup.txt` for state machine setup
   - Create animation clips from sliced sprites
   - Set up transitions and parameters

## Animation Frame Counts

| Animation Type | Frames | Use Case |
|---------------|--------|----------|
| Idle | 1 | Standing/waiting |
| Walk | 6 | Movement locomotion |
| Attack (melee) | 8 | Basic attacks |
| Attack (ranged) | 6 | Projectile attacks |
| Skills | 6-10 | Special abilities |
| Hit | 3 | Taking damage |
| Death | 8 | Defeat sequence |

## Color Palettes

- **Auron:** Gold, orange, red (solar/fire theme)
- **Lyra:** White, light blue, gold accents (holy/light theme)
- **Ronan:** Silver, steel grey, red (armored/defensive theme)
- **Cira:** Purple, cyan, pink (arcane/magical theme)
- **Milo:** Black, dark grey, silver (shadow/stealth theme)

## Design Consistency

All sprites feature:
✅ Consistent chibi/SD proportions across all characters  
✅ Clean pixel art style optimized for mobile displays  
✅ Transparent backgrounds for easy compositing  
✅ Matching visual style suitable for Eclipse Reborn's aesthetic  
✅ Animation-ready frame layouts  
✅ Distinct color palettes per character for visual recognition  

## Next Steps

1. Import sprites into Unity project
2. Slice animation sheets in Sprite Editor
3. Create Animation Clips for each action
4. Set up Animator Controllers per character
5. Attach to character GameObjects
6. Test animations in-game

For complete Unity integration instructions, see:
- `/playable_demo/README.md` - Full Unity setup guide
- `/unity_scripts/AnimatorSetup.txt` - Animator parameters and events
- `/unity_scripts/PlayerController.cs` - Character control script

---

**Generated:** November 6, 2025  
**Total Files:** 30 sprite sheets (5 characters × 6 animations)  
**Ready for:** Unity 2021.3+ LTS
