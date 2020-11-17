﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PUN2_RoomController : MonoBehaviourPunCallbacks
{
    //Player instance prefab, must be located in the Resources folder
    public GameObject playerPrefab;
    public GameObject reaperPrefab;
    //Player spawn point
    public Transform runnerSpawnPoint;
    public Transform reaperSpawnPoint;
    public Camera PlayerCamera;

    // Start is called before the first frame update
    void Start()
    {
        //In case we started this demo with the wrong scene being active, simply load the menu scene
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Is not in the room, returning back to Lobby");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
            return;
        }

        //We're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
        if (PhotonNetwork.IsMasterClient == false)
        {
            PhotonNetwork.Instantiate(reaperPrefab.name, reaperSpawnPoint.position, Quaternion.identity, 0);
        }
        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name, runnerSpawnPoint.position, Quaternion.identity, 0);
        }
        
    }

    void OnGUI()
    {
        if (PhotonNetwork.CurrentRoom == null)
            return;

        //Leave this Room
        if (GUI.Button(new Rect(5, 5, 125, 25), "Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }

        //Show the Room name
        GUI.Label(new Rect(135, 5, 200, 25), PhotonNetwork.CurrentRoom.Name);

        //Show the list of the players connected to this Room
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //Show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": MasterClient" : "");
            GUI.Label(new Rect(5, 35 + 30 * i, 200, 25), PhotonNetwork.PlayerList[i].NickName + isMasterClient);
        }
    }

    public override void OnLeftRoom()
    {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
