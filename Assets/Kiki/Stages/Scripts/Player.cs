using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float normalSpeed = 9f; // The default speed of the player
    private float currentSpeed;    // The current speed of the player
    private float rotateSpeed = 10f;

    public Text speedText; // Reference to the UI Text component

    void Start()
    {
        currentSpeed = normalSpeed; // Initialize the current speed
        UpdateSpeedText(); // Initialize speed display
        Debug.Log("Player script started. Current speed: " + currentSpeed);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = direction.normalized;

        // Move the player
        transform.position += direction * Time.deltaTime * currentSpeed;

        if (direction != Vector3.zero)
        {
            // Rotate the player
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
        }

        // Update speed display
        UpdateSpeedText();
    }

    // Method to set the player's speed
    public void SetSpeed(float factor)
    {
        currentSpeed = normalSpeed * factor;
        Debug.Log("Speed Set to: " + currentSpeed + " (Factor: " + factor + ")");
        UpdateSpeedText();
    }

    // Method to reset the player's speed to normal
    public void ResetSpeed()
    {
        currentSpeed = normalSpeed;
        Debug.Log("Speed Reset to Normal: " + currentSpeed);
        UpdateSpeedText();
    }

    // Update the UI text with the current speed
    void UpdateSpeedText()
    {
        if (speedText != null)
        {
            speedText.text = $"Speed: {currentSpeed:F2}"; // Format speed to 2 decimal places
        }
    }
}
