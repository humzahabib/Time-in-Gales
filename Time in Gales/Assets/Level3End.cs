using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3End : MonoBehaviour
{
    AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        
    
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(8);
            SceneManager.LoadScene("Main Menu");
        }
    }
    
}
