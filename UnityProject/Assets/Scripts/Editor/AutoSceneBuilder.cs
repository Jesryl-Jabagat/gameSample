using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AutoSceneBuilder
{
    [MenuItem("Eclipse Reborn/Setup Simple Demo Scene")]
    public static void SetupSimpleDemo()
    {
        // Create new 2D scene
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "SimpleDemo";

        // Main Camera with proper 2D setup
        var cameraGO = new GameObject("Main Camera");
        var cam = cameraGO.AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 5f;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.2f, 0.3f, 0.5f, 1f); // Light blue background
        cameraGO.transform.position = new Vector3(0f, 0f, -10f);
        cameraGO.tag = "MainCamera";
        
        // Add AudioListener
        cameraGO.AddComponent<AudioListener>();

        // Ground (simple visual reference)
        var ground = GameObject.CreatePrimitive(PrimitiveType.Quad);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(20f, 1f, 1f);
        ground.transform.position = new Vector3(0f, -3f, 0f);
        var groundRenderer = ground.GetComponent<MeshRenderer>();
        if (groundRenderer != null)
        {
            groundRenderer.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
            groundRenderer.sharedMaterial.color = new Color(0.16f, 0.16f, 0.24f);
        }

        // Player
        var player = new GameObject("Player_Auron");
        player.transform.position = Vector3.zero;
        player.transform.localScale = new Vector3(1f, 1.5f, 1f);
        player.tag = "Player";
        
        var rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.AddComponent<CapsuleCollider2D>();

        var sr = player.AddComponent<SpriteRenderer>();
        sr.color = new Color(1f, 0.84f, 0f); // gold placeholder

        var combat = player.AddComponent<CombatSystem>();
        var pc = player.AddComponent<PlayerController>();

        // Create AttackPoint child
        var attackPoint = new GameObject("AttackPoint");
        attackPoint.transform.SetParent(player.transform);
        attackPoint.transform.localPosition = new Vector3(1f, 0f, 0f);

        // Enemy
        var enemy = new GameObject("Enemy_Shade");
        enemy.transform.position = new Vector3(5f, 0f, 0f);
        var enemyRb = enemy.AddComponent<Rigidbody2D>();
        enemyRb.gravityScale = 0f;
        enemy.AddComponent<CapsuleCollider2D>();
        var enemySr = enemy.AddComponent<SpriteRenderer>();
        enemySr.color = new Color(0.55f, 0f, 1f); // purple placeholder
        enemy.AddComponent<CombatTarget>();
        enemy.AddComponent<SimpleEnemyAI>();

        // Save scene
        System.IO.Directory.CreateDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), "Assets/Scenes/SimpleDemo.unity");

        EditorUtility.DisplayDialog("Eclipse Reborn", 
            "Simple demo scene created!\n\n" +
            "Controls:\n" +
            "- WASD: Move\n" +
            "- Space: Attack\n" +
            "- Q/W: Skills\n" +
            "- Shift: Dash\n\n" +
            "Press Play to test!", "OK");
    }

    [MenuItem("Eclipse Reborn/Fix Black Screen")]
    public static void FixBlackScreen()
    {
        // Find the main camera
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            mainCam = Object.FindFirstObjectByType<Camera>();
        }
        
        if (mainCam != null)
        {
            mainCam.clearFlags = CameraClearFlags.SolidColor;
            mainCam.backgroundColor = new Color(0.2f, 0.3f, 0.5f, 1f);
            mainCam.orthographic = true;
            mainCam.orthographicSize = 5f;
            mainCam.transform.position = new Vector3(0f, 0f, -10f);
            
            EditorUtility.DisplayDialog("Fixed!", "Camera settings fixed! You should now see a blue background instead of black.", "OK");
        }
        else
        {
            EditorUtility.DisplayDialog("No Camera", "No camera found in scene. Try creating a new demo scene.", "OK");
        }
    }

    [MenuItem("Eclipse Reborn/Show Character Sprites")]
    public static void ShowCharacterSprites()
    {
        Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Characters", typeof(Object));
        EditorGUIUtility.PingObject(Selection.activeObject);
        
        EditorUtility.DisplayDialog("Eclipse Reborn Characters", 
            "Character sprites are now selected in Project window!\n\n" +
            "Available characters:\n" +
            "• Auron (Golden Warrior)\n" +
            "• Cira (Blue Mage)\n" +
            "• Lyra (Green Healer)\n" +
            "• Milo (Brown Rogue)\n" +
            "• Ronan (Red Tank)\n\n" +
            "Drag any sprite to Scene view to see it!", "OK");
    }
}