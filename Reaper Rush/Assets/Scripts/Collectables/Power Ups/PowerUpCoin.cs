using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Alex, Kips
 * 
 * The script attached to the instance of a coin power up to generate them
 * 
 * Version:
 * 
 */

public class PowerUpCoin : MonoBehaviour
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
            CreatePowerUp();
        }
    }

    // Generates instance of a coin power up
    void CreatePowerUp()
    {
        GameObject CoinClone = Instantiate(PowerUpOriginal, new Vector3(Random.Range(-4, 4), PowerUpOriginal.transform.position.y - 4, z += x = Random.Range(100, 900)), PowerUpOriginal.transform.rotation);
        CoinClone.transform.SetParent(this.transform);
    }
}
