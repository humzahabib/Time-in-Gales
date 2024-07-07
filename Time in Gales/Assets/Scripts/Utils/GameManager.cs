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
    public UnityEvent<Vector3, GameObject> EnemyDeadEvent = new UnityEvent<Vector3, GameObject>();
    public UnityEvent PlayerDeadEvent = new UnityEvent();
    public UnityEvent<Dialogue> DialogueDisplayEvent = new UnityEvent<Dialogue>();

    // UI Events
    public UnityEvent<float> HeatupValueChange = new UnityEvent<float>();
    public UnityEvent<float> PlayerHealthChangeEvent = new UnityEvent<float>();

    static GameManager instance;
    AudioManager audioManager;
    [SerializeField] Slider slider;
    [SerializeField] GameObject player;
    [SerializeField] AudioClip playerDamage;


    public GameObject Player
{
    get { return player; }
}


public AudioManager AudioManager
    {
        get { return audioManager; }
    }

public static GameManager Instance
{

    get { return instance; } 
}
    void EnemyDeadEventHandler(Vector3 pos, GameObject effectDeath)
    {
        StartCoroutine(InstantiateEffectDeath(pos, effectDeath));
    }

    IEnumerator InstantiateEffectDeath(Vector3 pos, GameObject effectDeath)
    {
        if (effectDeath != null)
        {
            GameObject myEffectDeath = Instantiate(effectDeath, pos, Quaternion.identity);
            myEffectDeath.SetActive(true);
            Debug.Log("EFFFFFFECCCTTTT");
            Debug.Log("Effect instantiated at position: " + transform.position);
            yield return new WaitForSeconds(0.2f);
            Debug.Log("FUCKKK");
            myEffectDeath.SetActive(false);
        }
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
        audioManager = FindObjectOfType<AudioManager>();
        Time.timeScale = 1f;
<<<<<<< Updated upstream
        GameManager.Instance.EnemyDeadEvent.AddListener(EnemyDeadEventHandler);
=======


        
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void PlayerDamageEventHandler(float damage)
    {
        if(playerDamage != null)
        {
            GameManager.Instance.AudioManager.Play(playerDamage);
        }
        Debug.Log(damage);
    }

    void Listener(float damage, GameObject o)
    {
        Debug.Log("Manager Listening");
    }


    void HeatupValueChangeEventListener(float value)
    {
        if (slider != null)
            slider.value = value;
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

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
