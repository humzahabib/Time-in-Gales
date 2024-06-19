using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent<float> PlayerDamageEvent;
    static GameManager instance;



public static GameManager Instance
{

    get { return instance; } 
}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void PlayerDamageEventHandler(float damage)
    {
        Debug.Log(damage);
    }
}
