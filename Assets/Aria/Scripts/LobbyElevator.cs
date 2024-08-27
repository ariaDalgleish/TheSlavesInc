using System.Collections;
using UnityEngine;

public class LobbyElevator : MonoBehaviour
{
    public InteractableButton button;
    float posY;
    Vector3 elevatorPos;

    

    // Start is called before the first frame update
    IEnumerator Start()
    {
        elevatorPos = transform.localPosition; // Sets the elevator position to the current position of the elevator

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

            posY = Mathf.Clamp(posY, 0, 3.52f); 
            elevatorPos.y = posY;

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = elevatorPos; 
    }



}
