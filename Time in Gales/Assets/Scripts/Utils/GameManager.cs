using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent<float, GameObject> EnemyDamageGivenEvent = new UnityEvent<float, GameObject>();
    public UnityEvent<float> PlayerDamageEvent = new UnityEvent<float>();
    public UnityEvent EnemyDeadEvent = new UnityEvent();
    public UnityEvent PlayerDeadEvent = new UnityEvent();
    public UnityEvent<Dialogue> DialogueDisplayEvent = new UnityEvent<Dialogue>();

    // UI Events
    public UnityEvent<float> HeatupValueChange = new UnityEvent<float>();
    public UnityEvent<float> PlayerHealthChangeEvent = new UnityEvent<float>();

    static GameManager instance;

    [SerializeField] Slider slider;
    [SerializeField] GameObject player;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject mainMenuscreen;
    [SerializeField] GameObject levelSelectScreen;
    [SerializeField] GameObject optionSelectScreen;


    public GameObject Player
{
    get { return player; }
}


public static GameManager Instance
{

    get { return instance; } 
}

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(GameManager.instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HeatupValueChange.AddListener(HeatupValueChangeEventListener);

        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void PlayerDamageEventHandler(float damage)
    {
        Debug.Log(damage);
    }

    void Listener(float damage, GameObject o)
    {
        Debug.Log("Manager Listening");
    }


    void HeatupValueChangeEventListener(float value)
    {
        slider.value = value;
    }


    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(true);
        }
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("Level1");
    }

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

    public void BackToMainMenuPanel()
    {
        if (optionSelectScreen != null)
        {
            optionSelectScreen.SetActive(false);
        }
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
