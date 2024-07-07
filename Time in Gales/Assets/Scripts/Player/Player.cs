using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform pistol;
    [SerializeField] Transform rifle;
    [SerializeField] CharacterController controller;


    // Specs of the Player
    public float speed = 6f;
    private float gravity = -9.81f;
    private float downVelocity;
    private float shieldTime = 6f;
    private float shieldCooldownTime = 30f;
    private float timeSinceShieldEnabled;
    private float timeSinceShieldDisabled;
    [SerializeField] GameObject walkEffect;

    private Vector3 direction;
    int hasRifleHash;
    int fireHash;
    [SerializeField] float MaxHealth;
    float currentHealth;
    bool shield;

    // Start is called before the first frame update
    void Start()
    {
        hasRifleHash = Animator.StringToHash("hasRifle");
        fireHash = Animator.StringToHash("Fire");
        currentHealth = MaxHealth;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDamageEvent.AddListener(PlayerDamageGivenEventHandler);
        }
    }

    IEnumerator InstantiateWalkEffect(GameObject walkEffect)
    {
        if (walkEffect != null)
        {
            GameObject myeffect = Instantiate(walkEffect, transform.position, Quaternion.identity);
            myeffect.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            myeffect.SetActive(false);
            Debug.Log("traill");
        }
    }



    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerDeadEvent.Invoke();
            }
        }


        #region Movement Logic

        if (controller != null)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput) * speed;

            controller.Move(direction * Time.deltaTime);

            if (horizontalInput != 0 || verticalInput != 0)
            {
                StartCoroutine(InstantiateWalkEffect(walkEffect));
            }

            if (Time.timeScale > 0)
            { // Aiming logic
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
            if (animator != null)
            {
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
                    if (rifle != null) rifle.gameObject.SetActive(true);
                    if (pistol != null) pistol.gameObject.SetActive(false);
                }

                if (hasRifle && isChangeToPistol)
                {
                    animator.SetBool(hasRifleHash, false);
                    if (rifle != null) rifle.gameObject.SetActive(false);
                    if (pistol != null) pistol.gameObject.SetActive(true);
                }

                if (!Fire && isFirePressed && hasRifle)
                {
                    animator.SetBool(fireHash, true);

                }
                else
                {
                    animator.SetBool(fireHash, false);
                }
            }
        }
        #endregion

        // Shield Logic


        if(Input.GetKeyDown(KeyCode.Space) && timeSinceShieldDisabled > shieldCooldownTime && !shield)
        {
            ShieldEnabled();
            timeSinceShieldEnabled = 0;
            Debug.Log("Shield Enabled");
        }

        if(timeSinceShieldEnabled > shieldTime && shield)
        {
            ShieldDisabled();
            timeSinceShieldDisabled = 0;
            Debug.Log("Shield Disabled");
        }

        timeSinceShieldEnabled += Time.deltaTime;
        timeSinceShieldDisabled += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Time.timeScale > 0)
        {
            if (controller != null)
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

        }
        
    }
    void PlayerDamageGivenEventHandler(float damage)
    {
        if(!shield)
        {
            currentHealth -= damage;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerHealthChangeEvent.Invoke(damage);
            }
        }
    }

    void ShieldEnabled()
    {
        shield = true;
    }

    void ShieldDisabled()
    {
        shield = false;
    }
}

