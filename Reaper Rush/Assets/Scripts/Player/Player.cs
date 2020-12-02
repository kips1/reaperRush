using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 25.0f;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private float jumpHeight = 10.0f;


    private GameObject playerPosition;
    private CharacterController controller;
    private HealthBar healthBar;
    public Animator anim;


    private float yVelocity = 0.0f;
    private float xDirection = 0;
    private float zDirection = 1;
    public float distanceUnit;
    public float maxHealth;
    public float currentHealth;
    public bool hasLost;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = 100;
        currentHealth = 100;
        hasLost = false;
        controller = GetComponent<CharacterController>();
        InvokeRepeating("distance", 0, 1 / speed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(xDirection, 0, zDirection);
        Vector3 velocity = direction * speed;
        

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(anim.GetBool("IsJumping"));
                anim.SetBool("IsJumping", true);
                anim.Play("Jump", 1);
                yVelocity = jumpHeight;

            }

            else if (Input.GetKey(KeyCode.A) && xDirection > -4.48f)
            {
                xDirection -= 0.01f;
            } 

            else if (Input.GetKey(KeyCode.D) && xDirection < 3.48f)
            {
                xDirection += 0.01f;
            }

            else
            {
                xDirection = 0;
            }
        } 
        else
        {
            yVelocity -= gravity;
        }

        if (currentHealth <= 0)
        {
            speed = 0;
            distanceUnit += 0;
            hasLost = true;
        }

        velocity.y = yVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    void distance()
    {
        if (currentHealth > 0)
        {
            distanceUnit = distanceUnit + 1;
        } else
        {
            distanceUnit += 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            CoinAddScript.coinAmount += 1;
            Destroy(other.gameObject);
        }
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

}
