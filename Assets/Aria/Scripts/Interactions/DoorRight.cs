using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRight : MonoBehaviour
{
    public InteractableButton button;
    float posX;
    Vector3 elevatorPos;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        elevatorPos = transform.localPosition;

        while (true)
        {
            if (button.holdingButton == false)
            {
                posX -= Time.deltaTime;
            }
            else
            {
                posX += Time.deltaTime;
            }

            posX = Mathf.Clamp(posX, -9.67f, -7.48f);
            elevatorPos.x = posX;

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = elevatorPos;
    }
}
