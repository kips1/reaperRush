using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadeCooldown : MonoBehaviour
{
    public float timeLeft2 = 5.0f;
    public Text timer2;

    // Start is called before the first frame update
    void Start()
    {
        timer2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft2 -= Time.deltaTime;
        timer2.text = (timeLeft2).ToString("0");
        if (timeLeft2 < 0.5)
        {
            timer2.enabled = false;
            timeLeft2 = 0;
        }
    }
}
