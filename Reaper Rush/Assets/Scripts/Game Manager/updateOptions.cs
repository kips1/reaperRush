using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateOptions : MonoBehaviour
{
    private GameObject options;
    // Start is called before the first frame update
    void Start()
    {
        options = GameObject.FindGameObjectWithTag("Options");
    }

    // Update is called once per frame
    void Update()
    {
        options.GetComponent<Options>().masterVol = GameObject.Find("MusicSlider").GetComponent<Slider>().value;
        options.GetComponent<Options>().sfxVol = GameObject.Find("SFXSlider").GetComponent<Slider>().value;
        options.GetComponent<Options>().backgroundmusicVol = GameObject.Find("BackgroundSlider").GetComponent<Slider>().value;
    }
}
