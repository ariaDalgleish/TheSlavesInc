using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public UIManager uIManager;
    public TextMeshProUGUI timerText;
    public float timeValue = 90;
    public bool timerIsRunning = false;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    //Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeValue = 0;
                timerIsRunning = false;
                uIManager.LevelFinished();

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