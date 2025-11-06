using UnityEngine;

/// <summary>
/// PlayerController handles touch input, movement, combat actions, and animation control for player heroes.
/// Optimized for mobile with touch joystick, skill buttons, and animation parameter management.
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

    [Header("UI References - Assign in Inspector")]
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private UnityEngine.UI.Button attackButton;
    [SerializeField] private UnityEngine.UI.Button skill1Button;
    [SerializeField] private UnityEngine.UI.Button skill2Button;
    [SerializeField] private UnityEngine.UI.Button dashButton;

    private Vector2 moveInput;
    private bool isDashing;
    private bool canDash = true;
    private bool isAttacking;
    private bool canAttack = true;
    
    private float lastDashTime;
    private float lastAttackTime;

    private HeroData currentHeroData;
    private CombatSystem combatSystem;

    private static readonly int AnimSpeed = Animator.StringToHash("Speed");
    private static readonly int AnimAttack = Animator.StringToHash("Attack");
    private static readonly int AnimSkill1 = Animator.StringToHash("Skill1");
    private static readonly int AnimSkill2 = Animator.StringToHash("Skill2");
    private static readonly int AnimDash = Animator.StringToHash("Dash");
    private static readonly int AnimHit = Animator.StringToHash("Hit");
    private static readonly int AnimDeath = Animator.StringToHash("Death");

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (combatSystem == null) combatSystem = GetComponent<CombatSystem>();
    }

    private void Start()
    {
        SetupInputHandlers();
    }

    /// <summary>
    /// Initialize touch input listeners for combat buttons.
    /// Mobile-optimized with direct button event binding.
    /// </summary>
    private void SetupInputHandlers()
    {
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(OnAttackButtonPressed);
        }

        if (skill1Button != null)
        {
            skill1Button.onClick.AddListener(OnSkill1ButtonPressed);
        }

        if (skill2Button != null)
        {
            skill2Button.onClick.AddListener(OnSkill2ButtonPressed);
        }

        if (dashButton != null)
        {
            dashButton.onClick.AddListener(OnDashButtonPressed);
        }
    }

    private void Update()
    {
        if (isDashing || isAttacking) return;

        HandleMovementInput();
        UpdateAnimationParameters();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isAttacking)
        {
            Move();
        }
    }

    /// <summary>
    /// Read joystick input for 8-directional movement.
    /// Supports both virtual joystick and keyboard fallback for testing.
    /// </summary>
    private void HandleMovementInput()
    {
        if (movementJoystick != null)
        {
            moveInput = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
        }
        else
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        moveInput = moveInput.normalized;

        if (moveInput.x != 0)
        {
            FlipSprite(moveInput.x < 0);
        }
    }

    /// <summary>
    /// Apply movement using Rigidbody2D for physics-based collision.
    /// </summary>
    private void Move()
    {
        Vector2 movement = moveInput * moveSpeed;
        rb.velocity = movement;
    }

    /// <summary>
    /// Perform dash in current movement direction.
    /// Includes i-frames during dash (can be extended with invulnerability logic).
    /// </summary>
    private void OnDashButtonPressed()
    {
        if (!canDash || isDashing || moveInput == Vector2.zero) return;

        StartCoroutine(DashCoroutine());
    }

    private System.Collections.IEnumerator DashCoroutine()
    {
        isDashing = true;
        canDash = false;
        animator.SetTrigger(AnimDash);

        Vector2 dashDirection = moveInput.normalized;
        float dashTimer = 0f;

        while (dashTimer < dashDuration)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTimer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    /// <summary>
    /// Handle basic attack button press.
    /// Attack applies damage via CombatSystem in animation event (OnAttackHit).
    /// </summary>
    private void OnAttackButtonPressed()
    {
        if (!canAttack || isAttacking) return;

        StartCoroutine(AttackCoroutine());
    }

    private System.Collections.IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        canAttack = false;
        rb.velocity = Vector2.zero;

        animator.SetTrigger(AnimAttack);

        // Trigger hit even without animation events (editor/testing convenience)
        yield return new WaitForSeconds(Mathf.Max(0.1f, attackCooldown * 0.4f));
        OnAttackHit();

        float remaining = Mathf.Max(0f, attackCooldown - Mathf.Max(0.1f, attackCooldown * 0.4f));
        if (remaining > 0f)
        {
            yield return new WaitForSeconds(remaining);
        }

        isAttacking = false;
        canAttack = true;
    }

    /// <summary>
    /// Called via Animation Event on attack hit frame (frame 4 for most heroes).
    /// Detects enemies in range and applies damage.
    /// </summary>
    public void OnAttackHit()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            if (combatSystem != null)
            {
                combatSystem.DealDamage(hit.gameObject, 1.0f);
            }
        }
    }

    /// <summary>
    /// Execute Skill 1 (unique per hero, defined in HeroData).
    /// Animation triggers skill VFX; damage applied in animation event OnSkill1Hit.
    /// </summary>
    private void OnSkill1ButtonPressed()
    {
        if (currentHeroData == null || currentHeroData.skill1Cooldown > 0) return;

        StartCoroutine(Skill1Coroutine());
    }

    private System.Collections.IEnumerator Skill1Coroutine()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger(AnimSkill1);

        currentHeroData.skill1Cooldown = currentHeroData.skill1CooldownMax;

        yield return new WaitForSeconds(currentHeroData.skill1CastTime);
    }

    /// <summary>
    /// Called via Animation Event on skill 1 hit frame.
    /// </summary>
    public void OnSkill1Hit()
    {
        if (currentHeroData == null) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, currentHeroData.skill1Range, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            if (combatSystem != null)
            {
                combatSystem.DealDamage(hit.gameObject, currentHeroData.skill1Multiplier);
            }
        }
    }

    /// <summary>
    /// Execute Skill 2 / Ultimate (charges via combat, defined in HeroData).
    /// Animation triggers ultimate VFX; damage applied in animation event OnSkill2Hit.
    /// </summary>
    private void OnSkill2ButtonPressed()
    {
        if (currentHeroData == null || currentHeroData.skill2Cooldown > 0) return;

        StartCoroutine(Skill2Coroutine());
    }

    private System.Collections.IEnumerator Skill2Coroutine()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger(AnimSkill2);

        currentHeroData.skill2Cooldown = currentHeroData.skill2CooldownMax;

        yield return new WaitForSeconds(currentHeroData.skill2CastTime);
    }

    /// <summary>
    /// Called via Animation Event on skill 2 hit frame.
    /// </summary>
    public void OnSkill2Hit()
    {
        if (currentHeroData == null) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, currentHeroData.skill2Range, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            if (combatSystem != null)
            {
                combatSystem.DealDamage(hit.gameObject, currentHeroData.skill2Multiplier, true);
            }
        }
    }

    /// <summary>
    /// Update Animator parameters for blend tree transitions.
    /// Speed controls idle/walk/run blend.
    /// </summary>
    private void UpdateAnimationParameters()
    {
        float speed = moveInput.magnitude;
        animator.SetFloat(AnimSpeed, speed);
    }

    /// <summary>
    /// Flip sprite based on movement direction (for 2D side view).
    /// </summary>
    private void FlipSprite(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    /// <summary>
    /// Initialize hero data (called when hero is spawned or switched).
    /// </summary>
    public void SetHeroData(HeroData heroData)
    {
        currentHeroData = heroData;
    }

    /// <summary>
    /// Trigger hit animation when taking damage (called by CombatSystem).
    /// </summary>
    public void OnTakeDamage()
    {
        animator.SetTrigger(AnimHit);
    }

    /// <summary>
    /// Trigger death animation and disable controls.
    /// </summary>
    public void OnDeath()
    {
        animator.SetTrigger(AnimDeath);
        enabled = false;
        rb.velocity = Vector2.zero;
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

/// <summary>
/// HeroData holds hero-specific stats and skill parameters.
/// Loaded from hero_templates.json at runtime.
/// </summary>
[System.Serializable]
public class HeroData
{
    public string heroId;
    public string heroName;
    public float hp;
    public float maxHp;
    public float atk;
    public float def;
    public float spd;
    public float crit;

    public float skill1Multiplier;
    public float skill1Range;
    public float skill1CastTime;
    public float skill1CooldownMax;
    public float skill1Cooldown;

    public float skill2Multiplier;
    public float skill2Range;
    public float skill2CastTime;
    public float skill2CooldownMax;
    public float skill2Cooldown;
}
