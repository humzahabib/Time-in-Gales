using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UnityEvent<float> PlayerHealthChangeEvent = new UnityEvent<float>();
    public UnityEvent<float, GameObject> EnemyDamageGivenEvent = new UnityEvent <float, GameObject>();
    public UnityEvent EnemyDeadEvent = new UnityEvent();
    public UnityEvent PlayerDeadEvent = new UnityEvent();


    // UI Events
    public UnityEvent<float> HeatupValueChange = new UnityEvent<float>();
    public UnityEvent<float> PlayerHealthChangeEvent = new UnityEvent<float>();

    static GameManager instance;

    [SerializeField] Slider slider;
    [SerializeField] GameObject player;



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
            Destroy(this);
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HeatupValueChange.AddListener(HeatupValueChangeEventListener);
        PlayerHealthChangeEvent.AddListener(PlayerDamageEventHandler);
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
        Debug.Log(value);
        slider.value = value;
    }


    

}
