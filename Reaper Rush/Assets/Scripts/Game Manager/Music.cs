using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public GameObject options;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = GameObject.FindGameObjectWithTag("Options").GetComponent<Options>().backgroundmusicVol; ;   
    }
}
