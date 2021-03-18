using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCooldown : MonoBehaviour
{
    public float timeLeft1 = 5.0f;
    public Text timer1;

    // Start is called before the first frame update
    void Start()
    {
        timer1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft1 -= Time.deltaTime;
        timer1.text = (timeLeft1).ToString("0");
        if (timeLeft1 < 0.5)
        {
            timer1.enabled = false;
            timeLeft1 = 0;
        }
    }
}
