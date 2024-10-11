using UnityEngine;
using System.Collections;
using Photon.Pun;

public class EnemyMovement : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject trailPrefab; // Prefab with TrailRenderer and BoxCollider
    public float moveSpeed = 2f; // Enemy movement speed
    public float changeDirectionTime = 2f; // Time to change direction
    public float trailSpawnInterval = 1f; // Interval at which trails are spawned
    public float trailDuration = 3f; // Duration the trail stays
    private float changeDirectionTimer;
    private Vector3 movementDirection;
    private Rigidbody rb; // Add a Rigidbody variable

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component

        if (photonView.IsMine)
        {
            rb.useGravity = true; // Enable gravity for the enemy
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Lock rotation on X and Z axes

            changeDirectionTimer = changeDirectionTime;
            movementDirection = RandomDirection();
            StartCoroutine(SpawnTrail());
        }
        else
        {
            // Disable unnecessary components for non-local enemies
            GetComponent<EnemyTrailManager>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            rb.useGravity = false; // No need for gravity on non-local enemies
            rb.isKinematic = true; // Make non-local enemies kinematic
            enabled = false;
        }
    }

    private void FixedUpdate() // Changed Update to FixedUpdate
    {
        if (!photonView.IsMine) return;

        changeDirectionTimer -= Time.fixedDeltaTime;
        if (changeDirectionTimer <= 0)
        {
            movementDirection = RandomDirection();
            changeDirectionTimer = changeDirectionTime;
            photonView.RPC("UpdateMovement", RpcTarget.All, movementDirection);
        }

        // Rotate the enemy towards the movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Use Rigidbody to move the enemy
        rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime); // Move using Rigidbody
    }

    private Vector3 RandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        return new Vector3(randomX, 0, randomZ).normalized;
    }

    private IEnumerator SpawnTrail()
    {
        while (true)
        {
            // Instantiate the trail prefab
            GameObject trail = Instantiate(trailPrefab, transform.position, Quaternion.identity);
            trail.transform.parent = transform; // Make it a child of the enemy

            // Wait for the trail duration
            yield return new WaitForSeconds(trailDuration);

            // Destroy the trail GameObject
            Destroy(trail);

            // Wait for the next spawn
            yield return new WaitForSeconds(trailSpawnInterval);
        }
    }

    [PunRPC]
    private void UpdateMovement(Vector3 newDirection)
    {
        movementDirection = newDirection;
    }
}
