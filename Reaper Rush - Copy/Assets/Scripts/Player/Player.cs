using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public float distanceValue;
    public GameObject obstacleGenerator;   
    [SerializeField] private float speed = 25.0f;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private float jumpHeight = 10.0f;
    private GameObject playerPosition;
    private CharacterController controller;
    private float yVelocity = 0.0f;
    private float xDirection = 0;
    private float zDirection = 1;
    public float distanceUnit;
    public GameObject ObstacleGeneratorScript;
    public GameObject obstacle;
    
    //ObstacleGenerator sn = GameObject.FindObjectOfType(typeof(Player)) as ObstacleGenerator;
    // Start is called before the first frame update
    void Start()
    {
        obstacleGenerator = GameObject.FindWithTag("ObstacleGenerator");
        controller = GetComponent<CharacterController>();
    InvokeRepeating("distance", 0, 1 / speed);
    }

    // Update is called once per frame
    void Update()

    {
        if (distanceUnit == distanceValue + 30)
        {
            Debug.Log("test");
            obstacleGenerator.SetActive(true);

            //Instantiate(GameObject.FindWithTag("ObstacleGenerator"), new Vector3(0, 0, 0), Quaternion.identity);

        }
        Vector3 direction = new Vector3(xDirection, 0, zDirection);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        distanceValue = distanceUnit;
        if (other.gameObject.layer == 8)
        {
            
            CoinAddScript.coinAmount += 1;
            Destroy(other.gameObject);
            

        }

        if (other.gameObject.layer == 10)
        {
            
            Destroy(other.gameObject);
            
            //Debug.Log("test");
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 30), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 35), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 40), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 45), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 50), Quaternion.identity);
            Instantiate(GameObject.FindWithTag("Coin"), new Vector3(Random.Range(-4, 4), 2, distanceUnit + 55), Quaternion.identity);
            obstacleGenerator.SetActive(false);
            
            //GameObject obstacleClone = PhotonNetwork.Instantiate(obstacle.name, new Vector3(Random.Range(-4, 4), 2, distanceUnit + 30), obstacle.transform.rotation);
            //Instantiate(GameObject.FindWithTag("ObstacleGenerator"), new Vector3(0, 0, 0), Quaternion.identity);
            //sn.Generate();

            //object.GetComponent<ObstacleGenerator>().Generate();

            

        }
        




    }


}
