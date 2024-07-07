using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int totalEnemiesinScene = 0;

    [SerializeField] int enemiesTargetCount;
    [SerializeField] GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
    }


    void EnemyDeadEventHandler()
    {
        totalEnemiesinScene--;
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            if (totalEnemiesinScene < enemiesTargetCount)
            {
                GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
