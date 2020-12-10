using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ObstacleScript : MonoBehaviourPun

{
    //public int myNum;
    private ObstacleGenerator generate;
    private Renderer render;
    public GameObject runner;
    private GameObject reaperobj;
    private GameObject roomController;
    private GameObject manager;

    private void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");
        generate = GetComponentInParent<ObstacleGenerator>();
        render = GetComponent<Renderer>();
        roomController = GameObject.FindGameObjectWithTag("RoomController");
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    private void Update()
    {
        if (render.transform.position.z < runner.transform.position.z - 40)
        {
            if (PhotonNetwork.IsMasterClient == true && photonView.IsMine && manager.GetComponent<GameManager>().round == 0)
            {
                PhotonNetwork.Destroy(gameObject);

            } else if (!PhotonNetwork.IsMasterClient == true && !photonView.IsMine && manager.GetComponent<GameManager>().round == 1)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            render.material.color = Color.green;
            runner.GetComponent<Player>().TakeDamage(25);
            //generate.Message(myNum);
            //Debug.Log("Works!");
            //if(SceneManager.GetActiveScene().name == "Game") {
            //    SceneManager.LoadScene("Game2");
            //    PhotonNetwork.LoadLevel("Game2");
            //}

        }
    }
    




}
