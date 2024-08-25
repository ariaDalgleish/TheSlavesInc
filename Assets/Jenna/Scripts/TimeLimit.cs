using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    private LevelLoader levelLoader; // Reference to LevelLoader

    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>(); // Find LevelLoader in the scene
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            remainingTime = 0;
            timerText.color = Color.red;

            // Call LoadScoreScene after the timer reaches 0
            levelLoader.LoadScoreScene();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
