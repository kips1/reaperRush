using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class swap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient &&  PhotonNetwork.PlayerList.Length > 1)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
