using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public int playerLives = 3; // Initial number of lives
    public TextMeshProUGUI livesText; 

    private void Start()
    {
        UpdateLivesText(); // Initialize the text with the initial number of lives.
    }

    
    public void LoseLife()
    {
        playerLives--;

        if (playerLives < 0)
        {
            playerLives = 0; // Ensure lives don't go below zero
        }

        UpdateLivesText();

      
    }

  
    void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + playerLives.ToString();
        }
    }
}
