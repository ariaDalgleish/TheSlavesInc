using UnityEngine;
using System.Collections;

public class EnemyTrailManager : MonoBehaviour
{
    public GameObject trailArea; // Prefab with the TrailRenderer and BoxCollider
    public float trailDuration = 3f; // Duration for which the trail remains

    private void Start()
    {
        StartCoroutine(SpawnTrail());
    }

    private IEnumerator SpawnTrail()
    {
        while (true)
        {
            // Instantiate the trail prefab
            GameObject trail = Instantiate(trailArea, transform.position, Quaternion.identity);
            trail.transform.parent = transform; 

            // Wait for the duration of the trail
            yield return new WaitForSeconds(trailDuration);

            // Destroy the trail GameObject
            Destroy(trail);
        }
    }
}
