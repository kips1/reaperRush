using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

/*
 * Author: Josh, Alex, Kips
 * 
 * The script attached to the instance of a runner which provides basic attributes and functionality
 * 
 * Version:
 * 
 */

public class Runner : MonoBehaviourPun
{
    // Fields accessible in inspector
    [SerializeField] public float speed = 25.0f;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private float jumpHeight = 10.0f;

    // Defines the objects that are associated directly to the runner instance
    private GameObject playerPosition;
    private GameObject rmController;
    public GameObject obstacleGenerator;
    public GameObject ObstacleGeneratorScript;
    public GameObject obstacle;
    public GameObject manager;
    
    private CharacterController controller;
    private HealthBar healthBar;
    public Animator anim;

    AudioSource coinSound;
    AudioSource powerUpSound;

    // Initialise distance counter state
    private int start = 0;

    private float yVelocity = 0.0f;
    private float xDirection = 0;
    private float zDirection = 0;
    public float distanceUnit;
    public float maxHealth;
    public float currentHealth;
    public float distanceValue;
    float timeLeft = 3.0f;
   
    // Fields for the player's health state
    public bool takeDamage = true;
    public bool hasLost;

    // Start is called before the first frame update
    // Initialise fields
    void Start()
    {
        obstacleGenerator = GameObject.FindWithTag("ObstacleGenerator");
        rmController = GameObject.FindWithTag("RoomController");
        anim = GameObject.FindGameObjectWithTag("Player_Running").GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager");
        controller = GetComponent<CharacterController>();
        var aSources = GetComponents<AudioSource>();
        coinSound = aSources[0];
        powerUpSound = aSources[1];
        maxHealth = 100;
        currentHealth = 100;
        hasLost = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(xDirection, 0, zDirection);
        Vector3 velocity = direction * speed;

        // Sets the movement when an instance is created
        if (GameObject.Find("Reaper(Clone)") != null)
        {
            photonView.RPC("ready", RpcTarget.AllBuffered, true);
        }

        if (manager.GetComponent<GameManager>().bothReady)
        {
            zDirection = 1;
        }
        // Calls the Distance function when the runner's start state is 0 and the runner is moving forward
        if(start == 0 && zDirection == 1)
        {
            InvokeRepeating(nameof(Distance), 0, 1 / speed);
            start = 1;
        }
        // Enables the obstacle generator after a specified distance
        if (distanceUnit == distanceValue + 30)
        {
            obstacleGenerator.SetActive(true);
        }
       
        // Checks if the runner is on the ground
        if (controller.isGrounded)
        {
            // Allows the runner to jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                photonView.RPC("syncAnimation", RpcTarget.AllBuffered, "isJumping", true);
                yVelocity = jumpHeight;
            }
            // Move left
            else if (Input.GetKey(KeyCode.A) && xDirection > -4.48f)
            {
                xDirection -= 0.045f;
            } 
            // Move right
            else if (Input.GetKey(KeyCode.D) && xDirection < 3.48f)
            { 
                xDirection += 0.045f;
            }
            // Stop moving left/right
            else
            {
                xDirection = 0;
            }
        } 
        // Plays jumping animation and makes the runner fall
        else
        {
            photonView.RPC("syncAnimation", RpcTarget.AllBuffered, "isJumping", false);
            yVelocity -= gravity;
        }

        // Checks when runner has died
        if (currentHealth <= 0)
        {
            speed = 0;
            distanceUnit += 0;
            hasLost = true;
            photonView.RPC("syncAnimation", RpcTarget.AllBuffered, "hasDied", true);
            photonView.RPC("syncAnimation", RpcTarget.AllBuffered, "Die");
            // Checks that there is more than 1 player
            if (PhotonNetwork.PlayerList.Length > 1)
            {
                // Sends final score to game manager
                photonView.RPC("changeDistance", RpcTarget.AllBuffered, distanceUnit);
                photonView.RPC("changeDead", RpcTarget.AllBuffered, hasLost);
            }
        }

        // Changes Y position when jumping
        velocity.y = yVelocity;
        // Balances game speed to prevent varying framerate advantage
        controller.Move(velocity * Time.deltaTime);
    }

    // Increments the distance counter
    void Distance()
    {
        if (currentHealth > 0)
        {
            distanceUnit = distanceUnit + 1;
        }

        else
        {
            distanceUnit += 0;
        }
    }

    // Collision detection for collectibles
    private void OnTriggerEnter(Collider other)
    {
        distanceValue = distanceUnit;

        // Handles invulnerability powerup 
        if (other.gameObject.layer == 20)
        {
            Destroy(other.gameObject);         
            powerUpSound.Play();
            StartCoroutine(invulnerableActiveFor(5));
        }

        // Handles health powerup
        if (other.gameObject.layer == 15)
        {
            Destroy(other.gameObject);
            currentHealth += 5;
            powerUpSound.Play();
        }

        // Handles coin powerup
        if (other.gameObject.layer == 10)
        {
            powerUpSound.Play();
            Destroy(other.gameObject);
            for(int i = 30; i < 54; i += 3)
            {
                Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + i), Quaternion.identity);
            }
            obstacleGenerator.SetActive(false);
        }

        // Handles coin collectible
        if (other.gameObject.layer == 8)
        {
            coinSound.Play();
            CoinAddScript.coinAmount += 1;
            Destroy(other.gameObject);
        }

        // Destroys the rock when collision occurs
        if (other.gameObject.layer == 25)
        {
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {  
            currentHealth -= damage;
    }

    // Makes runner invulnerable for a given time
    IEnumerator invulnerableActiveFor(float time)
    {
        takeDamage = false;
        yield return new WaitForSeconds(time);
        takeDamage = true;
    }

    // Updates distance score in game manager
    [PunRPC]
    void changeDistance(float distance)
    {
        manager.GetComponent<GameManager>().distanceScore = distance;
    }

    // Change runner's state in game manager
    [PunRPC]
    void changeDead(bool isDead)
    {
        manager.GetComponent<GameManager>().dead = isDead; ;
    }


    [PunRPC]
    void syncAnimation(string anim, bool set)
    {
        this.anim.SetBool(anim, set);
    }

    [PunRPC]
    void syncAnimation(string anim)
    {
        this.anim.SetTrigger(anim);
    }

    [PunRPC]
    void ready(bool runnerReady)
    {
        manager.GetComponent<GameManager>().runnerReady = runnerReady;
    }
}