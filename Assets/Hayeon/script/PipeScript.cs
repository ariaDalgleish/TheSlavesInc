using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PipeScript : MonoBehaviour, IPointerClickHandler
{
    float[] rotations = { 0, 90, 180, 270 };

    public float correctRotation;
    [SerializeField]
    public bool isPlaced = false;
    private bool puzzleCompleted = false; 

    private Interacter interacter; // Reference to the Interacter script
    private PuzzleClearManager puzzleClearManager; // Reference to the Puzzle Clear Manager
    private PRMDatasending resetManager; // Tracks if the puzzle is completed

    private void Start()
    {
        interacter = FindObjectOfType<Interacter>(); // Find the Interacter script in the scene
        puzzleClearManager = FindObjectOfType<PuzzleClearManager>(); // Find the Puzzle Clear Manager in the scene
        resetManager = FindObjectOfType<PRMDatasending>();

        RandomizePipeRotation();
        CheckIfPlaced();
    }

    private void Update()
    {
        // Check for the 'E' key press to hide the puzzle clear panel
        if (puzzleCompleted && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, attempting to hide the Puzzle Clear Screen.");
            if (puzzleClearManager != null)
            {
                puzzleClearManager.HidePuzzleClearScreen();
            }
        }
    }

    public void ResetPipe()
    {
        RandomizePipeRotation();
        isPlaced = false;
        puzzleCompleted = false;
        Debug.Log("Pipe has been reset.");
    }

    private void RandomizePipeRotation()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RotatePipe();
    }

    private void RotatePipe()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        CheckIfPlaced();
    }

    public void CheckIfPlaced()
    {
        // Use modulo to handle the rotations and floating-point comparison
        float currentRotation = Mathf.Round(transform.eulerAngles.z % 360);
        float targetRotation = Mathf.Round(correctRotation % 360);

        if (Mathf.Approximately(currentRotation, targetRotation))
        {
            isPlaced = true;
        }
        else
        {
            isPlaced = false;
        }

        // Check if all pipes are correctly placed
        CheckAllPipes();
    }

    private void CheckAllPipes()
    {
        // Find all PipeScript components in the scene
        PipeScript[] allPipes = FindObjectsOfType<PipeScript>();

        // Check if all pipes are placed correctly
        foreach (PipeScript pipe in allPipes)
        {
            if (!pipe.isPlaced)
            {
                return;
            }
        }

        // If all pipes are placed correctly, hide the puzzle panel
        interacter.HidePuzzlePanel();

        // Mark the puzzle as completed and show the clear screen
        CheckPuzzleCompletion();
    }

    // Method to check if the puzzle is completed
    public void CheckPuzzleCompletion()
    {
        if (puzzleCompleted) return; // Avoid re-checking if already completed

        Debug.Log("Puzzle Completed!");
        puzzleCompleted = true;

        if (puzzleClearManager != null)
        {
            puzzleClearManager.ShowPuzzleClearScreen();
        }

        if (resetManager != null)
        {
            resetManager.StartResetCoroutine();
            Debug.Log("Reset coroutine started.");
        }
    }
}
