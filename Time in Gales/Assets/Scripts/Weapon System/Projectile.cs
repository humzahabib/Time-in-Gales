using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float damagePoints;
    [SerializeField] bool isPlayers;
    [SerializeField] GameObject bulletEffect;

    IEnumerator InstantiateBulletEffect(GameObject bulletEffect)
    {
        if (bulletEffect != null)
        {
            GameObject myeffect = Instantiate(bulletEffect, transform.position, Quaternion.identity);
            myeffect.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            myeffect.SetActive(false);
            Debug.Log("fireeeeeee");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        Vector3 forward = transform.forward;
        forward.y = 0f;

        StartCoroutine(InstantiateBulletEffect(bulletEffect));
        rb.velocity = forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isPlayers)
            GameManager.Instance.EnemyDamageGivenEvent.Invoke(damagePoints, collision.gameObject);
        if (collision.gameObject.tag == "Player")
            GameManager.Instance.PlayerDamageEvent.Invoke(damagePoints);
        Destroy(this.gameObject);
    }
}
