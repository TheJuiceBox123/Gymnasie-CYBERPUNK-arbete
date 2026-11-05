using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack")]
    public Transform attackPoint;      // empty child positioned in front of the player
    public float attackRange;
    public LayerMask enemyLayer;
    public int attackDamage;
    public GameObject attackVisual; // The visual object that "flashes" during attack
    public float flashDuration; // How long the "flash" object is visible
    public float knockbackForce;

    public void AttackInput(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0f) return;

        if (!context.performed) return; // fires once on key press

        Attack();
    }

    void Attack()
    {
        Debug.Log("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);
            // enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);

            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage, attackPoint.position, knockbackForce);
            }
        }

        StartCoroutine(FlashAttackVisual());

    }

    System.Collections.IEnumerator FlashAttackVisual()
    {
        attackVisual.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        attackVisual.SetActive(false);
    }

    // Visualize attack range in editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
