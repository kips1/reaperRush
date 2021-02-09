using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Author: Josh
 * 
 * Displays the current health of the runner
 * 
 * Version:
 * 
 */

public class HealthBar : MonoBehaviour
{
    private GameObject runner;

    public Image health;

    // Start is called before the first frame update
    // Initialise fields
    private void Start()
    {
        health = this.GetComponentInChildren<Image>();
        runner = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    // Checks the runner's current health and updates accordingly
    private void Update()
    {
        float healthPercentage = runner.GetComponent<Runner>().currentHealth / runner.GetComponent<Runner>().maxHealth;
        health.fillAmount = healthPercentage;
    }
}