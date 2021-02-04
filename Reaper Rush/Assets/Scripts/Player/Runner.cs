using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

/*
 * Author: Josh, Alex, Kips
 * 
 * The main script attached to the instance of a runner which provides basic attributes and functionality
 * 
 * Version:
 */

public class Runner : MonoBehaviourPun
{
    // fields that are accessible within the inspector
    [SerializeField] public float speed = 25.0f;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private float jumpHeight = 10.0f;

    // defines the objects that are associated directly to the runner instance
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

    private int start = 0;

    private float yVelocity = 0.0f;
    private float xDirection = 0;
    private float zDirection = 0;
    public float distanceUnit;
    public float maxHealth;
    public float currentHealth;
    public float distanceValue;
    float timeLeft = 3.0f;
   
    private bool isJumping = false;
    public bool takeDamage = true;
    public bool hasLost;

    // Start is called before the first frame update
    void Start()
    {
        obstacleGenerator = GameObject.FindWithTag("ObstacleGenerator");
        rmController = GameObject.FindWithTag("RoomController");
        
        maxHealth = 100;
        currentHealth = 100;
        hasLost = false;
        controller = GetComponent<CharacterController>();


        anim = GameObject.FindGameObjectWithTag("Player_Running").GetComponent<Animator>();

        var aSources = GetComponents<AudioSource>();
        coinSound = aSources[0];
        powerUpSound = aSources[1];
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    void Update()
    {


        if (GameObject.Find("Reaper(Clone)") != null)
        {
            zDirection = 1;
        }

        if(start == 0 && zDirection == 1)
        {
            InvokeRepeating("distance", 0, 1 / speed);
            start = 1;
        }

        timeLeft -= Time.deltaTime;
        if (distanceUnit == distanceValue + 30)
        {
            Debug.Log("test");
            obstacleGenerator.SetActive(true);
            
        }
       

        Vector3 direction = new Vector3(xDirection, 0, zDirection);
        Vector3 velocity = direction * speed;


        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isJumping", true);
                yVelocity = jumpHeight;
            }

            else if (Input.GetKey(KeyCode.A) && xDirection > -4.48f)
            {
                
                xDirection -= 0.045f;
            } 

            else if (Input.GetKey(KeyCode.D) && xDirection < 3.48f)
            {
                
                xDirection += 0.045f;
            }

            else if (Input.GetKey(KeyCode.I) && xDirection < 3.48f)
            {
                currentHealth += 10;
            }
            else
            {
                xDirection = 0;
            }

        } 
            
        else
        {
            anim.SetBool("isJumping", false);
            yVelocity -= gravity;
        }

        
        if (currentHealth <= 0)
        {
            if (PhotonNetwork.PlayerList.Length > 1)
            {
                photonView.RPC("changeDistance", RpcTarget.AllBuffered, distanceUnit);
            }
            speed = 0;
            distanceUnit += 0;
            hasLost = true;
            anim.SetBool("hasDied", true);
            anim.SetTrigger("Die");
            if (PhotonNetwork.PlayerList.Length > 1)
            {
                photonView.RPC("changeDead", RpcTarget.AllBuffered, hasLost);
            }
            //GameObject.FindGameObjectWithTag("UI").GetComponent<Text>().enabled = true;
        }

        velocity.y = yVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Reset()
    {
        rmController.GetComponent<PUN2_RoomController>().Start();
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
        distanceValue = distanceUnit;

        if (other.gameObject.layer == 20)
        {
            
            Destroy(other.gameObject);         
            powerUpSound.Play();
            StartCoroutine(invulnerableActiveFor(5));
        }

        if (other.gameObject.layer == 15)
        {
            Destroy(other.gameObject);
            currentHealth += 5;
            powerUpSound.Play();
        }

        if (other.gameObject.layer == 8)
        {
            coinSound.Play();
            CoinAddScript.coinAmount += 1;
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 10)
        {
            powerUpSound.Play();
            Destroy(other.gameObject);

            //Debug.Log("test");
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 30), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 33), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 36), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 39), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 42), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 45), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 48), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 52), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 53), Quaternion.identity);
            obstacleGenerator.SetActive(false);

        }

        if (other.gameObject.layer == 25)
        {
            Destroy(other.gameObject);
        }




    }
    public void TakeDamage(float damage)
    {  
            currentHealth -= damage;  
        
    }



    IEnumerator invulnerableActiveFor(float time)
    {
        takeDamage = false;
        yield return new WaitForSeconds(time);
        takeDamage = true;

    }

    [PunRPC]
    void changeDistance(float distance)
    {
        manager.GetComponent<GameManager>().distanceScore = distance;
    }

    [PunRPC]
    void changeDead(bool isDead)
    {
        manager.GetComponent<GameManager>().dead = isDead; ;
    }
}
