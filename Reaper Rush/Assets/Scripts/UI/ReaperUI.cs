using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/*
 * Author: Kips
 * 
 * Displays the reaper interface
 * 
 * Version:
 * 
 */

public class ReaperUI : MonoBehaviour
{
    private GameObject reaper;

    public Text playerName;
    public Text activeObject;

    public Button rockButton;
    public Button fireButton;

    // Start is called before the first frame update
    // Initialise fields
    void Start()
    {
        reaper = GameObject.FindGameObjectWithTag("Reaper");
        activeObject.text = "Choose your weapon!";
    }

    // Update is called once per frame
    void Update()
    {
        playerName.text = PhotonNetwork.NickName;
        if (activeObject.text == "ROCK")
        {
            rockButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            rockButton.GetComponent<Image>().color = Color.white;
        }

        if (activeObject.text == "FIRE")
        {
            fireButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            fireButton.GetComponent<Image>().color = Color.white;
        }
    }

    // Changes text to the selected ability
    public void setRock()
    {
        activeObject.text = "ROCK";
    }

    public void setFire()
    {
        activeObject.text = "FIRE";
    }
}