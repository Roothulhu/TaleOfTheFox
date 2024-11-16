using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private Score Score;
    [SerializeField] private GameObject darkBackground;

    [SerializeField] private GameObject winGO;
    [SerializeField] private GameObject loseGO;
    
    [SerializeField] private TextMeshProUGUI cherryTMP;
    [SerializeField] private TextMeshProUGUI diamondTMP;
    [SerializeField] private TextMeshProUGUI potionTMP;
    [SerializeField] private TextMeshProUGUI livesTMP;
    
    public void SetFinalScore()
    {
        cherryTMP.text = Score.cherry.ToString();
        diamondTMP.text = Score.diamond.ToString();
        potionTMP.text = Score.potion.ToString();
        livesTMP.text = Score.lives.ToString();

        if (Score.diamond >= 5 && 
            Score.cherry >= 5 && 
            Score.potion >= 1 && 
            Score.lives > 0)
        {
            winGO.SetActive(true);
            loseGO.SetActive(false);
        }
        else
        {
            loseGO.SetActive(true);
            winGO.SetActive(false);
        }

        darkBackground.SetActive(true);
    }
    
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
