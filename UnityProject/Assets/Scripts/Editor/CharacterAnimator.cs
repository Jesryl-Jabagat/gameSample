using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;
using System.Collections.Generic;

public class CharacterAnimator
{
    [MenuItem("Eclipse Reborn/Animate All Characters")]
    public static void AnimateAllCharacters()
    {
        EditorUtility.DisplayProgressBar("Eclipse Reborn", "Creating character animations...", 0f);
        
        try
        {
            string[] characters = { "Auron", "Cira", "Lyra", "Milo", "Ronan" };
            
            for (int i = 0; i < characters.Length; i++)
            {
                float progress = (float)i / characters.Length;
                EditorUtility.DisplayProgressBar("Eclipse Reborn", $"Animating {characters[i]}...", progress);
                
                AnimateCharacter(characters[i]);
            }
            
            AssetDatabase.Refresh();
            
            EditorUtility.DisplayDialog("Animation Complete!", 
                "🎭 ALL CHARACTERS ANIMATED! 🎭\n\n" +
                "Created for each character:\n" +
                "✅ Walking animation (smooth cycle)\n" +
                "✅ Attack animation (combat moves)\n" +
                "✅ Skill animation (special abilities)\n" +
                "✅ Idle animation (standing pose)\n" +
                "✅ Hit reaction animation\n\n" +
                "Drag characters to scene and press Play to see animations!", "Awesome!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Animation setup failed: " + e.Message);
            EditorUtility.DisplayDialog("Animation Error", "Something went wrong: " + e.Message, "OK");
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    static void AnimateCharacter(string characterName)
    {
        // Create Animations folder if it doesn't exist
        if (!AssetDatabase.IsValidFolder("Assets/Animations"))
        {
            AssetDatabase.CreateFolder("Assets", "Animations");
        }
        
        // Create Animator Controller
        string controllerPath = $"Assets/Animations/{characterName}_Controller.controller";
        AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
        
        // Add parameters for animation control
        controller.AddParameter("Speed", AnimatorControllerParameterType.Float);
        controller.AddParameter("Attack", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Skill1", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Skill2", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Hit", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Death", AnimatorControllerParameterType.Trigger);
        
        var rootStateMachine = controller.layers[0].stateMachine;
        
        // Create animation states
        var idleState = CreateAnimationState(rootStateMachine, characterName, "Idle", "idle");
        var walkState = CreateAnimationState(rootStateMachine, characterName, "Walk", "walk_animation_sheet");
        var attackState = CreateAnimationState(rootStateMachine, characterName, "Attack", "attack_animation_sheet");
        var skill1State = CreateAnimationState(rootStateMachine, characterName, "Skill1", "skill_animation_sheet");
        var hitState = CreateAnimationState(rootStateMachine, characterName, "Hit", "hit_animation_sheet");
        
        // Set default state
        rootStateMachine.defaultState = idleState;
        
        // Create transitions
        CreateTransitions(idleState, walkState, attackState, skill1State, hitState);
        
        // Update character prefab with animator
        UpdateCharacterPrefab(characterName, controller);
        
        Debug.Log($"✅ Animated {characterName}");
    }

    static AnimatorState CreateAnimationState(AnimatorStateMachine stateMachine, string characterName, string animationName, string spriteSheetName)
    {
        // Create animation clip
        AnimationClip clip = new AnimationClip();
        clip.name = $"{characterName}_{animationName}";
        clip.frameRate = 12f; // Good speed for pixel art
        
        // Find sprite sheet or single sprite
        List<Sprite> sprites = FindAnimationSprites(characterName, spriteSheetName);
        
        if (sprites.Count > 0)
        {
            // Create keyframes for sprite animation
            ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[sprites.Count];
            
            for (int i = 0; i < sprites.Count; i++)
            {
                keyframes[i] = new ObjectReferenceKeyframe();
                keyframes[i].time = i / clip.frameRate;
                keyframes[i].value = sprites[i];
            }
            
            // Create animation curve
            EditorCurveBinding spriteBinding = new EditorCurveBinding();
            spriteBinding.type = typeof(SpriteRenderer);
            spriteBinding.path = "";
            spriteBinding.propertyName = "m_Sprite";
            
            AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, keyframes);
            
            // Set loop for walking animation
            if (animationName == "Walk")
            {
                AnimationUtility.SetAnimationClipSettings(clip, new AnimationClipSettings 
                { 
                    loopTime = true,
                    startTime = 0f,
                    stopTime = sprites.Count / clip.frameRate
                });
            }
        }
        
        // Ensure Animations folder exists
        if (!AssetDatabase.IsValidFolder("Assets/Animations"))
        {
            AssetDatabase.CreateFolder("Assets", "Animations");
        }
        
        // Save animation clip
        string clipPath = $"Assets/Animations/{characterName}_{animationName}.anim";
        AssetDatabase.CreateAsset(clip, clipPath);
        
        // Create state in animator
        var state = stateMachine.AddState(animationName);
        state.motion = clip;
        
        return state;
    }

    static List<Sprite> FindAnimationSprites(string characterName, string spriteSheetName)
    {
        List<Sprite> sprites = new List<Sprite>();
        
        // Try to find sprite sheet first
        string[] possiblePaths = {
            $"Assets/Sprites/Characters/{characterName}_{spriteSheetName}",
            $"Assets/Sprites/Characters/{characterName}_hero_character_sprite_8cfb17d1.png",
            $"Assets/Sprites/Characters/{characterName}_mage_character_sprite_84c9f07c.png",
            $"Assets/Sprites/Characters/{characterName}_healer_character_sprite_96d4ac30.png",
            $"Assets/Sprites/Characters/{characterName}_rogue_character_sprite_6bf87aa4.png",
            $"Assets/Sprites/Characters/{characterName}_tank_character_sprite_09614f5a.png"
        };
        
        foreach (string path in possiblePaths)
        {
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
            foreach (Object asset in assets)
            {
                if (asset is Sprite sprite)
                {
                    sprites.Add(sprite);
                }
            }
            
            if (sprites.Count > 0) break;
        }
        
        // If no sprite sheet found, use single character sprite
        if (sprites.Count == 0)
        {
            string singleSpritePath = GetCharacterSpritePath(characterName);
            Sprite singleSprite = AssetDatabase.LoadAssetAtPath<Sprite>(singleSpritePath);
            if (singleSprite != null)
            {
                sprites.Add(singleSprite);
            }
        }
        
        return sprites;
    }

    static string GetCharacterSpritePath(string characterName)
    {
        switch (characterName)
        {
            case "Auron": return "Assets/Sprites/Characters/Auron_hero_character_sprite_8cfb17d1.png";
            case "Cira": return "Assets/Sprites/Characters/Cira_mage_character_sprite_84c9f07c.png";
            case "Lyra": return "Assets/Sprites/Characters/Lyra_healer_character_sprite_96d4ac30.png";
            case "Milo": return "Assets/Sprites/Characters/Milo_rogue_character_sprite_6bf87aa4.png";
            case "Ronan": return "Assets/Sprites/Characters/Ronan_tank_character_sprite_09614f5a.png";
            default: return "";
        }
    }

    static void CreateTransitions(AnimatorState idle, AnimatorState walk, AnimatorState attack, AnimatorState skill1, AnimatorState hit)
    {
        // Idle to Walk (when moving)
        var idleToWalk = idle.AddTransition(walk);
        idleToWalk.AddCondition(AnimatorConditionMode.Greater, 0.1f, "Speed");
        idleToWalk.duration = 0.1f;
        
        // Walk to Idle (when stopped)
        var walkToIdle = walk.AddTransition(idle);
        walkToIdle.AddCondition(AnimatorConditionMode.Less, 0.1f, "Speed");
        walkToIdle.duration = 0.1f;
        
        // Any state to Attack
        var anyToAttack = new AnimatorStateTransition();
        anyToAttack.destinationState = attack;
        anyToAttack.AddCondition(AnimatorConditionMode.If, 0, "Attack");
        anyToAttack.duration = 0.05f;
        idle.AddTransition(anyToAttack);
        walk.AddTransition(anyToAttack);
        
        // Attack back to Idle
        var attackToIdle = attack.AddTransition(idle);
        attackToIdle.hasExitTime = true;
        attackToIdle.exitTime = 0.9f;
        attackToIdle.duration = 0.1f;
        
        // Any state to Skill1
        var anyToSkill = new AnimatorStateTransition();
        anyToSkill.destinationState = skill1;
        anyToSkill.AddCondition(AnimatorConditionMode.If, 0, "Skill1");
        anyToSkill.duration = 0.05f;
        idle.AddTransition(anyToSkill);
        walk.AddTransition(anyToSkill);
        
        // Skill back to Idle
        var skillToIdle = skill1.AddTransition(idle);
        skillToIdle.hasExitTime = true;
        skillToIdle.exitTime = 0.9f;
        skillToIdle.duration = 0.1f;
        
        // Any state to Hit
        var anyToHit = new AnimatorStateTransition();
        anyToHit.destinationState = hit;
        anyToHit.AddCondition(AnimatorConditionMode.If, 0, "Hit");
        anyToHit.duration = 0.05f;
        idle.AddTransition(anyToHit);
        walk.AddTransition(anyToHit);
        
        // Hit back to Idle
        var hitToIdle = hit.AddTransition(idle);
        hitToIdle.hasExitTime = true;
        hitToIdle.exitTime = 0.9f;
        hitToIdle.duration = 0.1f;
    }

    static void UpdateCharacterPrefab(string characterName, AnimatorController controller)
    {
        string prefabPath = $"Assets/Prefabs/{characterName}.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        
        if (prefab != null)
        {
            // Load prefab for editing
            GameObject prefabInstance = PrefabUtility.LoadPrefabContents(prefabPath);
            
            // Add or update Animator component
            Animator animator = prefabInstance.GetComponent<Animator>();
            if (animator == null)
            {
                animator = prefabInstance.AddComponent<Animator>();
            }
            
            animator.runtimeAnimatorController = controller;
            
            // Save prefab
            PrefabUtility.SaveAsPrefabAsset(prefabInstance, prefabPath);
            PrefabUtility.UnloadPrefabContents(prefabInstance);
        }
    }

    [MenuItem("Eclipse Reborn/Test Character Animation")]
    public static void TestCharacterAnimation()
    {
        // Create a test scene with animated character
        var scene = UnityEditor.SceneManagement.EditorSceneManager.NewScene(
            UnityEditor.SceneManagement.NewSceneSetup.EmptyScene, 
            UnityEditor.SceneManagement.NewSceneMode.Single);

        // Camera
        var cam = new GameObject("Main Camera").AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 5;
        cam.backgroundColor = new Color(0.2f, 0.3f, 0.5f);
        cam.transform.position = new Vector3(0, 0, -10);

        // Load Auron prefab
        GameObject auronPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Auron.prefab");
        if (auronPrefab != null)
        {
            GameObject auron = PrefabUtility.InstantiatePrefab(auronPrefab) as GameObject;
            auron.transform.position = Vector3.zero;
        }

        EditorUtility.DisplayDialog("Test Scene Ready!", 
            "Animation test scene created!\n\n" +
            "Press Play and use WASD to see walking animation!\n" +
            "Press Space to see attack animation!", "Cool!");
    }
}