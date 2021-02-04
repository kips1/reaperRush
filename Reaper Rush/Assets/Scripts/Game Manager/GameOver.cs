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
            EndScreen(firstScore, secondScore);
        }

        else
        {
            EndScreen(secondScore, firstScore);
        }
    }

    private void EndScreen(float myScore, float otherScore)
    {
        if (otherScore > myScore)
        {
            Message.text = "YOU LOSE!";
            Top.text = player2 + ": " + otherScore;
            Bottom.text = PhotonNetwork.NickName + ": " + myScore;
        }
        else if (otherScore < myScore)
        {
            Message.text = "YOU WIN!";
            Bottom.text = player2 + ": " + otherScore;
            Top.text = PhotonNetwork.NickName + ": " + myScore;
            background.color = Color.green;
        }
        else if (otherScore.Equals(myScore))
        {
            Message.text = "DRAW!";
            Bottom.text = player2 + ": " + otherScore;
            Top.text = PhotonNetwork.NickName + ": " + myScore;
            background.color = Color.yellow;
        }
    }
}
