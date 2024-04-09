using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    // Player lives and restarting flag
    public int playerLives = 3;
    public bool IsRestarting { get; set; } = false;

    // Text to display lives count
    public Text livesText;

    // Reference to the PauseMenu panel and buttons
    public GameObject pauseMenuPanel;
    public Button resumeButton;
    public Button quitButton;

    // Flag to check if the game is paused
    public bool IsPaused { get; set; } = false;

    private void Start()
    {
        // Singleton pattern
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Find the pause menu panel within the UIContainer when the game starts
        FindPauseMenuPanel();

        // Initialize the PauseMenu panel and buttons
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false); // Hide the PauseMenu panel initially
        }

        if (resumeButton != null)
        {
            // Add an onClick event listener to the Resume button
            resumeButton.onClick.AddListener(ResumeGame);
        }

        if (quitButton != null)
        {
            // Add an onClick event listener to the Quit button
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    private void FindPauseMenuPanel()
    {
        Transform uiContainer = GameObject.Find("UIContainer").transform;
        if (uiContainer != null)
        {
            pauseMenuPanel = uiContainer.Find("Canvas/Panel/pauseMenuPanel").gameObject;
        }
    }

    private void Update()
    {
        if (!IsPaused)
        {
            // Game is not paused, check for pause input
            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseGame();
            }
        }
        else
        {
            // Game is paused, check for unpause input
            if (Input.GetKeyDown(KeyCode.P))
            {
                ResumeGame();
            }
        }

        // Rest of your Update method code...
    }

    // Method to handle player death
    public void Die()
    {
        // Decrement player lives when the player dies
        playerLives--;

        if (playerLives > 0)
        {
            // Respawn the player or perform other necessary actions
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // Ensure the game is unpaused when respawning
            IsPaused = false; // Resume the game
        }
        else
        {
            // Player is out of lives, return to the main menu
            ReturnToMainMenu();
        }
    }

    // Method to return to the main menu
    public static void ReturnToMainMenu()
    {
        // You can add any additional cleanup or save game progress logic here

        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    // Method to handle level restart
    public void RestartLevel()
    {
        IsRestarting = true; // Set the flag to indicate that the game is restarting

        // Perform the level reset logic here

        IsRestarting = false; // Reset the flag when the level reset is completed
    }

    // Method to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0; // Pause the game by setting the time scale to 0
        IsPaused = true;

        // Show the PauseMenu panel
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game by setting the time scale to 1
        IsPaused = false;

        // Hide the PauseMenu panel
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
    }
    public void ExitGame()
    {
        // Add any necessary cleanup logic here

        // Stop Play Mode if running in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit(); // Quit the application for standalone builds
#endif
    }

    public void QuitGame()
    {
        // Add any necessary cleanup logic here

        // Stop Play Mode if running in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit(); // Quit the application for standalone builds
#endif
    }
    public void LoseLife()
    {
        playerLives--;

        // Update the UI Text with the new number of lives
        if (livesText != null)
        {
            livesText.text = "Lives: " + playerLives;
        }

        if (playerLives <= 0)
        {
            // Reload the current scene when the player is out of lives
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
