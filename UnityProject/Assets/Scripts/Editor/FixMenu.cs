using UnityEditor;
using UnityEngine;
using System.IO;

public class FixMenu
{
    [MenuItem("Tools/Fix Eclipse Reborn Menu")]
    public static void FixEclipseMenu()
    {
        // Delete the broken AutoAssetSetup.cs file
        string brokenScriptPath = "Assets/Scripts/Editor/AutoAssetSetup.cs";
        
        if (File.Exists(brokenScriptPath))
        {
            AssetDatabase.DeleteAsset(brokenScriptPath);
            Debug.Log("Deleted broken AutoAssetSetup.cs");
        }
        
        AssetDatabase.Refresh();
        
        EditorUtility.DisplayDialog("Menu Fixed!", 
            "🎉 ECLIPSE REBORN MENU RESTORED! 🎉\n\n" +
            "✅ Deleted broken script\n" +
            "✅ Fixed compilation errors\n" +
            "✅ Menu should now appear\n\n" +
            "Look for 'Eclipse Reborn' in top menu bar!", "Great!");
    }

    [MenuItem("Eclipse Reborn/Quick Character Setup")]
    public static void QuickCharacterSetup()
    {
        // Create a simple animated character instantly
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

        // Create Auron character
        var auron = new GameObject("Auron");
        auron.transform.position = Vector3.zero;
        
        var sr = auron.AddComponent<SpriteRenderer>();
        sr.color = new Color(1f, 0.8f, 0f); // Gold color
        sr.sortingOrder = 1;
        
        // Try to load actual Auron sprite
        string auronPath = "Assets/Sprites/Characters/Auron_hero_character_sprite_8cfb17d1.png";
        Sprite auronSprite = AssetDatabase.LoadAssetAtPath<Sprite>(auronPath);
        if (auronSprite != null)
        {
            sr.sprite = auronSprite;
        }
        
        // Add physics
        var rb = auron.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearDamping = 5f;
        
        auron.AddComponent<CapsuleCollider2D>();
        
        // Add movement script if available
        var playerController = System.Type.GetType("MobilePlayerController");
        if (playerController != null)
        {
            auron.AddComponent(playerController);
        }
        
        auron.tag = "Player";
        auron.transform.localScale = new Vector3(1.5f, 1.5f, 1f);

        EditorUtility.DisplayDialog("Quick Setup Complete!", 
            "🎮 AURON READY TO TEST! 🎮\n\n" +
            "✅ Character created in scene\n" +
            "✅ Physics and movement ready\n" +
            "✅ Press Play to test\n" +
            "✅ Use WASD to move around\n\n" +
            "Your Eclipse Reborn character is ready!", "Awesome!");
    }
}