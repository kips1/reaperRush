using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Collectables : MonoBehaviourPun
{
    public GameObject runner;
    // Start is called before the first frame update
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (runner != null)
        {
            if (this.transform.position.z < runner.transform.position.z - 40)
            {
                if (PhotonNetwork.IsMasterClient && photonView.IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    
    }
   
}

