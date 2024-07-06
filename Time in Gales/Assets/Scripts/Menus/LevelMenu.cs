using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelMenu : MonoBehaviour
{

    [SerializeField] GameObject mainMenuscreen;
    [SerializeField] GameObject levelSelectScreen;

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void BackToMainMenuPanel()
    {
        if (levelSelectScreen != null)
        {
            levelSelectScreen.SetActive(false);
        }
        if (mainMenuscreen != null)
        {
            mainMenuscreen.SetActive(true);
        }
    }
}
