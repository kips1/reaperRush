using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Collectables : MonoBehaviourPun
{
    public GameObject runner;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
            if (this.transform.position.z < runner.transform.position.z - 40)
            {
                if (PhotonNetwork.IsMasterClient == true && gameObject.GetComponent<PhotonView>().IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handles coin powerup
        if (other.gameObject.tag == "Player" && this.gameObject.layer == 10)
        {
            for (int i = 30; i < 54; i += 3)
            {
                PhotonNetwork.Instantiate(coin.name, new Vector3(Random.Range(-4, 4), 2, runner.GetComponent<Runner>().distanceUnit + i), Quaternion.identity);
            }
            PhotonNetwork.Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Player" && this.gameObject.layer == 8)
        {
            if (PhotonNetwork.IsMasterClient == true && gameObject.GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}

