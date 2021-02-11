using System.Collections;
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
        if (render.transform.position.z < runner.transform.position.z - 80)
        {
            if (PhotonNetwork.IsMasterClient == true && photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
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
            runner.GetComponent<Runner>().anim.SetTrigger("Collide");
        }

        //If the rock spawns where a coin is placed, it will not spawn the obstacle
        if (collider.gameObject.CompareTag("Coin"))
        {
            Destroy(gameObject);
        }
        //If the rock spawns where any of the power-ups are placed, it will not spawn the obstacle
        if (collider.gameObject.layer == 20)
        {
            Destroy(gameObject);
        }

        if (collider.gameObject.layer == 15)
        {
            Destroy(gameObject);
        }

        if (collider.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }
}