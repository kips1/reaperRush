using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Photon.Pun;

public class Reaper : MonoBehaviour
{
    [SerializeField] private float speed = 25.0f;
    private CharacterController controller;
    private float xDirection = 0;
    private float zDirection = 1;
    public GameObject obstacle;
    public GameObject reaper;
    private Vector3 obstacleSpawn;
    private GameObject ReaperObj;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        reaper = GameObject.FindGameObjectWithTag("Reaper");
        ReaperObj = GameObject.FindGameObjectWithTag("ReaperObj");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(xDirection, 0, zDirection);
        Vector3 velocity = direction * speed;


        if (Input.GetKey(KeyCode.RightArrow) && xDirection < 3.48f)
        {
            xDirection -= 0.01f;
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && xDirection > -4.48f)
        {
            xDirection += 0.01f;
        }

        else
        {
            xDirection = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            obstacleSpawn = new Vector3(reaper.transform.position.x, 0, reaper.transform.position.z);
            ReaperObj.GetComponent<ReaperObj>().Generate(obstacleSpawn);
            //PhotonNetwork.Instantiate(obstacle.name, obstacleSpawn, obstacle.transform.rotation);
        }

        controller.Move(velocity * Time.deltaTime);

    }
}
