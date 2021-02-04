using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGen : MonoBehaviour
{
    
    public GameObject coin;

    // public GameObject CoinContainer;
    // Random rnd = new Random();
    // int x = rnd.Next(-5, 5);
    // int z = rnd.Next(3, 15);
    int z; 
    //public int Coins;

   void Start()
    {
        z = Random.Range(10, 25);
        //GameObject CoinClone = Instantiate(coinOriginal);
        
    }
    private void Update()
    {
        if (GameObject.Find("CoinGenerator").transform.childCount < 5)
        {
            CreateCoins();
        }

    }
    void CreateCoins()
    {

            GameObject CoinClone = Instantiate(coin, new Vector3(Random.Range(-4,4), coin.transform.position.y + 2, z+=10), coin.transform.rotation);
            CoinClone.transform.SetParent(this.transform);
            //CoinClone.transform.parent = CoinContainer.transform;
            //CoinClone.name = "CoinClone" + (i + 1);
    }
    
}
