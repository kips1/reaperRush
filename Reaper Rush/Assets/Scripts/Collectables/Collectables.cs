using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public GameObject runner;
    // Start is called before the first frame update
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (runner != null)
        {
            if (this.transform.position.z < runner.transform.position.z - 40)
            {
                if (gameObject != null)
                {
                    Destroy(gameObject);
                }
            }
        }
    
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Rock"))
        {
            Destroy(gameObject);
        }
    }
}

