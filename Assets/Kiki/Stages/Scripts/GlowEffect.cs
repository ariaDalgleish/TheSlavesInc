using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    [Header("References")]
    public Transform player; // Assign the player's Transform in the Inspector
    public Material normalMaterial; // Assign the normal material in the Inspector
    public Material glowMaterial; // Assign the glowing material in the Inspector
    public float glowDistance = 20f; // Set the distance at which the glow effect activates

    private Renderer _renderer;
    private bool isGlowing = false;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError("Renderer component missing from this GameObject");
            enabled = false;
        }
        else
        {
            _renderer.material = normalMaterial; // Ensure the initial material is set to normal
        }
    }

    void Update()
    {
        // Calculate the distance between the player and this object
        float distance = Vector3.Distance(player.position, transform.position);
        bool shouldGlow = distance < glowDistance;

        // Log detailed debugging information
        Debug.Log($"Player Position: {player.position}");
        Debug.Log($"Object Position: {transform.position}");
        Debug.Log($"Distance: {distance}");
        Debug.Log($"GlowDistance: {glowDistance}");
        Debug.Log($"Should Glow: {shouldGlow}, Is Glowing: {isGlowing}");

        // Handle the glow effect based on the distance
        if (shouldGlow && !isGlowing)
        {
            EnableGlow();
        }
        else if (!shouldGlow && isGlowing)
        {
            DisableGlow();
        }
    }

    void EnableGlow()
    {
        if (_renderer != null && glowMaterial != null)
        {
            _renderer.material = glowMaterial;
            isGlowing = true;
            Debug.Log("Glow enabled");
        }
    }

    void DisableGlow()
    {
        if (_renderer != null)
        {
            _renderer.material = normalMaterial;
            isGlowing = false;
            Debug.Log("Glow disabled");
        }
    }
}
