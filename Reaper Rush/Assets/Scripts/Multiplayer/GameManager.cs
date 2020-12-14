using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public int round;
    public float distanceScore;
    private int coinsCollected;
    public bool finalRound;

    private GameObject runner;
    private GameObject reaper;
    private GameObject rmController;
    private static GameManager _instance;
    public Photon.Realtime.Player s;

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
            //reaper = GameObject.FindGameObjectWithTag("Reaper");
            finalRound = true;

            if (PhotonNetwork.IsMasterClient)
            {
                if (runner.GetComponent<Player>().hasLost && round == 0)
                {
                    round++;
                    finalRound = true;
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
            if (PhotonNetwork.IsMasterClient && finalRound && PhotonNetwork.PlayerList.Length > 1)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]);
            }
            //PhotonNetwork.LoadLevel("Game");


            round++;
            if (s != PhotonNetwork.MasterClient && round == 2)
            {
                PhotonNetwork.LoadLevel("Game");
                round++;
            }
            else if (s != PhotonNetwork.MasterClient && round == 1 && PhotonNetwork.PlayerList.Length > 1)
            {
                PhotonNetwork.LoadLevel("Game");
                round = 3;
            }


        }




        if (round < 2)
        {
            //distanceScore = runner.GetComponent<Player>().distanceUnit;
        }


    }

    IEnumerator ExecuteAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        PhotonNetwork.LoadLevel("RoleSwap");
    }
}
