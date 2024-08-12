using UnityEngine;

public class DynamicGlow : MonoBehaviour
{
    public Transform playerTransform; 
    public float detectionRange = 5f; 
    public float maxEmissionIntensity = 5f; 
    public float minEmissionIntensity = 0f; 

    private Material material;

    void Start()
    {
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
