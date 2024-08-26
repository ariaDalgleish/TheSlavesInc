using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public GameObject trailPrefab; // Prefab with TrailRenderer and BoxCollider
    public float moveSpeed = 2f; // Enemy movement speed
    public float changeDirectionTime = 2f; // Time to change direction
    public float trailSpawnInterval = 1f; // Interval at which trails are spawned
    public float trailDuration = 3f; // Duration the trail stays

    private float changeDirectionTimer;
    private Vector3 movementDirection;

    private void Start()
    {
        changeDirectionTimer = changeDirectionTime;
        movementDirection = RandomDirection();
        StartCoroutine(SpawnTrail());
    }

    private void Update()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            movementDirection = RandomDirection();
            changeDirectionTimer = changeDirectionTime;
        }

        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
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
            trail.transform.parent = transform; // Optional: Make it a child of the enemy

            // Wait for the trail duration
            yield return new WaitForSeconds(trailDuration);

            // Destroy the trail GameObject
            Destroy(trail);

            // Wait for the next spawn
            yield return new WaitForSeconds(trailSpawnInterval);
        }
    }
}