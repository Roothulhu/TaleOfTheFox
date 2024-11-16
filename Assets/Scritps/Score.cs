using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int cherry;
    public int diamond;
    public int potion;
    public int lives;
    
    [SerializeField] private TextMeshProUGUI cherryTMP;
    [SerializeField] private TextMeshProUGUI diamondTMP;
    [SerializeField] private TextMeshProUGUI potionTMP;
    [SerializeField] private TextMeshProUGUI livesTMP;

    [SerializeField] private Finish Finish;

    private void Start()
    {
        InitializeUI();
    }

    public void IncreaseCherry()
    {
        cherry++;
        cherryTMP.text = cherry.ToString();
    }
    public void IncreaseDiamond()
    {
        diamond++;
        diamondTMP.text = diamond.ToString();
    }
    public void IncreasePotion()
    {
        potion++;
        potionTMP.text = potion.ToString();
    }

    public int HandleDamage()
    {
        lives--;
        livesTMP.text = lives.ToString();
        if (lives == 0)
        {
            Finish.SetFinalScore();
        }

        return lives;
    }

    private void InitializeUI()
    {
        
        cherry = 0;
        diamond = 0;
        potion = 0;
        lives = 5;
        
        cherryTMP.text = cherry.ToString();
        diamondTMP.text = diamond.ToString();
        potionTMP.text = potion.ToString();
        livesTMP.text = lives.ToString();
    }
}
