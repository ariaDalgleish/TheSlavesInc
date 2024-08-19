using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public InteractableButton button;
    float posY;
    Vector3 elevatorPos;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        elevatorPos = transform.localPosition;

        while (true)
        {
            if (button.holdingButton)
            {
                posY += Time.deltaTime;
            }
            else
            {
                posY -= Time.deltaTime;
            }

            posY = Mathf.Clamp(posY,0,4);
            elevatorPos.y = posY;

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = elevatorPos;
    }
    

   
}
