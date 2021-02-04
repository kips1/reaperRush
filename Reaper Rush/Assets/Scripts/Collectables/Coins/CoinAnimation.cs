using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Alex
 * 
 * The script attached to the instance of a coin for the animation
 * 
 * Version:
 */

public class CoinAnimation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Rotates the coin
        transform.Rotate(new Vector3(0f, 150f, 0f) * Time.deltaTime);        
    }
}
