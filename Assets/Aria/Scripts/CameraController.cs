using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed;
    public Vector3 offset;
    public float followDistance;
    public Quaternion rotation;

    public float teleDistanceThreshold = 100f;

    private void Update()
    {
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
