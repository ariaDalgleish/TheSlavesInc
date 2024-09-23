using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public float speed = 3f;              // Speed at which the meteor moves towards the spaceship
    public int meteorHealth = 20;         // Health of the meteor (Just set for 20 now, can change later)
    public float timeLimit = 5f;          // Time limit before the meteor hits the spaceship
    public Vector2 targetPosition;       // Position of the spaceship
    public bool isDestroyed = false;     // Flag to check if the meteor is already destroyed

    public PuzzleClearManager puzzleClearManager;  // Reference to the PuzzleClearManager script



    void Update()
    {
        // If the meteor is destroyed, stop further actions
        if (isDestroyed) return;

        // Move the meteor towards the spaceship each frame
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Timer logic: reduce the time limit as time passes
        timeLimit -= Time.deltaTime;

        // If the time runs out (timeLimit reaches 0), the meteor hits the spaceship
        if (timeLimit <= 0)
        {
            DamageSpaceship();  // Call the function to damage the spaceship
        }

        // Check if the meteor is close enough to the spaceship to register a hit
        if (Vector2.Distance(transform.position, targetPosition) < 0.5f)
        {
            DamageSpaceship();  // Call the function to damage the spaceship if it's too close
        }
    }

    // Method that reduces the health of the meteor when the player presses 'K'
    public void ReduceHealth()
    {
        meteorHealth--;  // Decrease meteor's health by 1
        if (meteorHealth <= 0)  // If the meteor's health is zero or below
        {
            isDestroyed = true;  // Set the meteor as destroyed
            Debug.Log("Meteor destroyed!");  // Log the successful destruction of the meteor

            if (puzzleClearManager != null)
            {
                puzzleClearManager.ShowPuzzleClearScreen();
            }

            Destroy(gameObject);  // Destroy the meteor GameObject
        }
    }

    // Method that damages the spaceship when the meteor reaches it or the time runs out
    void DamageSpaceship()
    {
        Debug.Log("Meteor hit the spaceship!");
        Destroy(gameObject);  // Destroy the meteor when it hits the spaceship
        //Will add more later when spaceship health bar sorts out
    }

    public void ResetPuzzle()
    {
        timeLimit = 5f;
        meteorHealth = 20;
        isDestroyed = false;

        transform.position = new Vector2(0, 0);
        Debug.Log("Meteor puzzle reset!");
    }
}
