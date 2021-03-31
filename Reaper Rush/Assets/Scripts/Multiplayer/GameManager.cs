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
    public string currentRunner;


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
    public bool runnerReady;
    public bool reaperReady;
    public bool runnerReadytemp;
    public bool reaperReadytemp;
    public bool bothReady;
    public bool playersConnected;


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
        playersConnected = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Removes the manager instance when in the lobby
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameLobby")
        {
            Destroy(this.gameObject);
        }

        // Keeps track of first client's score
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game")
        {
            runner = GameObject.FindGameObjectWithTag("Player");
            reaper = GameObject.FindGameObjectWithTag("Reaper");
            if (currentRunner != null && runner != null && GameObject.Find(currentRunner) != null)
            {
                runner.GetComponent<Runner>().anim = GameObject.FindGameObjectWithTag(currentRunner).GetComponent<Animator>();
            }
            finalRound = true;
            runnerReadytemp = runnerReady;
            reaperReadytemp = reaperReady;
            // Checks when the first client has finished and loads role swap scene
            if (round == 0 && PhotonNetwork.IsMasterClient)
            {
                if (runner.GetComponent<Runner>().hasLost && round == 0 && PhotonNetwork.IsMasterClient)
                {
                    distanceScored = distanceScore;
                    round++;
                    finalRound = true;
                    StartCoroutine(ExecuteAfter(5.0f));
                }
            }

            // Updates the round for the other client
            if (PhotonNetwork.IsMasterClient == false)
            {
                round = 1;  
            }
        }

        // Saves first client's score and state, then clears for second client
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RoleSwap" && round == 1)
        {
            distanceScored = distanceScore;
            firstDead = dead;
            dead = false;
            runner = null;
            runnerReady = false;
            reaperReady = false;
            bothReady = false;

            // Switches second client to master client so they become the runner
            if (PhotonNetwork.IsMasterClient && finalRound && PhotonNetwork.PlayerList.Length > 1)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]);
            }

            round++;
            if (PhotonNetwork.PlayerList.Length > 1)
            {
                player2 = PhotonNetwork.PlayerListOthers[0].NickName;
            }
        }

        // Ensures that master client has switched, then loads back into game
        if (s != PhotonNetwork.MasterClient && round == 2 && PhotonNetwork.IsMasterClient || s != PhotonNetwork.MasterClient && round == 1 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
            round = 5;
            lastRound = true;
        }

        // Ensures both clients are synced to the same round
        if(s != PhotonNetwork.MasterClient && !PhotonNetwork.IsMasterClient)
        {
            round = 5;
        }

        // Saves the second player's score and state
        if (round == 5)
        {
            secondScore = distanceScore;
            secondDead = dead;

            // When second player has finished, changes to score board screen
            if (secondDead && PhotonNetwork.IsMasterClient)
            {
                round = 10;
                StartCoroutine(ExecuteLast(10.0f));
            }
            if (!PhotonNetwork.IsMasterClient && secondDead)
            {
                round = 10;
            }
        }

        // Sends players back to lobby after a certain time
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameEnd" && round == 10 && PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                round = 11;
                StartCoroutine(ExecuteBackToLobby(5.0f));
            }
        }

        if (reaperReady && runnerReady)
        {
            bothReady = true;
        }
    }

    // Coroutines to be executed after a given time
    IEnumerator ExecuteAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (playersConnected)
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

    //We have left the Room, return back to the GameLobby
    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        PhotonNetwork.Disconnect();
        Destroy(this.gameObject);
    }
}