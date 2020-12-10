using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int round;
    private int distanceScore;
    private int coinsCollected;
    public bool finalRound;

    private GameObject runner;
    private GameObject reaper;
    private GameObject rmController;
    private static GameManager _instance;

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

            if (runner.GetComponent<Player>().hasLost && round == 0)
            {
                round++;
                finalRound = true;
                if (PhotonNetwork.IsMasterClient)
                {
                    runner.GetComponent<Player>().Reset();
                    PhotonNetwork.Destroy(runner);
                } else if (!PhotonNetwork.IsMasterClient)
                {
                    reaper.GetComponent<Reaper>().Reset();
                    PhotonNetwork.Destroy(reaper);
                }

                
                    Debug.Log(finalRound);
                PhotonNetwork.LoadLevel("RoleSwap");
                this.gameObject.tag = "mainManager";
            }

            
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RoleSwap" && round == 1)
        {
            PhotonNetwork.LoadLevel("Game");
            round++;
        }
    }
}
