using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[System.Serializable]
public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float damagePoints;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 forward = transform.forward;
        forward.y = 0f;
        

        rb.velocity = forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.EnemyDamageGivenEvent.Invoke(damagePoints, collision.gameObject);
        if (collision.gameObject.tag == "Player")
            GameManager.Instance.PlayerDamageEvent.Invoke(damagePoints);
        Destroy(this.gameObject);
    }
}
