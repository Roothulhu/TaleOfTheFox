using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private Score Score;

    // Enum para definir los tipos de objetos recolectables
    public enum ItemType
    {
        Cherry,
        Gem,
        Potion
    }

    [SerializeField] private ItemType itemType; // Define el tipo de objeto usando el enum

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Validando case de fruta");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Colisión con el jugador detectada");
            
            switch (itemType)
            {
                case ItemType.Cherry:
                    Debug.Log("Collected: Cherry");
                    Score.IncreaseCherry();
                    Destroy(gameObject);
                    break;

                case ItemType.Gem:
                    Debug.Log("Collected: Gem");
                    Score.IncreaseDiamond();
                    Destroy(gameObject);
                    break;

                case ItemType.Potion:
                    Debug.Log("Collected: Potion");
                    Score.IncreasePotion();
                    Destroy(gameObject);
                    break;

                default:
                    Debug.LogWarning("ItemType no reconocido");
                    break;
            }
        }
    }
}