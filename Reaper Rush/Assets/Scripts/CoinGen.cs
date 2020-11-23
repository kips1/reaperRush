using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGen : MonoBehaviour
{
    public GameObject coinOriginal;
    public GameObject CoinContainer;
    // Random rnd = new Random();
    // int x = rnd.Next(-5, 5);
    // int z = rnd.Next(3, 15);
    int z = Random.Range(10, 25);
    public int Coins;

   void Start()
    {
        //GameObject CoinClone = Instantiate(coinOriginal);
        CreateCoins(Coins);
    }
    void CreateCoins(int coinsNum)
    {
        for (int i = 0; i < coinsNum; i++)
        {
            GameObject CoinClone = Instantiate(coinOriginal, new Vector3(Random.Range(-4,4), coinOriginal.transform.position.y + 2, z+=10), coinOriginal.transform.rotation);
            //CoinClone.transform.parent = CoinContainer.transform;
            //CoinClone.name = "CoinClone" + (i + 1);
        }
    }
    
}
