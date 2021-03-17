using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Collectables : MonoBehaviourPun
{
    public GameObject runner;

    private Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");
        render = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
            if (this.transform.position.z < runner.transform.position.z - 40)
            {
                if (PhotonNetwork.IsMasterClient == true && photonView.IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
    }
}

