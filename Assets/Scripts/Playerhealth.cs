using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100; // Initial player health
    private bool canTakeDamage = true;
    public GameObject deathPanel; // Reference to the death panel GameObject
    private AudioSource audioSource;
    public void TakeDamage(int damageAmount)
    {
        if (canTakeDamage)
        {
            playerHealth -= damageAmount;
          
            if (audioSource != null)
            {
                audioSource.Play();
            }

            if (playerHealth <= 0)
            {
                Die();
            }

        
            StartCoroutine(DamageCooldown(0.5f));
        }
    }

    IEnumerator DamageCooldown(float delay)
    {
        canTakeDamage = false; // Prevent further damage
        yield return new WaitForSeconds(delay);
        canTakeDamage = true; // Enable damage-taking again after the delay
    }
    private void Start()
    {
       
        audioSource = GetComponent<AudioSource>();
    }
    void Die()
    {
       
        GameManager.ReturnToMainMenu();
    }
}
