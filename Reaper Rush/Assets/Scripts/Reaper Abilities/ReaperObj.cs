using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Kips
 * 
 * Deals with the generation of the reaper abilities
 * 
 * Version:
 * 
 */

public class ReaperObj : MonoBehaviour
{
    // Object is created for each reaper ability
    public GameObject obstacle;
    public GameObject fire;

    // Creates an instance of the reaper obstacle and places it below the reaper instance
    public void Generate(Vector3 position)
    {
        GameObject obstacleClone = PhotonNetwork.Instantiate(obstacle.name, position, obstacle.transform.rotation);
        obstacleClone.transform.SetParent(this.transform, true);
    }

    // Creates an instance of the fire and places it in front of the reaper instance
    public void GenerateFire(Vector3 position)
    {
        GameObject obstacleClone = PhotonNetwork.Instantiate(fire.name, position, fire.transform.rotation);
        obstacleClone.transform.SetParent(this.transform, true);
    }
}
