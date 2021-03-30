using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Author: Alex
 * 
 * Displays the amount of coins collected by runner 
 * 
 * Version:
 * 
 */

public class CoinAddScript : MonoBehaviour
{
    Text text;

    public static int coinAmount = 20;

    // Start is called before the first frame update
    // Initialise fields
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = coinAmount.ToString();
    }
}