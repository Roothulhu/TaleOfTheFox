using UnityEngine;

public class closeUI : MonoBehaviour
{

    // Assign this through the Unity Inspector
    public GameObject uiElement;

    // Call this method on button click
    public void Close()
    {
        uiElement.SetActive(false);
    }

}
