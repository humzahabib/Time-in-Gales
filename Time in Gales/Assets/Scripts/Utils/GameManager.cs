using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



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
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(true);

            Debug.Log("Nigga bigga tidda");
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
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingBar;
    [SerializeField] TextMeshProUGUI progressText;

    public void LoadLevel(string scene)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(scene));
    }


    IEnumerator LoadAsync(string scene)
    {
        yield return new WaitForSeconds(0.25f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}
