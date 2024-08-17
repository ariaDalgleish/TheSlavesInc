using UnityEngine;

public class PuzzleClearManager : MonoBehaviour
{
    public GameObject puzzleClearPanel; // Reference to the Puzzle Clear Panel

    private void Start()
    {
        // Ensure the panel is hidden initially
        if (puzzleClearPanel != null)
        {
            puzzleClearPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // Check for the 'E' key press to hide the puzzle clear panel
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, attempting to hide the Puzzle Clear Screen.");
            HidePuzzleClearScreen();
        }
    }

    // Show the puzzle clear panel
    public void ShowPuzzleClearScreen()
    {
        if (puzzleClearPanel != null)
        {
            puzzleClearPanel.SetActive(true);
            Debug.Log("Puzzle Clear Screen shown.");
        }
    }

    // Hide the puzzle clear panel
    public void HidePuzzleClearScreen()
    {
        if (puzzleClearPanel != null)
        {
            puzzleClearPanel.SetActive(false);
            Debug.Log("Puzzle Clear Screen hidden.");
        }
    }
}