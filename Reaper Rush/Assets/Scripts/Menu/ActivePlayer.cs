using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    public string runner;
    // Start is called before the first frame update
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().currentRunner;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name != runner)
        {
            gameObject.SetActive(false);
        }
    }
}
