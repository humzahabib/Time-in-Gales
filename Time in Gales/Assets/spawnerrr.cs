using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerrr : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemyCount = 6; // Number of enemies to spawn
    private GameObject[] enemies;
    int onceSpawned = 0;

    //public GameObject[] GetEnemyArray()
    //{
    //    return enemies;
    //}


    // Start is called before the first frame update
    void Start()
    {
        enemies = new GameObject[enemyCount];
        for (int i = 0; i < enemyCount; i++)
        {
            enemies[i] = enemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggerrrr");
        int i = 0;
        while (i < enemyCount && onceSpawned < enemyCount)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(15f, 40f), 0.3f, Random.Range(46f, 52f));
            Instantiate(enemies[i], spawnPosition, Quaternion.identity);
            i++;
            onceSpawned++;
        }
    }
}
