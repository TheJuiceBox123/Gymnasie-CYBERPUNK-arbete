using UnityEditor.Tilemaps;
using UnityEngine;

public class RangedGroundEnemy : MonoBehaviour
{
    [Header("References")]
    public Transform player; // Drag you player gameobject here
    public Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 2f;
    private int facingDirection = -1; // -1 = facing left, 1 = facing right (set based on sprite's default facing)

    [Header("GroundCheck")]
    public Transform groundCheckPoint;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    [Header("LedgeCheck")]
    public Transform ledgeCheckPoint;
    public float ledgeCheckDistance = 0.3f;

    [Header("Attack")]
    public float stopDistance = 4f; // How close the enemy gets before stopping to shoot
    public float attackCooldown = 2f;
    private float attackTimer;
    public GameObject projectilePrefab;
    public Transform firePoint; // Where the projectile spawns
    public float projectileSpeed = 8f;

    private void Update()
    {
        // Decides whether to walk toward the player or stop and shoot
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Ground checken
        bool isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, groundLayer);

        // Ledge checken
        bool ledgeAhead = !Physics2D.Raycast(ledgeCheckPoint.position, Vector2.down, ledgeCheckDistance, groundLayer);

        // Visualizes the ledge check, and makes it switch color depending on if it detects ground or not
        Debug.DrawRay(ledgeCheckPoint.position, Vector2.down * ledgeCheckDistance, ledgeAhead ? Color.red : Color.green);

        // Flippar enemyn mot playerns håll
        FacePlayer();

        // Decides if the enemy should walk or stop depending on the distance to the player
        if (distanceToPlayer > stopDistance && isGrounded && !ledgeAhead)
        {
            // Player is far away enough
            Walk();
        }
        else
        {
            // If either player is close enough or if there's a ledge, stop moving
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

            // Only attack if it's the player being close enough (so the enemy doesn't shoot bcs of a ledge)
            if (distanceToPlayer <= stopDistance)
            {
                HandleAttackTimer();
            }
        }
    }

    void Walk()
    {
        // Move in the direction enemy is facing
        // (Undviker samma misstag som innan genom att inte röra rb.lineraVelocity.y så gravitation funkar)
        rb.linearVelocity = new Vector2(facingDirection * moveSpeed, rb.linearVelocity.y);
    }

    void FacePlayer()
    {
        // If the player is to the opposite direction, flip to face player
        if (player.position.x > transform.position.x && facingDirection == -1)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingDirection *= -1; // reverse direction

        // Flip sprite by inverting x scale
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void HandleAttackTimer()
    {
        // Cooldown timer
        attackTimer -= Time.deltaTime;

        // When timer is zero, fire and reset timer

        if (attackTimer <= 0f)
        {
            Shoot();
            attackTimer = attackCooldown;
        }
    }

    void Shoot()
    {
        // Spawn projectile at fire point, other script for aiming
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Calculate direction to player from the fire point (normalized ignores distance)
        Vector2 directionToPlayer = (player.position - firePoint.position).normalized;

        // Pass the information and speed to the projectile script
       // EnemyProjectileScript projectileScript = projectile.GetComponent<EnemyProjectileScript>();
       // projectileScript.SetDirection(directionToPlayer, projectileSpeed);
    }



}
