﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 150f, 0f) * Time.deltaTime);        
    }
}