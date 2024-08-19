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

    private void Start()
    {
        currentDurability = maxDurability;
        UpdateDurabilityBar();
    }

    private void Update()
    {
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
