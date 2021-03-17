using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Alex, Kips
 * 
 * The script attached to the instance of a coin to generate them
 * 
 * Version:
 * 
 */

public class CoinGen : MonoBehaviourPunCallbacks
{
    public GameObject coin;

    int z;

    //  Start is called before the first frame update
    // Initialise fields
    void Start()
    {
        z = Random.Range(10, 25);
    }

    // Update is called once per frame
    private void Update()
    {
        // Controls the number of coins to generate
        if (GameObject.Find("CoinGenerator").transform.childCount < 9)
        {
            Generate();
        }
    }

    public void Generate()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            CreateCoins();
            return;
        }
    }

    // Generates instance of a coin
    void CreateCoins()
    {
            GameObject CoinClone = PhotonNetwork.Instantiate(coin.name, new Vector3(Random.Range(-4,4), coin.transform.position.y + 2, z+=10), coin.transform.rotation);
            //CoinClone.transform.SetParent(this.transform);
    }
}
