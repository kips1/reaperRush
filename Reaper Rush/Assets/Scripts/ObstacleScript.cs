using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public int myNum;
    private ObstacleGenerator generate;
    private Renderer render;

    private void Start()
    {
        generate = GetComponentInParent<ObstacleGenerator>();
        render = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            render.material.color = Color.green;
            generate.Message(myNum);
            Debug.Log("Works!");

        }
    }
    
}
