using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private float jumpHeight = 10.0f;
    private GameObject playerPosition;
    private CharacterController controller;
    private float yVelocity = 0.0f;
    private float xDirection = 0;
    public Text distanceMoved;
    private float distanceUnit;

    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController>();
        InvokeRepeating("distance", 0, 1 / speed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(xDirection, 0, 1);
        Vector3 velocity = direction * speed;

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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

        velocity.y = yVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    void distance()
    {
        distanceUnit = distanceUnit + 1;
        distanceMoved.text = distanceUnit.ToString();
    }

    void getBoundry()
    {
        
    }
}
