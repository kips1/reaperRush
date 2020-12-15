﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class GameOver : MonoBehaviour
{
    public Text Message;
    public Text Top;
    public Text Bottom;
    public GameObject manager;
    public float firstScore;
    public float secondScore;
    public Image background;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        firstScore = manager.GetComponent<GameManager>().distanceScored;
        secondScore = manager.GetComponent<GameManager>().secondScore;
        background = gameObject.GetComponentInChildren<Image>();
        if (PhotonNetwork.IsMasterClient == false)
        {
            if(secondScore > firstScore)
            {
                Message.text = "YOU LOSE!";
                Top.text = PhotonNetwork.PlayerListOthers[0].NickName + ": " + secondScore;
                Bottom.text = PhotonNetwork.NickName + ": " + firstScore;
            }
            else if (secondScore < firstScore)
            {
                Message.text = "YOU WIN!";
                Bottom.text = PhotonNetwork.PlayerListOthers[0].NickName + ": " + secondScore;
                Top.text = PhotonNetwork.NickName + ": " + firstScore;
                background.color = Color.green;
            }
            else if (secondScore.Equals(firstScore))
            {
                Message.text = "DRAW!";
                Bottom.text = PhotonNetwork.PlayerListOthers[0].NickName + ": " + secondScore;
                Top.text = PhotonNetwork.NickName + ": " + firstScore;
                background.color = Color.yellow;
            }
        }
        else
        {
            if (firstScore > secondScore)
            {
                Message.text = "YOU LOSE!";
                Top.text = PhotonNetwork.PlayerListOthers[0].NickName + ": " + firstScore;
                Bottom.text = PhotonNetwork.NickName + ": " + secondScore;
            }
            else if (firstScore < secondScore)
            {
                Message.text = "YOU WIN!";
                Bottom.text = PhotonNetwork.PlayerListOthers[0].NickName + ": " + firstScore;
                Top.text = PhotonNetwork.NickName + ": " + secondScore;
                background.color = Color.green;
            }
            else if (secondScore.Equals(firstScore))
            {
                Message.text = "DRAW!";
                Bottom.text = PhotonNetwork.PlayerListOthers[0].NickName + ": " + firstScore;
                Top.text = PhotonNetwork.NickName + ": " + secondScore;
                background.color = Color.yellow;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}