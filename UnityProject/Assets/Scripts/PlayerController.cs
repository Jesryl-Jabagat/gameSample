using UnityEngine;

/// <summary>
/// PlayerController handles keyboard input, movement, combat actions, and animation control for player heroes.
/// Simplified version without UI dependencies for initial testing.
/// </summary>
public class PlayerController : MonoBehaviour
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

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;

    [Header("Input Settings")]
    [SerializeField] private KeyCode attackKey = KeyCode.Space;
    [SerializeField] private KeyCode skill1Key = KeyCode.Q;
    [SerializeField] private KeyCode skill2Key = KeyCode.W;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;

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
    private static readonly int AnimDash = Animator.StringToHash("Dash");

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (combatSystem == null) combatSystem = GetComponent<CombatSystem>();
    }

    private void Start()
    {
        Debug.Log("PlayerController initialized with keyboard controls:");
        Debug.Log($"Movement: WASD or Arrow Keys");
        Debug.Log($"Attack: {attackKey}, Skill1: {skill1Key}, Skill2: {skill2Key}, Dash: {dashKey}");
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        UpdateAnimations();
    }

    private void HandleInput()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveInput = new Vector2(horizontal, vertical).normalized;

        // Combat input
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

        if (Input.GetKeyDown(dashKey) && canDash)
        {
            PerformDash();
        }
    }

    private void HandleMovement()
    {
        if (isDashing) return;

        Vector2 movement = moveInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

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

    private void PerformAttack()
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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            // Deal damage to enemy
            var enemyAI = enemy.GetComponent<SimpleEnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.TakeDamage(25);
                Debug.Log("Hit enemy for 25 damage!");
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

    private void PerformSkill1()
    {
        Debug.Log("Player uses Skill 1!");
        
        if (animator != null)
        {
            animator.SetTrigger(AnimSkill1);
        }

        // Add skill logic here
    }

    private void PerformSkill2()
    {
        Debug.Log("Player uses Skill 2!");
        
        if (animator != null)
        {
            animator.SetTrigger(AnimSkill2);
        }

        // Add skill logic here
    }

    private void PerformDash()
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

        if (animator != null)
        {
            animator.SetTrigger(AnimDash);
        }

        Vector2 dashDirection = moveInput != Vector2.zero ? moveInput : Vector2.right;
        float dashTime = 0f;

        while (dashTime < dashDuration)
        {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
            dashTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

        // Reset dash cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void SetHeroData(HeroData heroData)
    {
        currentHeroData = heroData;
        Debug.Log($"Hero set: {heroData.heroName}");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}

// HeroData class is defined in DataClasses.cs