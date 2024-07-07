using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] int waves;
    private int wavesSpawned = 0;
    [SerializeField] private GameObject[] wave1;
    [SerializeField] private GameObject[] wave2;
    [SerializeField] private GameObject[] wave3;

    private bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int interval = 2;
    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % interval == 0)
        {
            if (isTriggered)
            {
                if (IsWaveFinished(wavesSpawned))
                {
                    wavesSpawned++;
                    if (wavesSpawned <= waves)
                    {
                        SpawnWave(wavesSpawned);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isTriggered)
            {
                Debug.Log("Triggered");
                isTriggered = true;
                SpawnWave(wavesSpawned);
                wavesSpawned++;
            }
        }
    }

    void SpawnWave(int waveNumber)
    {
        waveNumber++;
        if (waveNumber == 1)
        {
            foreach (GameObject enemy in wave1)
            {
                enemy.SetActive(true);
            }
        }
        else if (waveNumber == 2)
        {
            foreach (GameObject enemy in wave2)
            {
                enemy.SetActive(true);
            }
        }
        else if (waveNumber == 3)
        {
            foreach (GameObject enemy in wave3)
            {
                enemy.SetActive(true);
            }
        }
    }


    bool IsWaveFinished(int waveNumber)
    {
        if (waveNumber == 1)
        {
            foreach (GameObject enemy in wave1)
            {
                if (enemy.activeSelf)
                {
                    return false;
                }
            }
        }
        else if (waveNumber == 2)
        {
            foreach (GameObject enemy in wave2)
            {
                if (enemy.activeSelf)
                {
                    return false;
                }
            }
        }
        else if (waveNumber == 3)
        {
            foreach (GameObject enemy in wave3)
            {
                if (enemy.activeSelf)
                {
                    return false;
                }
            }
        }
        return true;
    }
        

}
