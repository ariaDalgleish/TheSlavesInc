using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorInput : MonoBehaviour
{
    public MeteorScript meteorController;  // Reference to the meteor
    public Image buttonImage;  // UI Button Image (for visual feedback)
    public Color pressedColor = Color.gray;  // Color when button is pressed
    public Vector3 pressedScale = new Vector3(0.9f, 0.9f, 1f);  // Scale when button is pressed
    private Color originalColor;
    private Vector3 originalScale;

    void Start()
    {
        // Store original button properties
        originalColor = buttonImage.color;
        originalScale = buttonImage.transform.localScale;
    }

    void Update()
    {
        // Detect when 'K' is pressed
        if (Input.GetKeyDown(KeyCode.K))
        {
            meteorController.ReduceHealth();  // Reduce meteor health
            ButtonPressed();  // Show button pressed effect
        }

        // Reset button visual after pressing
        if (Input.GetKeyUp(KeyCode.K))
        {
            ResetButton();  // Restore original button appearance
        }
    }

    void ButtonPressed()
    {
        buttonImage.color = pressedColor;  // Change color to simulate press
        buttonImage.transform.localScale = pressedScale;  // Scale down button
    }

    void ResetButton()
    {
        buttonImage.color = originalColor;  // Reset color
        buttonImage.transform.localScale = originalScale;  // Reset scale
    }
}