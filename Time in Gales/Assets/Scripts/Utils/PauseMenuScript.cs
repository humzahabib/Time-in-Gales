using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(true);
        }

        Time.timeScale = 0;

        //if (playerRB != null)
        //{
        //    playerRB.constraints = RigidbodyConstraints.FreezeRotation;
        //}
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);
        }
        //if (playerRB != null)
        //{
        //    playerRB.constraints = RigidbodyConstraints.None;
        //}
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
