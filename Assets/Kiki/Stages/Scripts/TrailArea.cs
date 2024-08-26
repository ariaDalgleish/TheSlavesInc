using UnityEngine;

public class TrailArea : MonoBehaviour
{
    public float slowdownFactor = 0.5f; // Factor by which to slow down the player
    public float slowdownDuration = 2f; // Duration of the slowdown effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ADPlayerMovement player = other.GetComponent<ADPlayerMovement>();
            if (player != null)
            {
                player.ApplySlowdown(slowdownFactor, slowdownDuration);
            }
        }
    }
}