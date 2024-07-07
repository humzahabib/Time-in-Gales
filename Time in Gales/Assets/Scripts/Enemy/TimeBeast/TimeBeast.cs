using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBeast : MonoBehaviour
{
    [SerializeField] AudioClip spawnAudio;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AudioManager.Play(spawnAudio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
