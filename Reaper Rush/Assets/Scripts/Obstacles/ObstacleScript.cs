﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

/*
 * Author: Josh, Kips
 * 
 * Hanldes the behaviour of the obstacles
 * 
 * Version:
 * 
 */

public class ObstacleScript : MonoBehaviourPun
{
    private GameObject reaperobj;
    private GameObject roomController;
    private GameObject manager;
    public GameObject runner;

    private ObstacleGenerator generate;

    private Renderer render;


    // Start is called before the first frame update
    private void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");
        generate = GetComponentInParent<ObstacleGenerator>();
        render = GetComponent<Renderer>();
        roomController = GameObject.FindGameObjectWithTag("RoomController");
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    // Deletes instance of obstacles when out of view
    private void Update()
    {
        if (runner != null)
        {
            if (render.transform.position.z < runner.transform.position.z - 80)
            {
                if (PhotonNetwork.IsMasterClient == true && gameObject.GetComponent<PhotonView>().IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }

    // Triggers damage function and animation when the obstacle detects a player instance
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (runner.GetComponent<Runner>().takeDamage == true)
            {
                runner.GetComponent<Runner>().TakeDamage(10);
            }

            if (PhotonNetwork.IsMasterClient == true && gameObject.GetComponent<PhotonView>().IsMine)
            {
                // Destroys the rock when collision occurs
                if (gameObject.CompareTag("Rock")) 
                { 
                    PhotonNetwork.Destroy(gameObject); 
                }
            }
        }

        
        //If the rock spawns where any of the power-ups are placed, it will not spawn the obstacle or if the rock spawns where a coin is placed, it will not spawn the obstacle
        if (collider.gameObject.layer == 8 || collider.gameObject.layer == 20 || collider.gameObject.layer == 15 || collider.gameObject.layer == 10)
        {
                photonView.RPC("DestroyObstacle", RpcTarget.AllBuffered);
        }
        
    }
    
    [PunRPC]
    void DestroyObstacle()
    {
        Destroy(gameObject);
    }
}