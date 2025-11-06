using UnityEngine;

/// <summary>
/// Simple virtual joystick for mobile without UI dependencies
/// Uses mouse/touch input directly
/// </summary>
public class Joystick : MonoBehaviour
{
    [Header("Joystick Settings")]
    public float handleRange = 1f;
    public float deadZone = 0.1f;
    public float joystickRadius = 100f;
    
    [Header("Visual Settings")]
    public Color backgroundColor = new Color(1f, 1f, 1f, 0.3f);
    public Color handleColor = new Color(1f, 1f, 1f, 0.8f);
    
    private Vector2 input = Vector2.zero;
    private Vector2 center;
    private bool isDragging = false;
    private Camera playerCamera;
    private Vector2 screenCenter;
    
    public float Horizontal => input.x;
    public float Vertical => input.y;
    public Vector2 Direction => input;

    void Start()
    {
        playerCamera = Camera.main;
        if (playerCamera == null)
            playerCamera = FindFirstObjectByType<Camera>();

        // Position joystick in bottom-left corner
        screenCenter = new Vector2(joystickRadius + 50f, joystickRadius + 50f);
        center = screenCenter;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Handle mouse/touch input
        bool inputDown = Input.GetMouseButtonDown(0);
        bool inputHeld = Input.GetMouseButton(0);
        bool inputUp = Input.GetMouseButtonUp(0);
        
        Vector2 inputPosition = Input.mousePosition;

        if (inputDown)
        {
            // Check if touch is within joystick area
            float distance = Vector2.Distance(inputPosition, center);
            if (distance <= joystickRadius)
            {
                isDragging = true;
                UpdateJoystick(inputPosition);
            }
        }
        else if (inputHeld && isDragging)
        {
            UpdateJoystick(inputPosition);
        }
        else if (inputUp || !inputHeld)
        {
            // Reset joystick
            isDragging = false;
            input = Vector2.zero;
        }
    }

    void UpdateJoystick(Vector2 inputPosition)
    {
        Vector2 direction = inputPosition - center;
        float distance = direction.magnitude;
        
        // Clamp to joystick radius
        if (distance > joystickRadius)
        {
            direction = direction.normalized * joystickRadius;
            distance = joystickRadius;
        }
        
        // Calculate input value
        input = direction / joystickRadius;
        
        // Apply dead zone
        if (input.magnitude < deadZone)
        {
            input = Vector2.zero;
        }
    }

    void OnGUI()
    {
        // Draw joystick background
        Vector2 bgPos = new Vector2(center.x - joystickRadius, Screen.height - center.y - joystickRadius);
        Rect bgRect = new Rect(bgPos.x, bgPos.y, joystickRadius * 2, joystickRadius * 2);
        
        GUI.color = backgroundColor;
        GUI.DrawTexture(bgRect, Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 0, backgroundColor, 0, joystickRadius);
        
        // Draw joystick handle
        Vector2 handlePos = center + (input * joystickRadius * handleRange);
        Vector2 handleScreenPos = new Vector2(handlePos.x - 25f, Screen.height - handlePos.y - 25f);
        Rect handleRect = new Rect(handleScreenPos.x, handleScreenPos.y, 50f, 50f);
        
        GUI.color = handleColor;
        GUI.DrawTexture(handleRect, Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 0, handleColor, 0, 25f);
        
        GUI.color = Color.white; // Reset GUI color
    }

    /// <summary>
    /// Get normalized input for movement
    /// </summary>
    public Vector2 GetInputVector()
    {
        return input;
    }

    /// <summary>
    /// Check if joystick is being used
    /// </summary>
    public bool IsActive()
    {
        return input.magnitude > deadZone;
    }

    /// <summary>
    /// Set joystick position on screen
    /// </summary>
    public void SetPosition(Vector2 position)
    {
        center = position;
        screenCenter = position;
    }
}