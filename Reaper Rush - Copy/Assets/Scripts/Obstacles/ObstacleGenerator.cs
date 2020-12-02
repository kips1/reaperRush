using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObstacleGenerator : MonoBehaviourPunCallbacks
{
    public GameObject obstacle;
    Vector3 position;
    bool next;
    public float[] posX;
    public float[] posZ;
    int value = 1;
    public int lastPosition = 1;

    void Update()
    {
        //StartCoroutine(WaitSys());
        if (GameObject.Find("Controller").transform.childCount < 15) {
            Generate();
        }
    }

  /*  IEnumerator WaitSys()
    {
        //yield return new WaitForSeconds(2f);
        next = true;
        Generate();
        yield return new WaitForSeconds(100f);
    }*/


    public void Generate()
    {
        //if (!next)
          //  return;
        if (PhotonNetwork.IsMasterClient == true)
        {
            int i = Random.Range(0, 3);
            position.x = posX[i];
            position.z += posZ[i];
            GameObject obstacleClone = PhotonNetwork.Instantiate(obstacle.name, position, obstacle.transform.rotation);
            //obstacleClone.GetComponent<ObstacleScript>().myNum = value;
            obstacleClone.transform.SetParent(this.transform);
            value += 1;
            next = false;
            return;
        }
    }


    /*public void ReaperGenerate(Vector3 reaperPosition)
    {
        //if (!next)
        //  return;
        //if (PhotonNetwork.IsMasterClient == true)
        //{
            //int i = Random.Range(0, 3);
            //position.x = posX[i];
            //position.z += posZ[i];
            GameObject obstacleClone2 = PhotonNetwork.Instantiate(obstacle.name, reaperPosition, obstacle.transform.rotation);
            //obstacleClone.GetComponent<ObstacleScript>().myNum = value;
            obstacleClone2.transform.SetParent(this.transform);
            value += 1;
            next = false;
            return;
        //}
    }
    */
    public void Message(int i)
    {
        if (lastPosition == i)
        {
            lastPosition += 1;
            Debug.Log("Found");
        } 

        else
        {
            Debug.Log("Not found");
        }
    }
}
