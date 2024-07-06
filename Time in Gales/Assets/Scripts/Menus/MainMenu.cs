using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject mainMenuscreen;
    [SerializeField] GameObject levelSelectScreen;
    [SerializeField] GameObject optionSelectScreen;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ToggleOnLevelSelectPanel()
    {
        if (mainMenuscreen != null)
        {
            mainMenuscreen.SetActive(false);
        }
        if (levelSelectScreen != null)
        {
            levelSelectScreen.SetActive(true);
        }
    }

    public void ToggleOnOptionPanel()
    {
        if (mainMenuscreen != null)
        {
            mainMenuscreen.SetActive(false);
        }
        if (optionSelectScreen != null)
        {
            optionSelectScreen.SetActive(true);
        }
    }
}
