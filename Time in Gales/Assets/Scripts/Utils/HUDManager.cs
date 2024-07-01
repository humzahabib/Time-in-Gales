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
        GameManager.Instance.PlayerDamageEvent.AddListener(PlayerHealthChangeEventHandler);
        GameManager.Instance.PlayerDeadEvent.AddListener(PlayerDeadEventHandler);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void PlayerDeadEventHandler()
    {
        deadScreen.gameObject.SetActive(true);
    }

    void HeatupValueChangeEventHandler(float value)
    {
        coolDownBar.value = value;
    }


    void PlayerHealthChangeEventHandler(float value)
    {
        playerHealthBar.value -= value;
    }
}
