using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100; // Initial player health

    // Method to handle player taking damage
    public void TakeDamage(int damageAmount)
    {
        playerHealth -= damageAmount;

        // Check if player's health is zero or less to indicate player death
        if (playerHealth <= 0)
        {
            Die(); // Call the method to handle player death
        }
        {
            Debug.Log("Player took damage: " + damageAmount);
            playerHealth -= damageAmount;
            // Rest of the code...
        }
    }

    // Method to handle player death
    void Die()
    {
        // Add logic to handle player death, e.g., play death animation, show game over screen, or reset player's position
        // For example, you can reload the current scene:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
