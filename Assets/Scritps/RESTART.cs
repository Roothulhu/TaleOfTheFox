using UnityEngine;
using UnityEngine.SceneManagement;

public class RESTART : MonoBehaviour
{
    // Call this method when the button is clicked
    public void RestartScene()
    {
        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}