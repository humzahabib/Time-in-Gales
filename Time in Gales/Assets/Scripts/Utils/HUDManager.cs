using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] Slider playerHealthBar;
    [SerializeField] Slider coolDownBar;
    [SerializeField] Transform deadScreen;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.PlayerDamageEvent != null && GameManager.Instance.PlayerDeadEvent != null)
        {
            GameManager.Instance.PlayerDamageEvent.AddListener(PlayerHealthChangeEventHandler);
            GameManager.Instance.PlayerDeadEvent.AddListener(PlayerDeadEventHandler);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void PlayerDeadEventHandler()
    {
        if(deadScreen != null)
        {
            deadScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void HeatupValueChangeEventHandler(float value)
    {
        if(coolDownBar != null)
        {
            coolDownBar.value = value;
        }
    }


    void PlayerHealthChangeEventHandler(float value)
    {
        if(playerHealthBar != null)
        {
            playerHealthBar.value -= value;
        }
    }
}
