using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUDManager : MonoBehaviour
{
    public UnityEvent<float> PlayerHealthBarChangeEvent = new UnityEvent<float>();
    [SerializeField] Slider playerHealthBar;


    static HUDManager instance;

public static HUDManager Instance
{get {return instance;}}

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthBarChangeEvent.AddListener(PlayerHealthChangeEventHandler);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHealthChangeEventHandler(float currentHealth)
    {
        playerHealthBar.value = currentHealth;
    }
}
