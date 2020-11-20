using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObstacleScript : MonoBehaviour
{
    //public int myNum;
    private ObstacleGenerator generate;
    private Renderer render;
    public GameObject runner;

    private void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");
        generate = GetComponentInParent<ObstacleGenerator>();
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (render.transform.position.z < runner.transform.position.z - 40)
        {
            if (PhotonNetwork.IsMasterClient == true)
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
            //generate.Message(myNum);
            Debug.Log("Works!");

        }
    }


}
