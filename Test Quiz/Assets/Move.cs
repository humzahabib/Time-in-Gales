using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] Transform green;

    Vector3 dist;

    // Start is called before the first frame update
    void Start()
    {

        dist = green.transform.position - this.transform.position;
        Debug.Log(Time.time);
    }
    bool printed = false;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, green.position) > 0.2f)
        {
            Vector3 pos = this.transform.position;
            pos += (dist * Time.deltaTime) / 2;
            transform.position = pos;
            Debug.Log(Time.time);
        }


        if (dist.magnitude <= 0.3f)
            Debug.Log(Time.time);
    }
}
