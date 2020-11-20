using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RunnerUI : MonoBehaviour
{
    public Text Distance;
    private GameObject runner;
    public Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Distance.text = runner.GetComponent<Player>().distanceUnit.ToString();
        playerName.text = PhotonNetwork.NickName;
    }
}
