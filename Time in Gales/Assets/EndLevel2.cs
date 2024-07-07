using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("EndLevel");
    }


    IEnumerable EndLevel()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Level 3");
    }
}
