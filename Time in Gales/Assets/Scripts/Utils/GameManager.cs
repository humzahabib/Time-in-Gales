using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent<float> PlayerDamageEvent;
    public UnityEvent<float, GameObject> EnemyDamageGivenEvent;
    static GameManager instance;

    GameObject player;



public GameObject Player
{
    get { return player; }
}


public static GameManager Instance
{

    get { return instance; } 
}
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
