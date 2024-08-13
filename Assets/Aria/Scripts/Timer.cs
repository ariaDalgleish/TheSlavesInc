using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    
    // Reference to the UIManager script
    public UIManager uIManager;
    // Reference to the TextMeshProUGUI component
    public TextMeshProUGUI timerText;
    public float timeValue = 90;
    public bool timerIsRunning = false;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeValue > 0) 
            {
                timeValue -= Time.deltaTime; // Decrease the time value by the time that has passed since the last frame
            }
            else
            {
                Debug.Log("Time has run out!");
                timeValue = 0;
                timerIsRunning = false;
                uIManager.LevelFinished(); // Call the LevelFinished method from the UIManager script

            }

            DisplayTime(timeValue); 
        }

       
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void ResetTimer()
    {
        timeValue = 90;
        timerIsRunning = true;
    }
}