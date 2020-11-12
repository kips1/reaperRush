using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstacle;
    Vector3 position;
    private bool next;
    public float[] posX;
    public float[] posZ;
    private int value = 1;
    public int lastPosition = 1;
    
    void FixedUpdate()
    {
        StartCoroutine(WaitSys());
    }

    IEnumerator WaitSys()
    {

        yield return new WaitForSeconds(500f);

        next = true;
        Generate();
    }

    void Generate()
    {
        if (!next)
            return;
        int i = Random.Range(0, 3);
        position.x = posX[i];
        position.z += posZ[i];
        GameObject obstacleClone = Instantiate(obstacle, position, obstacle.transform.rotation);
        obstacleClone.GetComponent<ObstacleScript>().myNum = value;
        obstacleClone.transform.SetParent(this.transform);
        value += 1;
        next = false;
        return;
    }

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
