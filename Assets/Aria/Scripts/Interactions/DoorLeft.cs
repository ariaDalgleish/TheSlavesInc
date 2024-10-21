using System.Collections;
using UnityEngine;

public class DoorLeft : MonoBehaviour
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
                posZ -= Time.deltaTime;
            }
            else
            {
                posZ += Time.deltaTime;
            }

            posZ = Mathf.Clamp(posZ, -11.24f, -8.79f);
            elevatorPos.z = posZ;

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = elevatorPos;
    }
    

   
}
