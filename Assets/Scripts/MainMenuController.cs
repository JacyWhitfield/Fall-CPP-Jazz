using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public AudioSource menuBackgroundMusic;  // Reference to the menu background music Audio Source
    public AudioSource levelBackgroundMusic; // Reference to the level background music Audio Source

    public void Start()
    {
        // Play the menu background music when the MainMenu scene is loaded
        if (!menuBackgroundMusic.isPlaying)
        {
            menuBackgroundMusic.Play();
        }
    }

    public void StartGame()
    {
        Debug.Log("Button clicked!");

        // Stop the menu background music
        menuBackgroundMusic.Stop();

        // Play the level background music (if it's not already playing)
        if (!levelBackgroundMusic.isPlaying)
        {
            levelBackgroundMusic.Play();
        }

        // Load the "Level1" scene
        SceneManager.LoadScene("Level");
    }
}
