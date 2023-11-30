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

    private void Update()
    {
        // Handle projectile movement, effects, or other behavior here
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with an enemy");

            // Destroy the enemy object
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Pickup"))
        {
            Debug.Log("Shot a pickup");

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
            Debug.Log("Shot a wall");

            Destroy(gameObject);
        }
    }
}
