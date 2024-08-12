using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerrr : MonoBehaviour
{
    public float normalSpeed = 9f; // The default speed of the player
    private float currentSpeed;    // The current speed of the player
    private float rotateSpeed = 10f;

 

    void Start()
    {
        currentSpeed = normalSpeed; // Initialize the current speed
        
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


    }

    // Method to set the player's speed
    public void SetSpeed(float factor)
    {
        currentSpeed = normalSpeed * factor;
        Debug.Log("Speed Set to: " + currentSpeed + " (Factor: " + factor + ")");

    }

    // Method to reset the player's speed to normal
    public void ResetSpeed()
    {
        currentSpeed = normalSpeed;
        Debug.Log("Speed Reset to Normal: " + currentSpeed);

    }

    // Update the UI text with the current speed
 
}
