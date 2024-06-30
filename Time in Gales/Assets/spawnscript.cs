using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnscript : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemyCount = 6; // Number of enemies to spawn
    private GameObject[] enemies;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the array
        enemies = new GameObject[enemyCount];
        for (int i = 0; i < enemyCount; i++)
        {
            enemies[i] = enemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Populate the array with enemy instances
        int i = 0;
        while (i < enemyCount)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(15f, 40f), 0.3f, Random.Range(46f, 52f));
            enemies[i] = Instantiate(enemy, spawnPosition, Quaternion.identity);
            i++;
        }
    }
}
