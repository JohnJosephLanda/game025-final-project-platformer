using UnityEngine;
using .QuitGame;

public class PauseMenu : MonoBehaviour
{
    // Reference to the UI Panel Game Object
    public GameObject pauseMenuPanel; 
    
    public static bool isPaused = false;

    void Update()
    {
        // Toggle pause when the Escape key is pressed
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Quit();
    }
}