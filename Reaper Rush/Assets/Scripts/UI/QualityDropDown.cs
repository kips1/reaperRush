using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualityDropDown : MonoBehaviour
{
    Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(QualitySettings.GetQualityLevel());


    }

    public void changeQuality()
    {
        if (dropdown.value == 0)
        {
            QualitySettings.SetQualityLevel(0);
        }
        if (dropdown.value == 1)
        {
            QualitySettings.SetQualityLevel(1);
        }
        if (dropdown.value == 2)
        {
            QualitySettings.SetQualityLevel(2);

        }
        if (dropdown.value == 3)
        {
            QualitySettings.SetQualityLevel(3);

        }
        if (dropdown.value == 4)
        {
            QualitySettings.SetQualityLevel(4);

        }
        if (dropdown.value == 5)
        {
            QualitySettings.SetQualityLevel(5);

        }
        if (dropdown.value == 6)
        {
            QualitySettings.SetQualityLevel(6);

        }
    }
}
