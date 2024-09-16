using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarPanel : MonoBehaviour
{
    public Image gaugeBar;
    public float Gauge = 0f;
    public float MaxGauge = 100f;
    public float Charging = 25f;
    public float DecreaseSpeed = 60f;

    public GameObject puzzleClearPanel; // Reference to the puzzle clear panel
    public PRMSolar resetManager; // Reference to the PRMSolar script for resetting

    public bool isFull = false; //track if the gauge has reached 100
    public bool puzzleCompleted = false; // Track if the puzzle has been completed

    private void Start()
    {
        puzzleClearPanel.SetActive(false); // Ensure the panel is hidden initially

        // If resetManager is not assigned in the inspector, try to find it
        if (resetManager == null)
        {
            resetManager = GetComponentInParent<PRMSolar>();
        }
        if (resetManager == null)
        {
            Debug.LogError("Reset manager not found!");
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.G) && (Input.GetKey(KeyCode.B) && !isFull))
        {
            Gauge += (MaxGauge / 3f) * Time.deltaTime;
            if (Gauge >= MaxGauge)
            {
                Gauge = MaxGauge;
                isFull = true;
                Debug.Log("Puzzle Completed!");
                puzzleCompleted = true;
                ShowPuzzleClearScreen();

                // Start the reset coroutine in PRMSolar after puzzle completion
                if (resetManager != null)
                {
                    Debug.Log("Starting reset coroutine from SolarPanel script...");
                    resetManager.StartResetCoroutine();
                }
                else
                {
                    Debug.LogError("Reset manager not found!");
                }
            }
        }
        else if (!isFull) //when v is not pressed
        {
            Gauge -= DecreaseSpeed * Time.deltaTime;
            if (Gauge < 0) Gauge = 0;
        }
        gaugeBar.fillAmount = Gauge / MaxGauge;

        // Check for 'E' key press to hide the panel
        if (Input.GetKeyDown(KeyCode.E))
        {
            HidePuzzleClearScreen();
        }
    }
    public void CheckPuzzleCompletion()
    {
        if (puzzleCompleted) return; // Avoid re-checking if already complete

        if (isFull)
        {
            Debug.Log("Puzzle Completed!");
            puzzleCompleted = true;
            ShowPuzzleClearScreen();

            // Start the reset coroutine in PRMSolar after puzzle completion
            if (resetManager == null)
            {
                resetManager.StartResetCoroutine();
            }
        }
    }
    private void ShowPuzzleClearScreen()
    {
        if (puzzleClearPanel != null)
        {
            puzzleClearPanel.SetActive(true);
        }
    }

    public void HidePuzzleClearScreen()
    {
        if (puzzleClearPanel != null)
        {
            puzzleClearPanel.SetActive(false);
        }
    }
}
