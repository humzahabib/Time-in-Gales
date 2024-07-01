using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float damagePoints;


    // Start is called before the first frame update
    void Start()
    {
        // Aiming logic
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            Vector3 lookDir = pointToLook - transform.position;
            lookDir.y = transform.position.y;
            Debug.DrawLine(transform.position, transform.position + lookDir, Color.red);
            lookDir.y = 0;
            transform.forward = lookDir;

        }
        

        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.EnemyDamageGivenEvent.Invoke(damagePoints, collision.gameObject);
        if (collision.gameObject.tag == "Player")
            GameManager.Instance.PlayerHealthChangeEvent.Invoke(damagePoints);
        Destroy(this.gameObject);
    }
}
