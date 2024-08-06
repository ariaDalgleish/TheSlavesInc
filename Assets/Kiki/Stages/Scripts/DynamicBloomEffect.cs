using UnityEngine;

public class DynamicGlow : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float detectionRange = 5f; // Range within which the object should glow
    public float maxEmissionIntensity = 5f; // Maximum emission intensity
    public float minEmissionIntensity = 0f; // Minimum emission intensity

    private Material material;

    void Start()
    {
        // Get the material from the Renderer component
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
        if (material == null) return;

        // Calculate the distance from the player to the object
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        Debug.Log($"Distance to player: {distance}");

        // Calculate emission intensity based on distance
        float intensity = Mathf.Lerp(minEmissionIntensity, maxEmissionIntensity, 1 - (distance / detectionRange));
        intensity = Mathf.Clamp(intensity, minEmissionIntensity, maxEmissionIntensity);

        // Set the emission intensity of the material
        Color emissionColor = Color.white * intensity;
        material.SetColor("_EmissionColor", emissionColor);
        Debug.Log($"Emission Intensity: {intensity}");
    }
}
