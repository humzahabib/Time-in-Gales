using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] GameObject[] gObjects;
    [SerializeField] int iterations;

    public void Instentiate(GameObject obj)
    {
        gObjects = new GameObject[iterations];
        for (int i = 0; i < iterations; i++)
        {
            gObjects[i] = Instantiate(gObjects[i], transform.position, Quaternion.identity);
            gObjects[i].SetActive(false);
        }
    }

    public GameObject SpawnObject(Vector3 position)
    {
        for (int i = 0; i < iterations; i++)
        {
            if (!gObjects[i].activeInHierarchy)
            {
                gObjects[i].transform.position = position;
                gObjects[i].SetActive(true);
                return gObjects[i];
            }
        }

        return null;
    }


    public void DestroyIt(GameObject obj)
    {
        obj.SetActive(false);
    }

}

// object pooling under construction
// This will be completed next time.
// In the meantime, please enjoy the gameplay.
// Thank you for your patience.
// Regards
// Zain and Farooq