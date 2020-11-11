using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject playerPosition;
    private GameObject obstacle;
    private CharacterController controller;
    private Player runner;
    private float xDirection = 0;
    private float zDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(xDirection, 0, zDirection);
        Vector3 velocity = direction * speed;


        if (Input.GetKey(KeyCode.RightArrow) && xDirection < 3.48f)
        {
            xDirection += 0.01f; 
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && xDirection > -4.48f)
        {
            xDirection -= 0.01f;
        }

        else
        {
            xDirection = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(obstacle, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0));
        }

        controller.Move(velocity * Time.deltaTime);
        
    }
}
