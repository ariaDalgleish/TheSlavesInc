using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed;
    public Vector3 offset;
    public float followDistance;
    public Quaternion rotation;

    public float teleDistanceThreshold = 100f;

    private void Start()
    {
        if (player == null)
        {
            // Find the player in the scene (assuming the player has a tag "Player")
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    private void Update()
    {
        if (player == null)
        {
            // Try to find the player again if it wasn't found initially
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            return; // Skip the rest of the update until the player is found
        }

        if (Vector3.Distance(transform.position, player.position) > teleDistanceThreshold)
        {
            transform.position = player.position + offset + -transform.forward * followDistance;
        }
        else
        {
            Vector3 pos = Vector3.Lerp(transform.position, player.position + offset + -transform.forward * followDistance, moveSpeed * Time.deltaTime);
            transform.position = pos;
        }

        transform.rotation = rotation;
    }
}
