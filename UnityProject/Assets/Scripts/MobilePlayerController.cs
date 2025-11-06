using UnityEngine;

/// <summary>
/// Mobile-optimized PlayerController for Eclipse Reborn
/// Supports both keyboard and mobile touch controls
/// </summary>
public class MobilePlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 12f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashCooldown = 2f;

    [Header("Combat Settings")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Mobile Controls")]
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private KeyCode attackKey = KeyCode.Space;
    [SerializeField] private KeyCode skill1Key = KeyCode.Q;
    [SerializeField] private KeyCode skill2Key = KeyCode.W;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;

    private Vector2 moveInput;
    private bool isDashing;
    private bool canDash = true;
    private bool isAttacking;
    private bool canAttack = true;
    
    private float lastDashTime;
    private float lastAttackTime;

    private HeroData currentHeroData;
    private CombatSystem combatSystem;

    // Animation parameter hashes for performance
    private static readonly int AnimSpeed = Animator.StringToHash("Speed");
    private static readonly int AnimAttack = Animator.StringToHash("Attack");
    private static readonly int AnimSkill1 = Animator.StringToHash("Skill1");
    private static readonly int AnimSkill2 = Animator.StringToHash("Skill2");

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (combatSystem == null) combatSystem = GetComponent<CombatSystem>();
        
        // Find mobile joystick if not assigned
        if (movementJoystick == null)
            movementJoystick = FindFirstObjectByType<Joystick>();
    }

    private void Start()
    {
        SetupMobileControls();
        Debug.Log("Mobile PlayerController initialized");
        Debug.Log("Controls: Joystick for movement, buttons for combat");
    }

    private void SetupMobileControls()
    {
        // Mobile controls setup
        Debug.Log("Mobile controls configured for keyboard input");
        Debug.Log($"Attack: {attackKey}, Skill1: {skill1Key}, Skill2: {skill2Key}");
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        UpdateAnimations();
    }

    private void HandleInput()
    {
        // Get input from mobile joystick or keyboard
        if (movementJoystick != null && movementJoystick.IsActive())
        {
            moveInput = movementJoystick.Direction;
        }
        else
        {
            // Fallback to keyboard for testing in editor
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            moveInput = new Vector2(horizontal, vertical).normalized;
        }

        // Keyboard input for combat
        if (Input.GetKeyDown(attackKey) && canAttack)
        {
            PerformAttack();
        }

        if (Input.GetKeyDown(skill1Key))
        {
            PerformSkill1();
        }

        if (Input.GetKeyDown(skill2Key))
        {
            PerformSkill2();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            PerformDash();
        }
    }

    private void HandleMovement()
    {
        if (isDashing) return;

        // Apply movement
        Vector2 movement = moveInput * moveSpeed;
        rb.linearVelocity = movement;

        // Face movement direction
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(moveInput.x > 0 ? 1 : -1, 1, 1);
        }
    }

    private void UpdateAnimations()
    {
        if (animator != null)
        {
            animator.SetFloat(AnimSpeed, moveInput.magnitude);
        }
    }

    public void PerformAttack()
    {
        if (!canAttack || isAttacking) return;

        isAttacking = true;
        canAttack = false;
        lastAttackTime = Time.time;

        Debug.Log("Player attacks!");

        if (animator != null)
        {
            animator.SetTrigger(AnimAttack);
        }

        // Check for enemies in attack range
        if (attackPoint != null)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in enemies)
            {
                var enemyTarget = enemy.GetComponent<CombatTarget>();
                if (enemyTarget != null)
                {
                    enemyTarget.TakeDamage(25);
                    Debug.Log("Hit enemy for 25 damage!");
                }

                var enemyAI = enemy.GetComponent<SimpleEnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.TakeDamage(25);
                }
            }
        }

        // Reset attack state
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        isAttacking = false;
        canAttack = true;
    }

    public void PerformSkill1()
    {
        Debug.Log("Player uses Skill 1!");
        
        if (animator != null)
        {
            animator.SetTrigger(AnimSkill1);
        }

        // Add skill 1 logic here (e.g., healing, buff, special attack)
        if (combatSystem != null)
        {
            // Example: Apply healing
            Debug.Log("Healing spell cast!");
        }
    }

    public void PerformSkill2()
    {
        Debug.Log("Player uses Skill 2!");
        
        if (animator != null)
        {
            animator.SetTrigger(AnimSkill2);
        }

        // Add skill 2 logic here (e.g., area attack, shield)
        if (combatSystem != null)
        {
            // Example: Area attack
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 3f, enemyLayer);
            foreach (Collider2D enemy in enemies)
            {
                var enemyTarget = enemy.GetComponent<CombatTarget>();
                if (enemyTarget != null)
                {
                    enemyTarget.TakeDamage(15);
                }
            }
            Debug.Log("Area attack performed!");
        }
    }

    public void PerformDash()
    {
        if (!canDash || isDashing) return;

        StartCoroutine(DashCoroutine());
    }

    private System.Collections.IEnumerator DashCoroutine()
    {
        isDashing = true;
        canDash = false;
        lastDashTime = Time.time;

        Debug.Log("Player dashes!");

        Vector2 dashDirection = moveInput != Vector2.zero ? moveInput : Vector2.right;
        float dashTime = 0f;

        while (dashTime < dashDuration)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            dashTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        rb.linearVelocity = Vector2.zero;

        // Reset dash cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void SetHeroData(HeroData heroData)
    {
        currentHeroData = heroData;
        Debug.Log($"Hero set: {heroData.heroName}");
        
        // Update stats based on hero data
        moveSpeed = heroData.baseMoveSpeed;
        // Add other stat applications here
    }

    // Public methods for mobile UI (can be called by touch buttons)
    public void OnAttackButtonPress()
    {
        PerformAttack();
    }

    public void OnSkill1ButtonPress()
    {
        PerformSkill1();
    }

    public void OnSkill2ButtonPress()
    {
        PerformSkill2();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        
        // Draw skill 2 area attack range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}