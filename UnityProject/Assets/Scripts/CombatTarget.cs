using UnityEngine;

[System.Serializable]
public class CombatTarget : MonoBehaviour
{
    [Header("Combat Stats")]
    public string enemyName = "Shade";
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int attackDamage = 15;
    public float attackRange = 1.5f;
    
    [Header("Visual Feedback")]
    public bool isTargeted = false;
    public Color normalColor = Color.white;
    public Color targetedColor = Color.red;
    
    private SpriteRenderer spriteRenderer;
    private CombatSystem combatSystem;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        combatSystem = FindFirstObjectByType<CombatSystem>();
        currentHealth = maxHealth;
        
        if (spriteRenderer)
        {
            spriteRenderer.color = normalColor;
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        
        // Visual feedback for taking damage
        if (spriteRenderer)
        {
            StartCoroutine(FlashDamage());
        }
        
        Debug.Log($"{enemyName} took {damage} damage. Health: {currentHealth}/{maxHealth}");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private System.Collections.IEnumerator FlashDamage()
    {
        if (spriteRenderer)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
    }
    
    public void SetTargeted(bool targeted)
    {
        isTargeted = targeted;
        if (spriteRenderer)
        {
            spriteRenderer.color = targeted ? targetedColor : normalColor;
        }
    }
    
    void Die()
    {
        Debug.Log($"{enemyName} has been defeated!");
        
        // Award XP to player
        if (combatSystem)
        {
            combatSystem.OnEnemyDefeated(this);
        }
        
        // Simple death effect - could be replaced with animation
        StartCoroutine(DeathSequence());
    }
    
    private System.Collections.IEnumerator DeathSequence()
    {
        // Fade out effect
        if (spriteRenderer)
        {
            float fadeTime = 1f;
            Color startColor = spriteRenderer.color;
            
            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
                spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }
        }
        
        gameObject.SetActive(false);
    }
    
    // Called when player clicks on this enemy
    void OnMouseDown()
    {
        if (combatSystem)
        {
            combatSystem.SelectTarget(this);
        }
    }
    
    // Visual feedback when mouse hovers
    void OnMouseEnter()
    {
        if (!isTargeted && spriteRenderer)
        {
            spriteRenderer.color = Color.yellow;
        }
    }
    
    void OnMouseExit()
    {
        if (!isTargeted && spriteRenderer)
        {
            spriteRenderer.color = normalColor;
        }
    }
}