using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplayMeteor : MonoBehaviour
{
    public Text timerText;  // Reference to UI Text component
    public MeteorScript meteorController;  // Reference to the meteor

    void Update()
    {
        timerText.text = "Time Left: " + Mathf.Clamp(meteorController.timeLimit, 0, 5).ToString("F2");
    }
}