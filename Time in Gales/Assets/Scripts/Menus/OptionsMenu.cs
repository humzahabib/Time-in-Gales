using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] GameObject mainMenuscreen;
    [SerializeField] GameObject optionSelectScreen;

    public void BackToMainMenuPanel()
    {
        if (optionSelectScreen != null)
        {
            optionSelectScreen.SetActive(false);
        }
        if (mainMenuscreen != null)
        {
            mainMenuscreen.SetActive(true);
        }
    }
}
