using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Josh, Kips
 * 
 * Retains all data that is necessary for the duration of the game such as the player scores
 * 
 * Version:
 * 
 */

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager _instance;

    private GameObject runner;
    private GameObject reaper;
    private GameObject rmController;

    public Photon.Realtime.Player s;

    public string player2;

    public int round;
    private int coinsCollected;

    public float distanceScore;
    public float distanceScored;
    public float secondScore;

    public bool finalRound;
    public bool lastRound;
    public bool firstDead;
    public bool secondDead;
    public bool dead;

    public static GameManager Instance { get { return _instance; } }

    // Executes the following when script is being loaded
    private void Awake()
    {
        // Ensures only one instance of this class is created
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rmController = GameObject.FindGameObjectWithTag("RoomController");
        DontDestroyOnLoad(gameObject);
        s = PhotonNetwork.MasterClient;
        round = 0;
        finalRound = false;
        lastRound = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Removes the manager instance when in the lobby
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameLobby")
        {
            Destroy(this.gameObject);
        }

        
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game")
        {
            runner = GameObject.FindGameObjectWithTag("Player");
            reaper = GameObject.FindGameObjectWithTag("Reaper");
            finalRound = true;
            if (round == 0 && PhotonNetwork.IsMasterClient)
            {
                if (runner.GetComponent<Runner>().hasLost && round == 0 && PhotonNetwork.IsMasterClient)
                {
                    distanceScored = distanceScore;
                    round++;
                    finalRound = true;
                    /*if (PhotonNetwork.IsMasterClient)
                    {
                        runner.GetComponent<Player>().Reset();
                        PhotonNetwork.Destroy(runner);
                    } else if (!PhotonNetwork.IsMasterClient)
                    {
                        reaper.GetComponent<Reaper>().Reset();
                        PhotonNetwork.Destroy(reaper);

                    }*/


                    StartCoroutine(ExecuteAfter(5.0f));


                    //this.gameObject.tag = "mainManager";
                }
            }
            if (PhotonNetwork.IsMasterClient == false)
            {
                round = 1;  
            }
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RoleSwap" && round == 1)
        {
            distanceScored = distanceScore;
            firstDead = dead;
            dead = false;
            runner = null;
            if (PhotonNetwork.IsMasterClient && finalRound && PhotonNetwork.PlayerList.Length > 1)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]);
            }
            //PhotonNetwork.LoadLevel("Game");
            round++;
            if (PhotonNetwork.PlayerList.Length > 1)
            {
                player2 = PhotonNetwork.PlayerListOthers[0].NickName;
            }

        }
        if (s != PhotonNetwork.MasterClient && round == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
            round = 5;
            lastRound = true;
        }
        else if (s != PhotonNetwork.MasterClient && round == 1 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
            round = 5;
            lastRound = true;
        }

        if(s != PhotonNetwork.MasterClient && !PhotonNetwork.IsMasterClient)
        {
            round = 5;
        }

        if (round == 5)
        {
            secondScore = distanceScore;
            secondDead = dead;
            if (secondDead && PhotonNetwork.IsMasterClient)
            {
                round = 10;
                StartCoroutine(ExecuteLast(10.0f));
            }
            if (!PhotonNetwork.IsMasterClient && secondDead)
            {
                round = 10;
            }
            //Debug.Log(secondScore + "thise is first" + distanceScored);
        }

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameEnd" && round == 10 && PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                round = 11;
                StartCoroutine(ExecuteBackToLobby(5.0f));
            }
        }


    }

    IEnumerator ExecuteAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        PhotonNetwork.LoadLevel("RoleSwap");
    }
    IEnumerator ExecuteLast(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PhotonNetwork.LoadLevel("GameEnd");
    }

    IEnumerator ExecuteBackToLobby(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PhotonNetwork.LeaveRoom();


    }

    public override void OnLeftRoom()
    {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    }

}