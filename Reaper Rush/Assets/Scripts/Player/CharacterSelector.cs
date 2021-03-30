using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviourPun
{
    public int currentCharacterIndex;
    public GameObject[] characters;
    public string runnerName;



    void Update()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("setModel", RpcTarget.AllBuffered);
        }



    }

    [PunRPC]
    void SetModel()
    {
        foreach (GameObject character in characters)
            character.SetActive(false);

        characters[currentCharacterIndex].SetActive(true);
        runnerName = characters[currentCharacterIndex].name;
    }

}
