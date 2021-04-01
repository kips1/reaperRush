using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Alex, Kips
 * 
 * The script attached to the instance of a coin power up to generate them
 * 
 * Version:
 * 
 */

public class PowerUpCoin : MonoBehaviourPunCallbacks
{
    public GameObject PowerUpOriginal;
    public GameObject PowerUpContainer;

    int z;
    int x;
    public int PowerUp;

    // Start is called before the first frame update
    // Initialise fields
    void Start()
    {
        z = Random.Range(100, 500);

    }

    // Update is called once per frame
    private void Update()
    {
        // Controls the number of coin power ups to generate 
        if (GameObject.Find("Power-Up #1 Gen").transform.childCount < 2)
        {
            Generate();
        }
    }
    public void Generate()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            CreatePowerUp();
            return;
        }
    }


    // Generates instance of a coin power up
    void CreatePowerUp()
    {
        GameObject CoinClone = PhotonNetwork.Instantiate(PowerUpOriginal.name, new Vector3(Random.Range(-4, 4), PowerUpOriginal.transform.position.y - 4, z += x = Random.Range(100, 900)), PowerUpOriginal.transform.rotation);
        
        CoinClone.transform.SetParent(this.transform);
        CoinClone.transform.SetParent(this.transform);
        CoinClone.transform.SetParent(this.transform);

    }
}
