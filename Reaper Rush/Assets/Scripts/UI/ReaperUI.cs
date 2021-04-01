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
    private GameObject manager;

    public Text playerName;
    public Text activeObject;
    public Text countdown;

    public Button rockButton;
    public Button fireButton;

    // Start is called before the first frame update
    // Initialise fields
    void Start()
    {
        reaper = GameObject.FindGameObjectWithTag("Reaper");
        activeObject.text = "Choose your weapon!";
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    void Update()
    {
        playerName.text = PhotonNetwork.NickName;
        if (activeObject.text == "SHADES")
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

        countdown.text = manager.GetComponent<GameManager>().countdown.ToString("0");
        if (countdown.text == "0")
        {
            countdown.enabled = false;
        }
    }

    // Changes text to the selected ability
    public void setRock()
    {
        activeObject.text = "SHADES";
    }

    public void setFire()
    {
        activeObject.text = "FIRE";
    }
}