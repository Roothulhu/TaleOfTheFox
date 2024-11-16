using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI



public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Score Score;
    public Image cherryImage;
    public Image gemImage;
    public Image potionImage;

    public int requiredCherries = 1; // Número necesario de cerezas
    public int requiredGems = 1; // Número necesario de gemas
    public int requiredPotions = 1; // Número necesario de pociones

    private int currentCherries = 0;
    private int currentGems = 0;
    private int currentPotions = 0;

    void Start()
    {
        SetImageColor(cherryImage, Color.red);
        SetImageColor(gemImage, Color.red);
        SetImageColor(potionImage, Color.red);
    }

    public void OnCherryCollected()
    {
        Score.IncreaseCherry();
        Debug.Log($"Cherries collected: {currentCherries}/{requiredCherries}");

        if (currentCherries >= requiredCherries)
        {
            UpdateImageState(cherryImage, true);
        }
    }

    public void OnGemCollected()
    {
        Score.IncreaseDiamond();
        Debug.Log($"Gems collected: {currentGems}/{requiredGems}");

        if (currentGems >= requiredGems)
        {
            UpdateImageState(gemImage, true);
        }
    }

    public void OnPotionCollected()
    {
        Score.IncreasePotion();
        
        Debug.Log($"Potions collected: {currentPotions}/{requiredPotions}");

        if (currentPotions >= requiredPotions)
        {
            UpdateImageState(potionImage, true);
        }
    }

    void UpdateImageState(Image image, bool collected)
    {
        if (collected)
        {
            SetImageColor(image, Color.green);
        }
    }

    void SetImageColor(Image image, Color color)
    {
        if (image != null)
        {
            image.color = color;
        }
    }
}