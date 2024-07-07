using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] int waves;
    private int wavesSpawned;
    [SerializeField] private GameObject[] wave1;
    [SerializeField] private GameObject[] wave2;
    [SerializeField] private GameObject[] wave3;
    [SerializeField] private AudioClip waveSound;

    private bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private int interval = 5;
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

        if (waveSound != null)
        {
            GameManager.Instance.AudioManager.Play(waveSound);
        }
    }


    bool IsWaveFinished(int waveNumber)
    {
        if (waveNumber == 1)
        {
            for (int i = 0; i < wave1.Length; i++)
            {
                if (wave1[i].activeInHierarchy)
                {
                    return false;
                }
            }
            return true;
        }
        else if (waveNumber == 2)
        {
            for (int i = 0; i < wave2.Length; i++)
            {
                if (wave2[i].activeInHierarchy)
                {
                    return false;
                }
            }
            return true;
        }
        else if (waveNumber == 3)
        {
            for (int i = 0; i < wave3.Length; i++)
            {
                if (wave3[i].activeInHierarchy)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return true;
        }
    }


}
