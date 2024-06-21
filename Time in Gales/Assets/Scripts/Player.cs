using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public CharacterController controller;
    public float speed = 6f;
    private float targetAngle;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public Transform cam;
    private float gravity = -9.81f;
    // [SerializeField] private float gravityMultiplier = 3.0f;
    private float downVelocity;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (controller.isGrounded)
        {
            downVelocity = -1.0f;
        }
        else
        {
            downVelocity += gravity * Time.deltaTime;
        }
        Vector3 direction = new Vector3(horizontalInput, downVelocity, verticalInput) * speed;

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
            // Rotate the player to face the mouse cursor
            transform.forward = lookDir;
        }

        //if (direction.sqrMagnitude >= 0.1f)
        //{
        //    targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg * cam.eulerAngles.y;
        //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        //    transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //    Vector3 moreDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //    controller.Move(moreDir.normalized * speed * Time.deltaTime);
        //}

    }
}
