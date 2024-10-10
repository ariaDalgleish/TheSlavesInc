using UnityEngine;
using System.Collections;

public class EnemyTrailManager : MonoBehaviour
{
    public GameObject trailAreaPrefab; // Prefab with TrailRenderer and BoxCollider
    public float trailSpawnInterval = 1f; // Interval at which trails are spawned
    public Color startColor = Color.red; // Start color of the trail
    public Color endColor = new Color(1, 0, 0, 0); // End color of the trail (transparent)
    public Material trailMaterial; // Material to apply to the trail

    private void Start()
    {
        StartCoroutine(SpawnTrails());
    }

    private IEnumerator SpawnTrails()
    {
        while (true)
        {
            // Instantiate the trail prefab at the enemy's position
            GameObject trail = Instantiate(trailAreaPrefab, transform.position, Quaternion.identity);
            trail.transform.position = transform.position; // Ensure it’s positioned correctly
            trail.transform.parent = transform; // Make it a child of the enemy

            // Get the TrailRenderer component from the trail prefab
            TrailRenderer trailRenderer = trail.GetComponent<TrailRenderer>();
            if (trailRenderer != null)
            {
                // Set the colors of the trail
                trailRenderer.startColor = startColor; // Set the start color
                trailRenderer.endColor = endColor; // Set the end color (transparent)

                // Check for assigned material and ensure it's set
                if (trailMaterial != null)
                {
                    // Create a new instance of the material to avoid shared references
                    trailRenderer.material = new Material(trailMaterial);
                }

                // Ensure the TrailRenderer's time is set properly
                trailRenderer.time = 2f; // Adjust this value as needed
            }

            // Wait for the duration of the trail (this is how long the trail will stay)
            yield return new WaitForSeconds(2f); // Adjust this to the desired duration

            // Destroy the trail GameObject after its duration
            Destroy(trail);

            // Wait for the next spawn
            yield return new WaitForSeconds(trailSpawnInterval);
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the BoxCollider of the trail prefab in the editor
        if (trailAreaPrefab != null)
        {
            BoxCollider boxCollider = trailAreaPrefab.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Gizmos.color = Color.red; // Draw the BoxCollider as a wireframe cube
                Gizmos.DrawWireCube(transform.position, boxCollider.size);
            }
        }
    }
}
