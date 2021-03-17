using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


/*
 * Author: Alex, Kips
 * 
 * The script attached to the instance of a health power up to generate them
 * 
 * Version:
 * 
 */

public class PowerUpHealth : MonoBehaviourPunCallbacks
{
    public GameObject PowerUp2Original;
    public GameObject PowerUp2Container;

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
        // Controls the number of health power ups to generate
        if (GameObject.Find("Power-Up #2 Gen").transform.childCount < 2)
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

    // Generates instance of a health power up
    void CreatePowerUp()
    {
        GameObject CoinClone = PhotonNetwork.Instantiate(PowerUp2Original.name, new Vector3(Random.Range(-4, 4), PowerUp2Original.transform.position.y - 4, z += x = Random.Range(200, 600)), PowerUp2Original.transform.rotation);
        CoinClone.transform.SetParent(this.transform);
    }
}
