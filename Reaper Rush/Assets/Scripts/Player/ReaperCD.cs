using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReaperCD : MonoBehaviour
{
    public float timeLeft = 5.0f;
    public Text timer;
    // Start is called before the first frame update
    void Start()
    {
        timer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timer.text = (timeLeft).ToString("0");
        if (timeLeft < 0.5)
        {
            timer.enabled = false;
            timeLeft = 0;
        }

        //print("The CD " + timer);
    }
}