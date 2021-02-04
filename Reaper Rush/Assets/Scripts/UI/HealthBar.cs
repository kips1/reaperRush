using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image health;
    private GameObject runner;


    private void Start()
    {
        health = this.GetComponentInChildren<Image>();
        runner = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        float healthPercentage = runner.GetComponent<Runner>().currentHealth / runner.GetComponent<Runner>().maxHealth;
        health.fillAmount = healthPercentage;
    }
}
