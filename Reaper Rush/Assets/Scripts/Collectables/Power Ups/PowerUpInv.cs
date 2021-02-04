using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Alex, Kips
 * 
 * The script attached to the instance of a invulnerability power up
 * 
 * Version:
 * 
 */

public class PowerUpInv : MonoBehaviour
{
    public GameObject PowerUp3Original;
    public GameObject PowerUp3Container;

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
        // Controls the number of invulnerability power ups to generate
        if (GameObject.Find("Power-Up #3 Gen").transform.childCount < 2)
        {
            CreatePowerUp();
        }
    }

    // Generates instance of an invulnerability power up
    void CreatePowerUp()
    {
        GameObject CoinClone = Instantiate(PowerUp3Original, new Vector3(Random.Range(-4, 4), PowerUp3Original.transform.position.y - 4, z += x = Random.Range(100, 800)), PowerUp3Original.transform.rotation);
        CoinClone.transform.SetParent(this.transform);
    }
}

