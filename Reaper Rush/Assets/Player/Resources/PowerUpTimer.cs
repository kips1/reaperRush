using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PowerUpTimer : Runner
{
    public float timeLeft = 3.0f;
    public Text timer;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
