using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    private Rigidbody2D rb;

    private void Awake()
    {
        // starta med fullt hp
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int amount, Vector3 sourcePosition, float knockbackForce)
    {
        currentHP -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. HP: {currentHP}/{maxHP}");

        ApplyKnockback(sourcePosition, knockbackForce);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void ApplyKnockback (Vector2 sourcePosition, float force)
    {
        if (rb == null)
        {
            Debug.Log($"{gameObject.name} has no Rigidbody2D - knockback skipped");
            return;
        }

        Vector2 knockbackdir =((Vector2)transform.position - sourcePosition).normalized;
        Debug.Log("Applying knockback");

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockbackdir * force, ForceMode2D.Impulse);

        // Disable the player's movement shortly
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetKnockedBack(0.2f);
        }

        RangedGroundEnemy rangedGroundEnemy = GetComponent<RangedGroundEnemy>();
        if (rangedGroundEnemy != null)
        {
            rangedGroundEnemy.SetKnockedBack(0.2f);
        }
    }

    void Die()
    {
        Debug.Log("{gameObject.name} died");
        Destroy(gameObject);
    }

}
