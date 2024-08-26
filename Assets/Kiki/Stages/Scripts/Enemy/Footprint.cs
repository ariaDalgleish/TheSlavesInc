using UnityEngine;
using System.Collections;

public class Footprint : MonoBehaviour
{
    public float slowdownDuration = 5f;

    private void Start()
    {
        Destroy(gameObject, slowdownDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ADPlayerMovement playerMovement = other.GetComponent<ADPlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.SetSpeed(playerMovement.speed * 0.5f); // Apply slowdown
                StartCoroutine(ResetSpeedAfterDelay(playerMovement, slowdownDuration));
            }
        }
    }

    private IEnumerator ResetSpeedAfterDelay(ADPlayerMovement playerMovement, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.ResetSpeed();
    }
}
