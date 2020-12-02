using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp1Gen : MonoBehaviour
{
    public GameObject PowerUpOriginal;
    public GameObject PowerUpContainer;
    
    int z;
    public int PowerUp;
    int x;
    void Start()
    {
        z = Random.Range(100, 500);
        
       
        CreatePowerUp(PowerUp);

    }
    void CreatePowerUp(int coinsNum)
    {
        for (int i = 0; i < coinsNum; i++)
        {
            GameObject CoinClone = Instantiate(PowerUpOriginal, new Vector3(Random.Range(-4, 4), PowerUpOriginal.transform.position.y - 4, z+= x = Random.Range(10, 50)), PowerUpOriginal.transform.rotation);
           
        }
    }

}
