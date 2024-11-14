using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemType; // Tipo de objeto: "Cherry", "Gem", "Potion"

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Collected: {itemType}");
            ItemCollector itemCollector = collision.GetComponent<ItemCollector>();

            if (itemCollector != null)
            {
                switch (itemType)
                {
                    case "Cherry":
                        itemCollector.OnCherryCollected();
                        break;

                    case "Gem":
                        itemCollector.OnGemCollected();
                        break;

                    case "Potion":
                        itemCollector.OnPotionCollected();
                        break;
                }

                Destroy(gameObject); // Destruir el objeto recolectado
            }
        }
    }
}