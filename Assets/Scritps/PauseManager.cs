using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // Assign your pause menu Canvas or Panel here
    public Button resumeButton;  // Assign your resume button here
    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false); // Ensure pause menu is hidden at start
        resumeButton.onClick.AddListener(ResumeGame); // Add listener to resume button
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }
}
