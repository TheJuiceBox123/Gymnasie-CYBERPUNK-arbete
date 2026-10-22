using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    private Vector2 direction;
    private float speed;

    public float lifetime;

    public int damage;
    public float knockbackForce;

    public void SetDirection(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        // Rotate the sprite toward its travel direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Space.World gör att rörelsen är baserad på världen
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");

            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, transform.position, knockbackForce);
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
