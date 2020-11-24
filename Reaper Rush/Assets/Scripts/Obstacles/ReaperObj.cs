using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ReaperObj : MonoBehaviour
{
    public GameObject obstacle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Generate(Vector3 position)
    {
        if (PhotonNetwork.IsMasterClient == false) {
        GameObject obstacleClone = PhotonNetwork.Instantiate(obstacle.name, position, obstacle.transform.rotation);
        //obstacleClone.GetComponent<ObstacleScript>().myNum = value;
        obstacleClone.transform.SetParent(this.transform, true);
        }
    }
}
