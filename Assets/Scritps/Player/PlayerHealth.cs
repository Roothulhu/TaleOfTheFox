using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public Animator animator; // Asignar el Animator en el Inspector
    public GameObject gameOverPanel; // Panel UI que se muestra cuando la vida llega a 0

    public Canvas healthCanvas; // Canvas que contiene el texto de salud
    private Text healthText; // Texto dentro del Canvas que muestra la salud

    public AudioClip damageSound; // Sonido al recibir daño
    public AudioClip colisionSound; // Sonido de colisión

    private AudioSource audioSource; // Fuente de audio para reproducir sonidos

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        gameOverPanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        // Buscar el Text dentro del Canvas
        if (healthCanvas != null)
        {
            healthText = healthCanvas.GetComponentInChildren<Text>();
            if (healthText == null)
            {
                Debug.LogError("No se encontró un Text dentro del Canvas asignado.");
            }
        }
        else
        {
            Debug.LogError("No se ha asignado un Canvas al script.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Smile"))
        {
            TakeDamage(1);
            PlayCollisionSound();
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        PlayDamageSound();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("isHurt"); // Animación de daño
        }
    }

    void Die()
    {
        animator.SetTrigger("Death"); // Animación de muerte
        gameOverPanel.SetActive(true); // Mostrar el panel de Game Over
        this.enabled = false; // Desactivar el script
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    void PlayDamageSound()
    {
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    void PlayCollisionSound()
    {
        if (colisionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(colisionSound);
        }
    }
}
