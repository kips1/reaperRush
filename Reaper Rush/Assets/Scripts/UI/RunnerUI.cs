using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RunnerUI : MonoBehaviour
{
    public Text Distance;
    public Text roundOver;
    private GameObject runner;
    public Text playerName;
    public Text Score;
    private GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    void Update()
    {
        Distance.text = runner.GetComponent<Player>().distanceUnit.ToString();
        playerName.text = PhotonNetwork.NickName;
        if (manager.GetComponent<GameManager>().distanceScore != 0)
        {
            Score.text = manager.GetComponent<GameManager>().distanceScore.ToString();
        }
    }
}
