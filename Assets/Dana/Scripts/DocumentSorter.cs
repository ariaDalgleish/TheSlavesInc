using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentSorter : MonoBehaviour, IPuzzle
{
    public Transform documentPile;
    public Transform wordStack;
    public Transform visualStack;

    private int initialDocumentCount;
    private bool isPaused = false;

    public GameObject puzzleClearPanel; // Reference to the puzzle clear panel
    private bool puzzleCompleted = false; // Track if the puzzle has been completed

    void Start()
    {
        initialDocumentCount = documentPile.childCount;
        puzzleClearPanel.SetActive(false); // Ensure the panel is hidden initially
    }

    void Update()
    {
        if (isPaused) return;

        if(Input.GetKeyDown(KeyCode.N))
        {
            SortDocument("WordType"); // Sort the document as a word type
        }
        // If the player presses the B key
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SortDocument("VisualType"); // Sort the document as a visual type
        }

        // Check for 'E' key press to hide the panel
        if (Input.GetKeyDown(KeyCode.E))
        {
            HidePuzzleClearScreen();
        }
    }

    public void ResetPuzzle()
    {
        foreach (Transform document in wordStack)
        {
            document.SetParent(documentPile);
        }
        foreach (Transform document in visualStack)
        {
            document.SetParent(documentPile);
        }
        Debug.Log("Document Sorter reset.");
    }


    void SortDocument(string type)
    {
        // Get the current document at the top of the pile
        if (documentPile.childCount == 0) return; // If no documents left, return

        Transform currentDocument = documentPile.GetChild(0); // Get the top document

      
        if (currentDocument.CompareTag(type))
        {
            currentDocument.SetParent(type == "WordType" ? wordStack : visualStack);
            currentDocument.localPosition = Vector3.zero;
            currentDocument.SetAsLastSibling(); // Move the document to the end of the stack

            // Check if all documents have been moved
            if (documentPile.childCount == 0)
            {
                ShowPuzzleClearScreen();
                Debug.Log("Game Cleared!");
            }
        }
        else
        {
            Debug.Log("Wrong key pressed!");
        }
    }

    public void CheckPuzzleCompletion()
    {
        if (puzzleCompleted) return; // Avoid re-checking if already complete

        if (isPaused)
        {
            Debug.Log("Puzzle Completed!");
            puzzleCompleted = true;
            ShowPuzzleClearScreen();
        }
    }
    private void ShowPuzzleClearScreen()
    {
        if (puzzleClearPanel != null)
        {
            puzzleClearPanel.SetActive(true);
        }
    }

    public void HidePuzzleClearScreen()
    {
        if (puzzleClearPanel != null)
        {
            puzzleClearPanel.SetActive(false);
        }
    }

}
