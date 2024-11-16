using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject misionGO;

    void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMission();
            }
        }
    
    void ToggleMission()
    {
        if (misionGO.activeInHierarchy)
        {
            misionGO.SetActive(false);
        }
        else
        {
            misionGO.SetActive(true);
        }
    }
}