using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Alex, Kips
 * 
 * The script attached to the instance of a coin to generate them
 * 
 * Version:
 * 
 */

public class CoinGen : MonoBehaviour
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
            CreateCoins();
        }
    }

    // Generates instance of a coin
    void CreateCoins()
    {
            GameObject CoinClone = Instantiate(coin, new Vector3(Random.Range(-4,4), coin.transform.position.y + 2, z+=10), coin.transform.rotation);
            CoinClone.transform.SetParent(this.transform);
    }
}
