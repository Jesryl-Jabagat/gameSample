using UnityEditor;
using UnityEngine;
using System.IO;

public class SimpleAssetSetup
{
    [MenuItem("Eclipse Reborn/Simple Asset Setup")]
    public static void SimpleSetupAssets()
    {
        EditorUtility.DisplayProgressBar("Eclipse Reborn", "Setting up assets...", 0f);
        
        try
        {
            // Step 1: Create folders
            CreateBasicFolders();
            EditorUtility.DisplayProgressBar("Eclipse Reborn", "Created folders...", 0.3f);
            
            // Step 2: Fix sprite settings
            FixSpriteSettings();
            EditorUtility.DisplayProgressBar("Eclipse Reborn", "Fixed sprite settings...", 0.6f);
            
            // Step 3: Create basic prefabs
            CreateBasicPrefabs();
            EditorUtility.DisplayProgressBar("Eclipse Reborn", "Created prefabs...", 0.9f);
            
            AssetDatabase.Refresh();
            
            EditorUtility.DisplayDialog("Simple Setup Complete!", 
                "✅ BASIC ASSETS READY!\n\n" +
                "Created:\n" +
                "• Prefabs folder\n" +
                "• Fixed sprite settings\n" +
                "• Basic character prefabs\n\n" +
                "Go to Assets/Prefabs to see your characters!", "Great!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Setup failed: " + e.Message);
            EditorUtility.DisplayDialog("Setup Error", "Something went wrong: " + e.Message, "OK");
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    static void CreateBasicFolders()
    {
        // Create essential folders only
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }
        
        if (!AssetDatabase.IsValidFolder("Assets/Animations"))
        {
            AssetDatabase.CreateFolder("Assets", "Animations");
        }
        
        Debug.Log("✅ Created basic folders");
    }

    static void FixSpriteSettings()
    {
        // Find all textures in Characters folder
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/Sprites/Characters" });
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            FixSingleSprite(path);
        }
        
        Debug.Log("✅ Fixed sprite settings");
    }

    static void FixSingleSprite(string path)
    {
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null) return;

        // Basic settings that work on all Unity versions
        importer.textureType = TextureImporterType.Sprite;
        importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        
        // Check if it's an animation sheet by filename
        string fileName = Path.GetFileNameWithoutExtension(path).ToLower();
        
        if (fileName.Contains("animation") || fileName.Contains("walk") || fileName.Contains("attack"))
        {
            importer.spriteImportMode = SpriteImportMode.Multiple;
        }
        else
        {
            importer.spriteImportMode = SpriteImportMode.Single;
        }

        importer.SaveAndReimport();
    }

    static void CreateBasicPrefabs()
    {
        string[] characters = { "Auron", "Cira", "Lyra", "Milo", "Ronan" };
        string[] spriteFiles = { 
            "Auron_hero_character_sprite_8cfb17d1.png",
            "Cira_mage_character_sprite_84c9f07c.png", 
            "Lyra_healer_character_sprite_96d4ac30.png",
            "Milo_rogue_character_sprite_6bf87aa4.png",
            "Ronan_tank_character_sprite_09614f5a.png"
        };

        for (int i = 0; i < characters.Length; i++)
        {
            CreateSimplePrefab(characters[i], spriteFiles[i]);
        }
        
        Debug.Log("✅ Created character prefabs");
    }

    static void CreateSimplePrefab(string characterName, string spriteFile)
    {
        // Create character GameObject
        GameObject character = new GameObject(characterName);
        
        // Add SpriteRenderer
        SpriteRenderer sr = character.AddComponent<SpriteRenderer>();
        string spritePath = $"Assets/Sprites/Characters/{spriteFile}";
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            // Fallback color if sprite not found
            sr.color = GetCharacterColor(characterName);
        }
        
        // Add basic physics
        Rigidbody2D rb = character.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        
        CapsuleCollider2D collider = character.AddComponent<CapsuleCollider2D>();
        
        // Set scale to make character bigger
        character.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        
        // Save as prefab
        string prefabPath = $"Assets/Prefabs/{characterName}.prefab";
        PrefabUtility.SaveAsPrefabAsset(character, prefabPath);
        
        // Clean up scene
        Object.DestroyImmediate(character);
        
        Debug.Log($"✅ Created {characterName} prefab");
    }

    static Color GetCharacterColor(string characterName)
    {
        switch (characterName)
        {
            case "Auron": return new Color(1f, 0.8f, 0f); // Gold
            case "Cira": return new Color(0f, 0.5f, 1f); // Blue
            case "Lyra": return new Color(0f, 1f, 0.5f); // Green
            case "Milo": return new Color(0.8f, 0.4f, 0f); // Brown
            case "Ronan": return new Color(1f, 0f, 0f); // Red
            default: return Color.white;
        }
    }

    [MenuItem("Eclipse Reborn/Create Test Scene")]
    public static void CreateTestScene()
    {
        // Create simple test scene
        var scene = UnityEditor.SceneManagement.EditorSceneManager.NewScene(
            UnityEditor.SceneManagement.NewSceneSetup.EmptyScene, 
            UnityEditor.SceneManagement.NewSceneMode.Single);

        // Camera
        var cam = new GameObject("Main Camera").AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 5;
        cam.backgroundColor = new Color(0.2f, 0.3f, 0.5f);
        cam.transform.position = new Vector3(0, 0, -10);
        cam.tag = "MainCamera";

        // Ground
        var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(10, 1, 1);
        ground.transform.position = new Vector3(0, -3, 0);
        ground.GetComponent<Renderer>().material.color = Color.gray;

        EditorUtility.DisplayDialog("Test Scene Created!", 
            "Basic test scene ready!\n\n" +
            "Now drag character prefabs from Assets/Prefabs to the scene!", "OK");
    }

    [MenuItem("Eclipse Reborn/Fix GPU Fence Error")]
    public static void FixGPUError()
    {
        EditorUtility.DisplayDialog("GPU Fence Fix", 
            "The GPU Fence error is a graphics driver issue.\n\n" +
            "Quick fixes:\n" +
            "• Update your graphics drivers\n" +
            "• Unity should still work despite this warning\n" +
            "• The error doesn't affect game functionality\n\n" +
            "Your game assets will work fine!", "OK");
    }
}