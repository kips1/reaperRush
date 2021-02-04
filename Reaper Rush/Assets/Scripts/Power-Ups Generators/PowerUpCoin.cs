using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCoin : MonoBehaviour
{
    public GameObject PowerUpOriginal;
    public GameObject PowerUpContainer;
    
    int z;
    public int PowerUp;
    int x;
    void Start()
    {
        z = Random.Range(100, 500);
        
       
        //CreatePowerUp(PowerUp);

    }
    private void Update()
    {
        if (GameObject.Find("Power-Up #1 Gen").transform.childCount < 15)
        {
            CreatePowerUp();
        }

    }
    void CreatePowerUp()
    {
            GameObject CoinClone = Instantiate(PowerUpOriginal, new Vector3(Random.Range(-4, 4), PowerUpOriginal.transform.position.y - 4, z+= x = Random.Range(100, 900)), PowerUpOriginal.transform.rotation);
            CoinClone.transform.SetParent(this.transform);
    }

}
