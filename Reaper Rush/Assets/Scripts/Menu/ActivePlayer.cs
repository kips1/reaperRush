using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    public string runner;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager"))
            {

            runner = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().currentRunner;
        }

        foreach (Transform runners in this.gameObject.GetComponentsInChildren<Transform>())
        {
            if(runners.gameObject.name == runner)
            {
                runners.gameObject.SetActive(true);
            }
            else if (runners.gameObject.name != runner)
            {
                runners.gameObject.SetActive(false);
            }
            
        }
    }
}
