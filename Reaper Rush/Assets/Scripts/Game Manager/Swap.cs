using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Josh, Kips
 * 
 * Handles the role swap when first runner has died
 * 
 * Version:
 * 
 */

public class Swap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Switches master client to the other role
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.PlayerList.Length > 1)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]);
        }

        // Changes scene after master client has switched
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RoleSwap")
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}