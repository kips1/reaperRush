using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeAbilityPosition : MonoBehaviour
{
    float x;
    float y;
    float z;
    Vector3 position;
    void Start()
    {
        x = Random.Range(-1, 1);
        y = Random.Range(-1, 1);
        z = Random.Range(-1, 1);
        position = new Vector3(x + x, y + y, z + z);
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
