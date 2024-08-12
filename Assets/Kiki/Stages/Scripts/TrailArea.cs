using UnityEngine;
using System.Collections;

public class TrailArea : MonoBehaviour
{
    public float slowDownFactor = 0.5f; // Factor by which the player's speed will be reduced
    public float slowDownDuration = 5f; // Duration for which the player is slowed down

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trail area");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                StartCoroutine(SlowDownPlayer(player));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trail area");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.ResetSpeed(); // Reset speed when the player exits the trail
            }
        }
    }

    private IEnumerator SlowDownPlayer(Player player)
    {
        player.SetSpeed(slowDownFactor); // Slow down the player
        yield return new WaitForSeconds(slowDownDuration); // Wait for the specified duration
        player.ResetSpeed(); // Reset the player's speed to normal
    }
}