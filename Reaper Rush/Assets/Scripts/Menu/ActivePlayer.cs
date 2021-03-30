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

            if (gameObject.name != runner)
            {
                gameObject.SetActive(false);
            }
            else if(gameObject.name == runner)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
