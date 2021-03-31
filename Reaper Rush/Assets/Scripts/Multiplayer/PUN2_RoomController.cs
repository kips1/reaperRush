using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Reflection;
using Photon.Realtime;

/*
 * Author: Sharp Coder
 * 
 * The script that handles all network activity for joining/controlling a room
 *
 * 
 */

public class PUN2_RoomController : MonoBehaviourPunCallbacks
{
    //Player instance prefab, must be located in the Resources folder
    public GameObject UI;
    public GameObject ReaperUI;
    public GameObject playerPrefab;
    public GameObject reaperPrefab;
    //Player spawn point
    public Transform runnerSpawnPoint;
    public Transform reaperSpawnPoint;
    public Camera PlayerCamera;
    private GameObject manager;

    public bool gameEnded;

    // Start is called before the first frame update
    public void Start()
    {

        //In case we started this demo with the wrong scene being active, simply load the menu scene
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Is not in the room, returning back to Lobby");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
            return;
        }


        //We're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, runnerSpawnPoint.position, Quaternion.identity, 0);
                PhotonNetwork.Instantiate(UI.name, runnerSpawnPoint.position, Quaternion.identity, 0);
            }
            else if(PhotonNetwork.IsMasterClient == false)
            {
                PhotonNetwork.Instantiate(reaperPrefab.name, reaperSpawnPoint.position, Quaternion.identity, 0);
                PhotonNetwork.Instantiate(ReaperUI.name, reaperSpawnPoint.position, Quaternion.identity, 0);
            }

    }

    private void Update()
    {
            if (GameObject.FindGameObjectWithTag("Player") != null && GameObject.FindGameObjectWithTag("Player").GetComponent<Runner>().zDirection == 1 && GameObject.FindGameObjectWithTag("Reaper") == null ||
                GameObject.FindGameObjectWithTag("Reaper") != null && GameObject.FindGameObjectWithTag("Player") == null && PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(DisplayMessageFor(5));
            }
    }

    void OnGUI()
    {
        if (PhotonNetwork.CurrentRoom == null)
            return;


        GameObject.Find("LeaveButton").GetComponent<Button>().onClick.AddListener(() =>
            {
            if (PhotonNetwork.NetworkClientState != Photon.Realtime.ClientState.Leaving)
            {
                PhotonNetwork.LeaveRoom();
            }
        });



        //Show the Room name
        GameObject.Find("Room").GetComponentInChildren<Text>().text = "Room: " + PhotonNetwork.CurrentRoom.Name;

        //Show the list of the players connected to this Room
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //Show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? "Host: " : "");
            if (PhotonNetwork.PlayerList[i].IsMasterClient)
            {
                GameObject.Find("MasterClient").GetComponentInChildren<Text>().text = isMasterClient + PhotonNetwork.PlayerList[i].NickName;
            }
            else
            {
                GameObject.Find("OtherClient").GetComponentInChildren<Text>().text = isMasterClient + PhotonNetwork.PlayerList[i].NickName;
            }
        }
    }

    public override void OnLeftRoom()
    {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        PhotonNetwork.Disconnect();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().playersConnected = false;
    }

    IEnumerator DisplayMessageFor(float time)
    {
        foreach (Transform objects in GameObject.FindGameObjectWithTag("DisconnectMessage").GetComponentInChildren<Transform>())
        {
            objects.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(time);
        if (PhotonNetwork.NetworkClientState != ClientState.Leaving && PhotonNetwork.NetworkClientState != ClientState.Disconnected)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}