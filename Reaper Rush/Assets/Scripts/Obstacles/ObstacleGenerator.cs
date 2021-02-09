using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Josh, Alex, Kips
 * 
 * Handles the generation of the obstacles on the track
 * 
 * Version:
 * 
 */

public class ObstacleGenerator : MonoBehaviourPunCallbacks
{
    // Defines the objects that are associated directly to the obstacles to be generated
    public GameObject obstacle;
    public GameObject gameManager;

    // Holds the current postion of the obstacle instance
    Vector3 position;

    int value = 1;
    public int lastPosition = 1;

    public float[] posX;
    public float[] posZ;

    bool next;

    // Start is called before the first frame update
    // Initialise fields
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Controller").transform.childCount < 9) {
            Generate();
        }
    }

    public void Generate()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            generateObstacles();
            return;
        }
    }

    // Generate instance of obstacles in random positions
    public void generateObstacles()
    {
        int i = Random.Range(0, 3);
        position.x = posX[i];
        position.z += posZ[i];
        GameObject obstacleClone = PhotonNetwork.Instantiate(obstacle.name, position, obstacle.transform.rotation);
        obstacleClone.transform.SetParent(this.transform);
        value += 1;
        next = false;
    }
}