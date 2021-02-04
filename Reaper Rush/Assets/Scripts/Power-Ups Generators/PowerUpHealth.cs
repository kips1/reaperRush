using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : MonoBehaviour
{
    public GameObject PowerUp2Original;
    public GameObject PowerUp2Container;

    int z;
    public int PowerUp;
    int x;
    void Start()
    {
        z = Random.Range(100, 500);

    }
    private void Update()
    {
        if (GameObject.Find("Power-Up #2 Gen").transform.childCount < 15)
        {
            CreatePowerUp();
        }

    }
    void CreatePowerUp()
    {
        GameObject CoinClone = Instantiate(PowerUp2Original, new Vector3(Random.Range(-4, 4), PowerUp2Original.transform.position.y - 4, z += x = Random.Range(200, 600)), PowerUp2Original.transform.rotation);
        CoinClone.transform.SetParent(this.transform);
    }

}
