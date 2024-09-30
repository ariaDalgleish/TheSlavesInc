using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilitySystem : MonoBehaviour
{
    public Image durabilityBar;
    public float maxDurability = 100;
    public float currentDurability;
    public float decreaseSpeed = 5f; // Durability decrease rate per second
    public float increaseAmount = 25f; // Durability increase amount when a puzzle is solved
    public bool isDecreasing = false; // Flag to control durability decrease

    private void Start()
    {
        currentDurability = maxDurability;
        UpdateDurabilityBar();
    }

    private void Update()
    {
        // Only decrease durability if the isDecreasing flag is true
        if (!isDecreasing) return;

        if (currentDurability > 0)
        {
            currentDurability -= decreaseSpeed * Time.deltaTime;
            UpdateDurabilityBar();
        }

        // If Durability reaches 0, trigger any necessary game over conditions or effects
        if (currentDurability <= 0)
        {
            currentDurability = 0;
            // Trigger Game Over or other related actions
        }
    }

    public void StartDecreasingDurability()
    {
        isDecreasing = true; // Set the flag to start decreasing durability
        Debug.Log("Durability decrease started.");
    }

    public void StopDecreasingDurability()
    {
        isDecreasing = false; // Set the flag to stop decreasing durability
        Debug.Log("Durability decrease stopped.");
    }

    public void IncreaseDurability()
    {
        currentDurability += increaseAmount;
        if (currentDurability > maxDurability)
        {
            currentDurability = maxDurability;
        }
        UpdateDurabilityBar();
    }

    private void UpdateDurabilityBar()
    {
        durabilityBar.fillAmount = currentDurability / maxDurability;
    }
}


