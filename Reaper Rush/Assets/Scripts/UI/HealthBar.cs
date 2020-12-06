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
        float healthPercentage = runner.GetComponent<Player>().currentHealth / runner.GetComponent<Player>().maxHealth;
        healthPercentage = Mathf.Clamp(healthPercentage, 0, 1);
        health.fillAmount = healthPercentage;
    }
}
