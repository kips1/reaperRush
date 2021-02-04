using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ReaperObj : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject fire;
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
        GameObject obstacleClone = PhotonNetwork.Instantiate(obstacle.name, position, obstacle.transform.rotation);
        obstacleClone.transform.SetParent(this.transform, true);
    }
    public void GenerateFire(Vector3 position)
    {
            GameObject obstacleClone = PhotonNetwork.Instantiate(fire.name, position, fire.transform.rotation);
            obstacleClone.transform.SetParent(this.transform, true);
    }
}
