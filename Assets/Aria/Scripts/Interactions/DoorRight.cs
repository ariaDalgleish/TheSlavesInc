using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRight : MonoBehaviour
{
    public InteractableButton button;
    float posZ;
    Vector3 elevatorPos;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        elevatorPos = transform.localPosition;

        while (true)
        {
            if (button.holdingButton == false)
            {
                posZ += Time.deltaTime;
            }
            else
            {
                posZ -= Time.deltaTime;
            }

            posZ = Mathf.Clamp(posZ, -14.56f, -12.11f);
            elevatorPos.z = posZ;

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = elevatorPos;
    }
}
