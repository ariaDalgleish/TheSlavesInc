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

    private bool isFull = false; //track if the gauge has reached 100

    void Update()
    {
        if (Input.GetKey(KeyCode.V) && (Input.GetKey(KeyCode.B) && !isFull))
        {
            Gauge += (MaxGauge / 3f) * Time.deltaTime;
            if (Gauge >= MaxGauge)
            {
                Gauge = MaxGauge;
                isFull = true;
                Debug.Log("completed!");
            }
        }
        else if (!isFull) //when v is not pressed
        {
            Gauge -= DecreaseSpeed * Time.deltaTime;
            if(Gauge < 0) Gauge = 0;
        }
        gaugeBar.fillAmount = Gauge / MaxGauge;
    }

}
