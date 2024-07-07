using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Start : MonoBehaviour
{
    AudioSource audioS;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioS = GetComponent<AudioSource>();
            audioS.Play();
        }
    }
}
