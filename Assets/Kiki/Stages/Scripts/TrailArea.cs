using UnityEngine;
using System.Collections;

public class TrailArea : MonoBehaviour
{
    public float slowDownFactor = 0.5f; 
    public float slowDownDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Enter with: " + other.name); // Log the name of the object entering the trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trail area"); // Confirm the player has entered the trail area
            ADPlayerMovement AriaPlayer = other.GetComponent<ADPlayerMovement>();
            if (AriaPlayer != null)
            {
                Debug.Log("Player component found. Applying slowdown."); // Confirm player component is found
                StartCoroutine(SlowDownPlayer(AriaPlayer));
            }
            else
            {
                Debug.LogWarning("Player component not found on: " + other.name); // Warn if player component is missing
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Trigger Exit with: " + other.name); // Log the name of the object exiting the trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trail area"); // Confirm the player has exited the trail area
            ADPlayerMovement AriaPlayer = other.GetComponent<ADPlayerMovement>();
            if (AriaPlayer != null)
            {
                Debug.Log("Player component found. Resetting speed."); // Confirm player component is found
                AriaPlayer.ResetSpeed(); // Reset speed when the player exits the trail
            }
        }
    }
    private IEnumerator SlowDownPlayer(ADPlayerMovement AriaPlayer)
    {
        AriaPlayer.SetSpeed(slowDownFactor); // Slow down the player
        yield return new WaitForSeconds(slowDownDuration); // Wait for the specified duration
        AriaPlayer.ResetSpeed(); // Reset the player's speed to normal
    }

       
}
