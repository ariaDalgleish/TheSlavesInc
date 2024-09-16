using System.Collections;
using UnityEngine;

public class PRMSolar : MonoBehaviour
{
    public float resetDelay = 10f; // Time to wait before resetting the puzzle
    private SolarPanel solarPanelScript; // Reference to the SolarPanel puzzle script

    private void Start()
    {
        // Find the SolarPanel script attached to the canvas
        solarPanelScript = GetComponentInChildren<SolarPanel>();
    }

    // This function is called after the puzzle is completed
    public void StartResetCoroutine()
    {
        if (solarPanelScript != null && solarPanelScript.puzzleCompleted)
        {
            Debug.Log("Starting puzzle reset coroutine...");
            StartCoroutine(ResetPuzzleWithDelay());
        }
        else
        {
            Debug.LogError("SolarPanel script is missing or puzzle not completed!");
        }
    }

    private IEnumerator ResetPuzzleWithDelay()
    {
        Debug.Log("waiting for reset delay...");
        yield return new WaitForSeconds(resetDelay); // Wait for the reset delay

        if (solarPanelScript != null)
        {
            Debug.Log("Resetting puzzle...");
            ResetPuzzle(); // Reset the puzzle after the delay
        }
        else
        {
            Debug.LogError("SolarPanel script referece is null during reset!");
        }
    }

    // Reset the puzzle state to its initial condition
    private void ResetPuzzle()
    {
        if (solarPanelScript != null)
        {
            solarPanelScript.Gauge = 0f; // Reset gauge to 0
            solarPanelScript.isFull = false; // Mark puzzle as not completed
            solarPanelScript.puzzleCompleted = false; // Mark puzzle as not completed
            solarPanelScript.gaugeBar.fillAmount = 0f; // Update the gauge bar UI
            solarPanelScript.HidePuzzleClearScreen(); // Hide the puzzle clear panel

            Debug.Log("Puzzle has been reset.");
        }
        else
        {
            Debug.LogError("SolarPanelScript is null while trying to reset!");
        }
    }
}

