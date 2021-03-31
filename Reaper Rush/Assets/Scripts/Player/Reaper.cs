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
 * 
 */

public class Reaper : MonoBehaviourPun
{
    // fields accessible in inspector
    [SerializeField] private float speed = 22.0f;

    // Defines the objects that are associated directly to the reaper instance
    private GameObject ReaperObj;
    private GameObject ReaperUI;
    private GameObject rmController;
    public GameObject manager;
    public GameObject obstacle;
    public GameObject reaper;

    private CharacterController controller;
    private Vector3 obstacleSpawn;

    AudioSource fireSound;

    private float xDirection = 0;
    private float zDirection = 0;

    public float cooldownTime;
    // public float time1 = 1;
    private float nextFireTime = 0;
    private float nextShadeTime = 0;

    // Start is called before the first frame update
    // Initialise fields
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

        // Starts the movement when an instance of runner is created
        if (GameObject.Find("Player(Clone)") != null)
        {
            photonView.RPC("ReaperReady", RpcTarget.AllBuffered, true);
        }

        if (manager.GetComponent<GameManager>().bothReady)
        {
            zDirection = 1;
        }

        // Stops movement when runner has died
        if (manager.GetComponent<GameManager>().dead)
        {
            zDirection = 0;
        }

        // Move right
        if (Input.GetKey(KeyCode.D) && xDirection < 3.48f)
        {
            xDirection -= 0.01f;
        }

        // Move left
        else if (Input.GetKey(KeyCode.A) && xDirection > -4.48f)
        {
            xDirection += 0.01f;
        }

        // Stop moving left/right
        else
        {
            xDirection = 0;
        }

        // Spawn shade obstacle
        if (Time.time > nextShadeTime)
        {

            if (Input.GetKeyDown(KeyCode.Mouse0) && ReaperUI.GetComponent<ReaperUI>().activeObject.text.Equals("ROCK"))
            {
                print("Shade Summoned");
                nextShadeTime = Time.time + 5;
                obstacleSpawn = new Vector3(reaper.transform.position.x, 0, reaper.transform.position.z);
                ReaperObj.GetComponent<ReaperObj>().Generate(obstacleSpawn);

                gameObject.GetComponent<ShadeCooldown>().timeLeft2 = 5.0f;
                gameObject.GetComponent<ShadeCooldown>().timer2.enabled = true;

            }
        }

        // Spawn fire
        if (Time.time > nextFireTime) 
        {
            
            if (Input.GetKeyDown(KeyCode.Mouse0) && ReaperUI.GetComponent<ReaperUI>().activeObject.text.Equals("FIRE"))
            {

                print("ability used, cooldown started");
                nextFireTime = Time.time + 5;

                obstacleSpawn = new Vector3(reaper.transform.position.x, 0, reaper.transform.position.z + 30);
                ReaperObj.GetComponent<ReaperObj>().GenerateFire(obstacleSpawn);
                fireSound.Play();

                gameObject.GetComponent<FireCooldown>().timeLeft1 = 5.0f;
                gameObject.GetComponent<FireCooldown>().timer1.enabled = true;



            }
        }

        

        // Balances game speed to prevent varying framerate advantage
        controller.Move(velocity * Time.deltaTime);


        if (manager.GetComponent<GameManager>().scoreTrack == 200)
        {
            speed = 23.0f;
        }
        if (manager.GetComponent<GameManager>().scoreTrack == 400)
        {
            speed = 24.0f;
        }
        if (manager.GetComponent<GameManager>().scoreTrack == 600)
        {
            speed = 25.0f;
        }
        if (manager.GetComponent<GameManager>().scoreTrack == 800)
        {
            speed = 26.0f;
        }
        if (manager.GetComponent<GameManager>().scoreTrack == 1000)
        {
            speed = 27.0f;
        }
        if (manager.GetComponent<GameManager>().scoreTrack == 1300)
        {
            speed = 28.0f;
        }
        if (manager.GetComponent<GameManager>().scoreTrack == 1600)
        {
            speed = 29.0f;
        }
        if (manager.GetComponent<GameManager>().scoreTrack == 1900)
        {
            speed = 30.0f;
        }
    }

    [PunRPC]
    void ReaperReady(bool reaperReady)
    {
        manager.GetComponent<GameManager>().reaperReady = reaperReady;
    }
}