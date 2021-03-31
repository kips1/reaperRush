using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

/*
 * Author: Sharp Coder 
 * 
 * The script that handles all network activity with the game lobby
 *
 * 
 */
public class PUN2_JoinGame : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI connectionStatus;
    public TextMeshProUGUI error;
    public TextMeshProUGUI waitingMessage;
    public InputField playerNameInput;


    // Our player name
    string playerName = "Please Enter Your Name";

    // Users are separated from each other by gameversion (which allows you to make breaking changes).
    string gameVersion = "0.9";
    // The list of created rooms
    List<RoomInfo> createdRooms = new List<RoomInfo>();

    Vector2 roomListScroll = Vector2.zero;
    bool joiningRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        // This makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnected)
        {
            // Set the App version before connecting
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
            // Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + cause.ToString() + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        //After we connected to Master server, join the Lobby
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("We have received the Room list");
        //After this callback, update the room list
        createdRooms = roomList;
    }
    void OnGUI()
    {
        GUI.Window(0, new Rect(Screen.width / 2 - 370, Screen.height / 2 - 100, 700, 250), LobbyWindow, "");
    }

    void LobbyWindow(int index)
    {
        //Connection Status and Room creation Button
        GUILayout.BeginHorizontal();

        connectionStatus.text = "Connection Status: " + PhotonNetwork.NetworkClientState;

        if (joiningRoom || !PhotonNetwork.IsConnected || PhotonNetwork.NetworkClientState != ClientState.JoinedLobby)
        {
            GUI.enabled = false;
        }

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        //Scroll through available rooms
        roomListScroll = GUILayout.BeginScrollView(roomListScroll, false, true);

        if (createdRooms.Count == 0)
        {
            waitingMessage.gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < createdRooms.Count; i++)
            {
                waitingMessage.gameObject.SetActive(false);
                GUILayout.BeginHorizontal("box");
                GUILayout.Label(createdRooms[i].Name, GUILayout.Width(400));
                GUILayout.Label(createdRooms[i].PlayerCount + "/" + createdRooms[i].MaxPlayers);

                if(createdRooms[i].PlayerCount == 0)
                {
                    Refresh();
                }

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Join"))
                {
                    if (playerName == "")
                    {
                        StartCoroutine(DisplayErrorFor(5.0f));
                    }
                    else
                    {
                        joiningRoom = true;

                        //Set our Player name
                        PhotonNetwork.NickName = playerName;

                        //Join the Room
                        PhotonNetwork.JoinRoom(createdRooms[i].Name);
                    }
                } 
                GUILayout.EndHorizontal();
            }
        }

        GUILayout.EndScrollView();

        //Set player name and Refresh Room button
        GUILayout.BeginHorizontal();

        //Player name text field
        playerName = playerNameInput.text;

        GUILayout.FlexibleSpace();

        GUI.enabled = (PhotonNetwork.NetworkClientState == ClientState.JoinedLobby || PhotonNetwork.NetworkClientState == ClientState.Disconnected) && !joiningRoom;


        GUILayout.EndHorizontal();

        if (joiningRoom)
        {
            GUI.enabled = true;
            GUI.Label(new Rect(900 / 2 - 50, 400 / 2 - 10, 100, 20), "Connecting...");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
        joiningRoom = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        //Set our player name
        PhotonNetwork.NickName = playerName;
        //Load the Scene called GameLevel (Make sure it's added to build settings)
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    // Refresh room list
    public void Refresh()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.NetworkClientState == ClientState.JoinedLobby)
            {
                //Re-join Lobby to get the latest Room list
                PhotonNetwork.JoinLobby(TypedLobby.Default);
            }
        else
            {
                //We are not connected, estabilish a new connection
                PhotonNetwork.ConnectUsingSettings();
            }
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    public void RemoveErrorMessages()
    {
        error.gameObject.SetActive(false);
    }

    IEnumerator DisplayErrorFor(float time)
    {
        error.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        error.gameObject.SetActive(false);
    }
}
