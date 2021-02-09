using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/*
 * Author: Kips
 * 
 * Displays the interface for the runner
 * 
 * Version:
 * 
 */

public class RunnerUI : MonoBehaviour
{
    private GameObject runner;
    private GameObject manager;

    public Text Distance;
    public Text roundOver;
    public Text playerName;
    public Text Score;

    // Start is called before the first frame update
    // Initialise fields
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    // Updates the distance and adds the name accordingly to the player's input
    void Update()
    {
        Distance.text = runner.GetComponent<Runner>().distanceUnit.ToString();
        playerName.text = PhotonNetwork.NickName;
        if (manager.GetComponent<GameManager>().distanceScore > 0)
        {
            Score.text = manager.GetComponent<GameManager>().distanceScored.ToString();
        }
    }
}