using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Kips
 * 
 * The script attached to the instance of a reaper which provides basic attributes and functionality
 * 
 * Version:
 */

public class Reaper : MonoBehaviour
{
    // fields accessible in inspector
    [SerializeField] private float speed = 25.0f;

    // Defines the objects that are associated directly to the reaper instance
    private GameObject ReaperObj;
    private GameObject ReaperUI;
    private GameObject rmController;
    private GameObject manager;
    public GameObject obstacle;
    public GameObject reaper;

    private CharacterController controller;
    private Vector3 obstacleSpawn;

    AudioSource fireSound;

    private float xDirection = 0;
    private float zDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        reaper = GameObject.FindGameObjectWithTag("Reaper");
        ReaperObj = GameObject.FindGameObjectWithTag("ReaperObj");
        ReaperUI = GameObject.FindGameObjectWithTag("ReaperUI");
        manager = GameObject.FindGameObjectWithTag("Manager");
        var aSources = GetComponents<AudioSource>();
        fireSound = aSources[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(xDirection, 0, zDirection);
        Vector3 velocity = direction * speed;

        if (GameObject.Find("Player(Clone)") == null)
        {
            zDirection = 0;
        }
        else
        {
            zDirection = 1;
        }

        if (manager.GetComponent<GameManager>().dead)
        {
            zDirection = 0;
        }


        if (Input.GetKey(KeyCode.D) && xDirection < 3.48f)
        {
            xDirection -= 0.01f;
        }

        else if (Input.GetKey(KeyCode.A) && xDirection > -4.48f)
        {
            xDirection += 0.01f;
        }

        else
        {
            xDirection = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && ReaperUI.GetComponent<ReaperUI>().activeObject.text.Equals("ROCK"))
        {
            obstacleSpawn = new Vector3(reaper.transform.position.x, 0, reaper.transform.position.z);
            ReaperObj.GetComponent<ReaperObj>().Generate(obstacleSpawn);
            
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && ReaperUI.GetComponent<ReaperUI>().activeObject.text.Equals("FIRE"))
        {
            obstacleSpawn = new Vector3(reaper.transform.position.x, 0, reaper.transform.position.z + 30);
            ReaperObj.GetComponent<ReaperObj>().GenerateFire(obstacleSpawn);
            fireSound.Play();

        }

        controller.Move(velocity * Time.deltaTime);

    }

    public void Reset()
    {
        PhotonNetwork.Destroy(gameObject);
        rmController.GetComponent<PUN2_RoomController>().Start();
        PhotonNetwork.LoadLevel("Game");
    }
}
