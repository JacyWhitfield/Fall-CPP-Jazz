using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damageAmount = 10; // Adjust the damage amount as needed

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

          
            Destroy(gameObject);
        }
    }
}
