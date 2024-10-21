using UnityEngine;

public class PuzzleClearManager : MonoBehaviour
{
    public GameObject puzzleClearPanel; // Reference to the Puzzle Clear Panel
    
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
            HidePuzzleClearScreen();
        }
    }

    // Show the puzzle clear panel
    public void ShowPuzzleClearScreen()
    {
        if (puzzleClearPanel != null)
        {
            audioManager.PlaySFX(audioManager.taskComplete);
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
        }
    }
}