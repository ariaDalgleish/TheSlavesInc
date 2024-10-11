using Photon.Realtime;
using UnityEngine;

public class DynamicGlow : MonoBehaviour
{
    public Transform player; 
    public float detectionRange = 5f; 
    public float maxEmissionIntensity = 5f; 
    public float minEmissionIntensity = 0f; 

    private Material material;

    void Start()
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

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer component missing from the GameObject.");
        }
    }

    void Update()
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

        if (material == null) return;

        // Calculate the distance from the player to the object
        float distance = Vector3.Distance(player.position, transform.position);
        //Debug.Log($"Distance to player: {distance}");

        // Calculate emission intensity based on distance
        float intensity = Mathf.Lerp(minEmissionIntensity, maxEmissionIntensity, 1 - (distance / detectionRange));
        intensity = Mathf.Clamp(intensity, minEmissionIntensity, maxEmissionIntensity);

        // Set the emission intensity of the material
        Color emissionColor = Color.white * intensity;
        material.SetColor("_EmissionColor", emissionColor);
        //Debug.Log($"Emission Intensity: {intensity}");
    }
}
