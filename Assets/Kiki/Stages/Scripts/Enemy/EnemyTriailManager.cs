using UnityEngine;
using System.Collections;

public class EnemyTrailManager : MonoBehaviour
{
    public GameObject trailAreaPrefab; // Prefab with TrailRenderer and BoxCollider
    public float trailSpawnInterval = 1f; // Interval at which trails are spawned

    private void Start()
    {
        StartCoroutine(SpawnTrails());
    }

    private IEnumerator SpawnTrails()
    {
        while (true)
        {
            // Instantiate the trail prefab
            GameObject trail = Instantiate(trailAreaPrefab, transform.position, Quaternion.identity);
            trail.transform.position = transform.position; // Ensure it’s positioned correctly
            trail.transform.parent = transform; // Optional: Make it a child of the enemy

            // Wait for the trail duration
            yield return new WaitForSeconds(trail.GetComponent<TrailRenderer>().time);

            // Destroy the trail GameObject
            Destroy(trail);

            // Wait for the next spawn
            yield return new WaitForSeconds(trailSpawnInterval);
        }
    }

    private void OnDrawGizmos()
    {
        if (trailAreaPrefab != null)
        {
            // Get the BoxCollider from the trail prefab
            BoxCollider boxCollider = trailAreaPrefab.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Gizmos.color = Color.red;
                // Draw the BoxCollider as a wireframe cube
                Gizmos.DrawWireCube(transform.position, boxCollider.size);
            }
        }
    }
}