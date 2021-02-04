using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/*
 * Author: Josh, Kips
 * 
 * Deals with the winning/losing scene of the game
 * 
 * Version:
 * 
 */

public class GameOver : MonoBehaviour
{
    public GameObject manager;

    public Image background;
    public Text Message;
    public Text Top;
    public Text Bottom;

    public string player2;

    public float firstScore;
    public float secondScore;

    // Start is called before the first frame update
    // Initialise fields
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        firstScore = manager.GetComponent<GameManager>().distanceScored;
        secondScore = manager.GetComponent<GameManager>().secondScore;
        background = gameObject.GetComponentInChildren<Image>();
        player2 = manager.GetComponent<GameManager>().player2;

        // Applies game over state conditions to each client
        if (!PhotonNetwork.IsMasterClient)
        {
            if(secondScore > firstScore)
            {
                Message.text = "YOU LOSE!";
                Top.text = player2 + ": " + secondScore;
                Bottom.text = PhotonNetwork.NickName + ": " + firstScore;
            }
            else if (secondScore < firstScore)
            {
                Message.text = "YOU WIN!";
                Bottom.text = player2 + ": " + secondScore;
                Top.text = PhotonNetwork.NickName + ": " + firstScore;
                background.color = Color.green;
            }
            else if (secondScore.Equals(firstScore))
            {
                Message.text = "DRAW!";
                Bottom.text = player2 + ": " + secondScore;
                Top.text = PhotonNetwork.NickName + ": " + firstScore;
                background.color = Color.yellow;
            }
        }

        else
        {
            if (firstScore > secondScore)
            {
                Message.text = "YOU LOSE!";
                Top.text = player2 + ": " + firstScore;
                Bottom.text = PhotonNetwork.NickName + ": " + secondScore;
            }
            else if (firstScore < secondScore)
            {
                Message.text = "YOU WIN!";
                Bottom.text = player2 + ": " + firstScore;
                Top.text = PhotonNetwork.NickName + ": " + secondScore;
                background.color = Color.green;
            }
            else if (secondScore.Equals(firstScore))
            {
                Message.text = "DRAW!";
                Bottom.text = player2 + ": " + firstScore;
                Top.text = PhotonNetwork.NickName + ": " + secondScore;
                background.color = Color.yellow;
            }
        }
    }
}
