﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInv : MonoBehaviour
{
    public GameObject PowerUp3Original;
    public GameObject PowerUp3Container;

    int z;
    public int PowerUp;
    int x;
    void Start()
    {
        z = Random.Range(100, 500);

    }
    private void Update()
    {
        if (GameObject.Find("Power-Up #3 Gen").transform.childCount < 15)
        {
            CreatePowerUp();
        }

    }
    void CreatePowerUp()
    {
        GameObject CoinClone = Instantiate(PowerUp3Original, new Vector3(Random.Range(-4, 4), PowerUp3Original.transform.position.y - 4, z += x = Random.Range(100, 800)), PowerUp3Original.transform.rotation);
        CoinClone.transform.SetParent(this.transform);

    }

}

