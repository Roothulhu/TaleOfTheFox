using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject linkedDoor; // Puerta vinculada
    public AudioClip teleportSound; // Sonido de teletransporte
    private AudioSource audioSource; // Componente de AudioSource
    private bool isTeleporting; // Evitar teletransporte inmediato de vuelta

    private void Start()
    {
        // Agregar y configurar el AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // No reproducir sonido al inicio
        audioSource.clip = teleportSound; // Asignar el clip de sonido
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            TeleportPlayer(other);
        }
    }

    private void TeleportPlayer(Collider2D player)
    {
        if (linkedDoor != null)
        {
            // Reproducir sonido de teletransporte
            if (teleportSound != null)
            {
                audioSource.Play();
            }

            // Teletransportar al jugador
            Door linkedDoorScript = linkedDoor.GetComponent<Door>();

            // Activar el estado de teletransporte en la puerta de destino
            if (linkedDoorScript != null)
            {
                linkedDoorScript.StartCoroutine(linkedDoorScript.PreventImmediateTeleport());
            }

            // Teletransportar al jugador
            player.transform.position = linkedDoor.transform.position;

            // Evitar que el jugador vuelva a teletransportarse de inmediato en la puerta actual
            StartCoroutine(PreventImmediateTeleport());
        }
        else
        {
            Debug.LogWarning("La puerta vinculada no está configurada.");
        }
    }

    private IEnumerator PreventImmediateTeleport()
    {
        isTeleporting = true;
        yield return new WaitForSeconds(0.5f); // Tiempo para evitar teletransporte inmediato
        isTeleporting = false;
    }
}
