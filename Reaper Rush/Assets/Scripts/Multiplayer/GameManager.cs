using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviourPunCallbacks
{
    public int round;
    public float distanceScore;
    public float distanceScored;
    private int coinsCollected;
    public bool finalRound;
    private GameObject runner;
    private GameObject reaper;
    private GameObject rmController;
    private static GameManager _instance;
    public Photon.Realtime.Player s;
    public bool lastRound;
    public float secondScore;
    public bool firstDead;
    public bool secondDead;
    public bool dead;
    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
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
        finalRound = false;
        round = 0;
        DontDestroyOnLoad(gameObject);
        s = PhotonNetwork.MasterClient;
        lastRound = false;

    }



    // Update is called once per frame
    void Update()
    {
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
                if (runner.GetComponent<Player>().hasLost && round == 0 && PhotonNetwork.IsMasterClient)
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
            if (secondDead)
            {
                round = 10;
                StartCoroutine(ExecuteLast(5.0f));
            }
            //Debug.Log(secondScore + "thise is first" + distanceScored);
        }

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameEnd" && round == 10)
        {
            StartCoroutine(ExecuteBackToLobby(14.0f));
            round = 11;
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
}