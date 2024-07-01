using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
<<<<<<< HEAD
=======

>>>>>>> parent of 519d549 (health)
    [SerializeField] Animator animator;
    [SerializeField] Transform pistol;
    [SerializeField] Transform rifle;
    [SerializeField] CharacterController controller;


    // Specs of the Player
    public float speed = 6f;
    private float gravity = -9.81f;
    private float downVelocity;

    private Vector3 direction;
    int hasRifleHash;
    int fireHash;
<<<<<<< HEAD
    float MaxHealth;
    float currentHealth;
    
=======
>>>>>>> parent of 519d549 (health)


    // Start is called before the first frame update
    void Start()
    {
        hasRifleHash = Animator.StringToHash("hasRifle");
        fireHash = Animator.StringToHash("Fire");
<<<<<<< HEAD
        MaxHealth = 100f;
        currentHealth = MaxHealth;
        GameManager.Instance.PlayerDamageEvent.AddListener(PlayerDamageGivenEventHandler);

=======
>>>>>>> parent of 519d549 (health)
    }




    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD



        if(currentHealth <= 0)
        {
            GameManager.Instance.PlayerDeadEvent.Invoke();
        }


        #region Movement Logic

=======
>>>>>>> parent of 519d549 (health)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput) * speed;

        controller.Move(direction * Time.deltaTime);

        // Aiming logic
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            Vector3 lookDir = pointToLook - transform.position;
            lookDir.y = 0;
            Debug.DrawLine(transform.position, transform.position + lookDir, Color.red);  
            // Rotate the player to face the mouse cursor
            transform.forward = lookDir;
            
        }



        // Walking Animation Logic
        float x;
        float y;

        if (controller.velocity.sqrMagnitude > 0.1f)
        {
            float angle = Vector3.SignedAngle(transform.forward, controller.velocity / speed, transform.up);

            x = Mathf.Cos(angle * Mathf.Deg2Rad);
            y = Mathf.Sin(angle * Mathf.Deg2Rad);
        }
        else
        {
            x = 0; y = 0;
        }

        // Debug.Log("X: " + x + "Y: " + y);
        animator.SetFloat("vy", x);
        animator.SetFloat("vx", y);

        // Gun Switching Animation Logic

        bool hasRifle = animator.GetBool(hasRifleHash);
        bool Fire = animator.GetBool(fireHash);
        bool isChangeToPistol = Input.GetKey("1");
        bool isChangeToRifle = Input.GetKey("2");
        bool isFirePressed = Input.GetButtonDown("Fire1");

        if (!hasRifle && isChangeToRifle)
        {
            animator.SetBool(hasRifleHash, true);
            rifle.gameObject.SetActive(true);
            pistol.gameObject.SetActive(false);
        }

        if (hasRifle && isChangeToPistol)
        {
            animator.SetBool(hasRifleHash, false);
            rifle.gameObject.SetActive(false);
            pistol.gameObject.SetActive(true);
        }

        if (!Fire && isFirePressed && hasRifle)
        {
            animator.SetBool(fireHash, true);

        }
        else
        {
            animator.SetBool(fireHash, false);
        }
<<<<<<< HEAD
        #endregion




=======
>>>>>>> parent of 519d549 (health)
    }


    private void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            downVelocity = -1.0f;
        }
        else
        {
            downVelocity += gravity * Time.deltaTime;
        }


        Vector3 directionDown = new Vector3(0f, downVelocity, 0f);

        controller.Move(directionDown * Time.deltaTime);

    }



    void PlayerDamageGivenEventHandler(float damage)
    {
        currentHealth -= damage;
    }
}
