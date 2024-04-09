using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
      

        // Stop Play Mode if running in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // Quit the application for standalone builds
#endif
    }
}
