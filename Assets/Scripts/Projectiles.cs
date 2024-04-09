using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 21f; // Speed of the projectile
    public float projectileLifetime = 2f; // Lifetime of the projectile

    private void Start()
    {
        // Set the initial velocity of the projectile
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // rb.velocity = transform.right * projectileSpeed;

        // Destroy the projectile after a specified time (e.g., projectileLifetime)
        Destroy(gameObject, projectileLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
       

            // Trigger death sequence in Enemy script
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TriggerDeath();
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EnemyTurret")) // Check for EnemyTurret
        {
           

            // Trigger death sequence in EnemyTurret script
            EnemyTurret enemyTurret = collision.gameObject.GetComponent<EnemyTurret>();
            if (enemyTurret != null)
            {
                enemyTurret.TriggerDeath();
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
        // ... rest of your collision handling ...
    
        else if (collision.gameObject.CompareTag("Pickup"))
        {
          

            // Destroy the pickup object
            Destroy(collision.gameObject);

            // Switch to the new bullet
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.SwitchToNewBullet();
            }

            // Destroy the original bullet
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            

            Destroy(gameObject);
        }
    }
}
