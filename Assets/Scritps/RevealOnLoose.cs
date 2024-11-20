using UnityEngine;

public class RevealOnLoose : MonoBehaviour
{
    public GameObject canvasToReveal; // Assign your Canvas here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure your player has the tag "Player"
        {
            canvasToReveal.SetActive(true); // Reveal the Canvas
            Time.timeScale = 0f;
        }
    }
}
