using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class SimpleMobileSetup
{
    [MenuItem("Eclipse Reborn/Setup Complete Mobile RPG")]
    public static void SetupCompleteMobileRPG()
    {
        // Configure project for mobile
        ConfigureProjectForMobile();
        
        // Create main game scene
        CreateMainGameScene();
        
        // Setup character prefabs
        SetupCharacterPrefabs();
        
        // Configure build settings
        ConfigureBuildSettings();
        
        EditorUtility.DisplayDialog("Eclipse Reborn Mobile Setup Complete!", 
            "✅ Mobile RPG setup finished!\n\n" +
            "Features created:\n" +
            "• Mobile-optimized graphics settings\n" +
            "• Virtual joystick controls\n" +
            "• Beautiful character visuals\n" +
            "• Android build settings\n" +
            "• Complete game scene\n\n" +
            "Press Play to test with mouse/keyboard!\n" +
            "Controls: WASD or drag joystick, Space/Q/W for combat", "OK");
    }

    private static void ConfigureProjectForMobile()
    {
        // Set platform to Android
        EditorUserBuildSettings.selectedBuildTargetGroup = BuildTargetGroup.Android;
        
        // Configure player settings for mobile
        PlayerSettings.companyName = "Eclipse Games";
        PlayerSettings.productName = "Eclipse Reborn";
        PlayerSettings.applicationIdentifier = "com.eclipsegames.eclipsereborn";
        PlayerSettings.bundleVersion = "1.0.0";
        
        // Android specific settings
        PlayerSettings.Android.bundleVersionCode = 1;
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
        
        // Graphics settings for mobile
        PlayerSettings.colorSpace = ColorSpace.Gamma; // Better performance on mobile
        
        // Performance settings
        QualitySettings.vSyncCount = 1;
        QualitySettings.antiAliasing = 0; // Disable for mobile performance
        
        Debug.Log("✅ Mobile project configuration complete");
    }

    private static void CreateMainGameScene()
    {
        // Create new scene
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "MobileRPG";

        // Setup camera for mobile
        var cameraGO = new GameObject("Main Camera");
        var cam = cameraGO.AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 6f; // Slightly larger for mobile
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.1f, 0.2f, 0.4f, 1f); // Dark blue
        cameraGO.transform.position = new Vector3(0f, 0f, -10f);
        cameraGO.tag = "MainCamera";
        cameraGO.AddComponent<AudioListener>();

        // Create background
        CreateBackground();
        
        // Create player with visual character
        CreatePlayerWithVisuals();
        
        // Create enemies with visuals
        CreateEnemiesWithVisuals();
        
        // Create mobile joystick object
        CreateMobileJoystick();

        // Save scene
        Directory.CreateDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), "Assets/Scenes/MobileRPG.unity");
        
        Debug.Log("✅ Mobile game scene created with full visuals");
    }

    private static void CreateBackground()
    {
        // Try to use the forest background
        string backgroundPath = "Assets/Sprites/Environment/Combat_forest_background_e6c04d00.png";
        Sprite bgSprite = AssetDatabase.LoadAssetAtPath<Sprite>(backgroundPath);
        
        if (bgSprite != null)
        {
            var bgGO = new GameObject("Background");
            var bgSR = bgGO.AddComponent<SpriteRenderer>();
            bgSR.sprite = bgSprite;
            bgSR.sortingOrder = -10;
            
            // Scale to fit screen
            bgGO.transform.localScale = new Vector3(3f, 3f, 1f);
            bgGO.transform.position = new Vector3(0f, 0f, 1f);
        }
        else
        {
            // Fallback: create colored background
            var bgGO = GameObject.CreatePrimitive(PrimitiveType.Quad);
            bgGO.name = "Background";
            bgGO.transform.localScale = new Vector3(25f, 20f, 1f);
            bgGO.transform.position = new Vector3(0f, 0f, 1f);
            
            var bgRenderer = bgGO.GetComponent<MeshRenderer>();
            bgRenderer.material = new Material(Shader.Find("Sprites/Default"));
            bgRenderer.material.color = new Color(0.2f, 0.4f, 0.2f, 1f); // Forest green
        }
    }

    private static void CreatePlayerWithVisuals()
    {
        var player = new GameObject("Player_Auron");
        player.transform.position = Vector3.zero;
        player.tag = "Player";
        
        // Add visual components
        var sr = player.AddComponent<SpriteRenderer>();
        sr.sortingOrder = 1;
        
        // Try to load Auron sprite
        string auronPath = "Assets/Sprites/Characters/Auron_hero_character_sprite_8cfb17d1.png";
        Sprite auronSprite = AssetDatabase.LoadAssetAtPath<Sprite>(auronPath);
        
        if (auronSprite != null)
        {
            sr.sprite = auronSprite;
            sr.color = Color.white;
            player.transform.localScale = new Vector3(1.5f, 1.5f, 1f); // Make character bigger
        }
        else
        {
            sr.color = new Color(1f, 0.8f, 0f, 1f); // Golden fallback
            player.transform.localScale = new Vector3(1f, 2f, 1f); // Tall rectangle
        }
        
        // Add physics and collision
        var rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.linearDamping = 5f; // Mobile-friendly movement
        
        var collider = player.AddComponent<CapsuleCollider2D>();
        collider.size = new Vector2(0.8f, 1.2f);
        
        // Add mobile game components
        player.AddComponent<MobilePlayerController>();
        player.AddComponent<CombatSystem>();
        
        // Create attack point
        var attackPoint = new GameObject("AttackPoint");
        attackPoint.transform.SetParent(player.transform);
        attackPoint.transform.localPosition = new Vector3(1f, 0f, 0f);
        
        Debug.Log("✅ Player with Auron visuals created");
    }

    private static void CreateEnemiesWithVisuals()
    {
        // Create Shade enemy
        var enemy1 = new GameObject("Enemy_Shade");
        enemy1.transform.position = new Vector3(4f, 2f, 0f);
        
        var enemySR1 = enemy1.AddComponent<SpriteRenderer>();
        enemySR1.sortingOrder = 1;
        
        string shadePath = "Assets/Sprites/Enemies/Basic_shade_enemy_sprite_a04ae306.png";
        Sprite shadeSprite = AssetDatabase.LoadAssetAtPath<Sprite>(shadePath);
        
        if (shadeSprite != null)
        {
            enemySR1.sprite = shadeSprite;
            enemy1.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        }
        else
        {
            enemySR1.color = new Color(0.4f, 0f, 0.8f, 1f); // Purple fallback
            enemy1.transform.localScale = new Vector3(0.8f, 1.5f, 1f);
        }
        
        enemy1.AddComponent<CapsuleCollider2D>();
        enemy1.AddComponent<CombatTarget>();
        enemy1.AddComponent<SimpleEnemyAI>();
        
        // Create Boss enemy
        var boss = new GameObject("Boss_Gorath");
        boss.transform.position = new Vector3(-4f, -2f, 0f);
        
        var bossSR = boss.AddComponent<SpriteRenderer>();
        bossSR.sortingOrder = 1;
        
        string gorathPath = "Assets/Sprites/Enemies/Gorath_boss_enemy_sprite_978e28a4.png";
        Sprite gorathSprite = AssetDatabase.LoadAssetAtPath<Sprite>(gorathPath);
        
        if (gorathSprite != null)
        {
            bossSR.sprite = gorathSprite;
            boss.transform.localScale = new Vector3(2f, 2f, 1f); // Big boss
        }
        else
        {
            bossSR.color = new Color(0.8f, 0f, 0f, 1f); // Red fallback
            boss.transform.localScale = new Vector3(1.5f, 2.5f, 1f); // Big rectangle
        }
        
        boss.AddComponent<CapsuleCollider2D>();
        boss.AddComponent<CombatTarget>();
        boss.AddComponent<SimpleEnemyAI>();
        
        Debug.Log("✅ Enemies with visuals created");
    }

    private static void CreateMobileJoystick()
    {
        // Create joystick object
        var joystickGO = new GameObject("Mobile_Joystick");
        joystickGO.AddComponent<Joystick>();
        
        Debug.Log("✅ Mobile joystick created");
    }

    private static void SetupCharacterPrefabs()
    {
        string[] characters = { "Auron", "Cira", "Lyra", "Milo", "Ronan" };
        string[] spriteFiles = { 
            "Auron_hero_character_sprite_8cfb17d1.png",
            "Cira_mage_character_sprite_84c9f07c.png", 
            "Lyra_healer_character_sprite_96d4ac30.png",
            "Milo_rogue_character_sprite_6bf87aa4.png",
            "Ronan_tank_character_sprite_09614f5a.png"
        };
        
        Directory.CreateDirectory("Assets/Prefabs");
        
        for (int i = 0; i < characters.Length; i++)
        {
            CreateCharacterPrefab(characters[i], spriteFiles[i]);
        }
        
        Debug.Log("✅ All character prefabs created");
    }

    private static void CreateCharacterPrefab(string characterName, string spriteFile)
    {
        var character = new GameObject(characterName);
        
        var sr = character.AddComponent<SpriteRenderer>();
        string spritePath = $"Assets/Sprites/Characters/{spriteFile}";
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
        
        character.AddComponent<Rigidbody2D>().gravityScale = 0f;
        character.AddComponent<CapsuleCollider2D>();
        character.AddComponent<MobilePlayerController>();
        character.AddComponent<CombatSystem>();
        
        // Save as prefab
        string prefabPath = $"Assets/Prefabs/{characterName}.prefab";
        PrefabUtility.SaveAsPrefabAsset(character, prefabPath);
        
        // Clean up scene
        Object.DestroyImmediate(character);
    }

    private static void ConfigureBuildSettings()
    {
        // Add scenes to build settings
        var scenes = new EditorBuildSettingsScene[]
        {
            new EditorBuildSettingsScene("Assets/Scenes/MobileRPG.unity", true)
        };
        EditorBuildSettings.scenes = scenes;
        
        // Set orientation to portrait for mobile
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
        
        Debug.Log("✅ Build settings configured for Android");
    }

    [MenuItem("Eclipse Reborn/Build Android APK")]
    public static void BuildAndroidAPK()
    {
        string buildPath = EditorUtility.SaveFolderPanel("Choose Build Location", "", "");
        if (string.IsNullOrEmpty(buildPath)) return;
        
        BuildPlayerOptions buildOptions = new BuildPlayerOptions();
        buildOptions.scenes = new[] { "Assets/Scenes/MobileRPG.unity" };
        buildOptions.locationPathName = buildPath + "/EclipseReborn.apk";
        buildOptions.target = BuildTarget.Android;
        buildOptions.options = BuildOptions.None;
        
        var result = BuildPipeline.BuildPlayer(buildOptions);
        
        if (result.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            EditorUtility.DisplayDialog("Build Successful!", 
                $"Eclipse Reborn APK built successfully!\n\nLocation: {buildOptions.locationPathName}\n\n" +
                "You can now install this APK on Android devices!", "OK");
        }
        else
        {
            EditorUtility.DisplayDialog("Build Failed", "Build failed. Check console for errors.", "OK");
        }
    }
}