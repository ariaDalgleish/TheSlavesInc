using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarPanel : MonoBehaviour, IPuzzle
{
    public Image gaugeBar;
    public float Gauge = 0f;
    public float MaxGauge = 100f;
    public float Charging = 25f;
    public float DecreaseSpeed = 60f;

    public GameObject puzzleClearPanel; // Reference to the puzzle clear panel

    private bool isFull = false; //track if the gauge has reached 100
    private bool puzzleCompleted = false; // Track if the puzzle has been completed

    private void Start()
    {
        puzzleClearPanel.SetActive(false); // Ensure the panel is hidden initially
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

    public void ResetPuzzle()
    {
        Gauge = 0f;
        isFull= false;
        gaugeBar.fillAmount = 0f;
        Debug.Log("SolarPanel puzzle reset.");
    }

}
