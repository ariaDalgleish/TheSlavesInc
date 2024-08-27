using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
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
                posX += Time.deltaTime;
            }
            else
            {
                posX -= Time.deltaTime;
            }

            posX = Mathf.Clamp(posX, -8.81f, -5.18f);
            elevatorPos.x = posX;

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = elevatorPos;
    }
    

   
}
