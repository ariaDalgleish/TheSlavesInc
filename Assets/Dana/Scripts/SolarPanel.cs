using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarPanel : MonoBehaviour
{
    public Slider gaugeBar; // Reference to the UI Slider that acts as the gauge bar
    public float fillSpeed = 0.2f; // Speed at which the gauge fills up
    public float decaySpeed = 0.1f; // Speed at which the gauge decreases

    private bool isFilling = false; // Whether the gauge is currently filling


    void Update()
    {
        // Check if both A and B buttons are pressed
        if (Input.GetKeyDown(KeyCode.V) && Input.GetKeyDown(KeyCode.B))
        {
            isFilling = true; // Start filling the gauge
        }
        else
        {
            isFilling = false; // Stop filling the gauge
        }

        // Fill or decrease the gauge based on the button press state
        if (isFilling)
        {
            FillGauge();
        }
        else
        {
            DecreaseGauge();
        }
    }

    void FillGauge()
    {
        // Increase the gauge value while A and B buttons are pressed
        if (gaugeBar.value < 1)
        {
            gaugeBar.value += fillSpeed * Time.deltaTime; // Increase the gauge value over time
            Debug.Log("Filling Gauge: " + gaugeBar.value); // Debug message for filling
        }
        else
        {
            Debug.Log("Gauge is fully filled! Task complete!"); // Log message when the gauge is fully filled
        }
    }

    void DecreaseGauge()
    {
        // Decrease the gauge value when A and B buttons are not pressed
        if (gaugeBar.value > 0)
        {
            gaugeBar.value -= decaySpeed * Time.deltaTime; // Decrease the gauge value over time
            Debug.Log("Decreasing Gauge: " + gaugeBar.value); // Debug message for decreasing
        }
    }

}
