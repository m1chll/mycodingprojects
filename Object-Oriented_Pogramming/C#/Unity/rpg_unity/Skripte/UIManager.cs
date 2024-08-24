using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject windowText;
    public Text questText;

    public void ToggleTextWindow(bool isEnabled)
    {
        windowText.SetActive(isEnabled);
    }

    public void SetQuestText(string content)
    {
        questText.text = content; 

    }
}
