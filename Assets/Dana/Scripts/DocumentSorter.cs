using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentSorter : MonoBehaviour, IPuzzle
{
    AudioManager audioManager;
    public Transform documentPile;
    public Transform wordStack;
    public Transform visualStack;

    public GameObject puzzleClearPanel; // Reference to the puzzle clear panel

    public DurabilitySystem durabilitySystem; // Reference to the DurabilitySystem

    //private bool puzzleCompleted = false; // Track if the puzzle has been completed

    private PuzzleResetManager resetManager; // Reference to the reset manager

    // List to store original positions and parents
    private List<Vector3> originalPositions = new List<Vector3>();
    private List<Transform> originalParents = new List<Transform>();
    private List<Transform> documentReferences = new List<Transform>();

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        resetManager = FindObjectOfType<PuzzleResetManager>(); // Find the reset manager in the scene
        Debug.Log("Puzzle Reset Manager found" + (resetManager != null));

        puzzleClearPanel.SetActive(false); // Ensure the panel is hidden initially
        Debug.Log("Initial document count: " + documentPile.childCount);

        // Store the original positions and parents of all documents in the pile
        foreach (Transform document in documentPile)
        {
            originalPositions.Add(document.localPosition);
            originalParents.Add(document.parent);
            documentReferences.Add(document);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SortDocument("WordType"); // Sort the document as a word type
            audioManager.PlaySFX(audioManager.paper);

        }
        // If the player presses the B key
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SortDocument("VisualType"); // Sort the document as a visual type
            audioManager.PlaySFX(audioManager.paper);

        }

        // Check for 'E' key press to hide the panel
        if (Input.GetKeyDown(KeyCode.E))
        {
            HidePuzzleClearScreen();
        }
    }

    public void ResetPuzzle()
    {
        Debug.Log("Resetting puzzle...");

        for (int i = 0; i < documentReferences.Count; i++)
        {
            Transform document = documentReferences[i];
            document.SetParent(originalParents[i]);
            document.localPosition = originalPositions[i];
            Debug.Log("Document reset: " + document.name);
        }

        //puzzleCompleted = false;
        Debug.Log("Document Sorter reset.");
    }


    void SortDocument(string type)
    {
        if (documentPile.childCount == 0)
        {
            Debug.Log("No more documents to sort");
            return;
        }

        Transform currentDocument = documentPile.GetChild(0); // Get the top document
        Debug.Log("Current document tag: " + currentDocument.tag);

        if (currentDocument.CompareTag(type))
        {
            currentDocument.SetParent(type == "WordType" ? wordStack : visualStack);
            currentDocument.localPosition = Vector3.zero;
            currentDocument.SetAsLastSibling(); // Move the document to the end of the stack

            Debug.Log("Document moved to " + (type == "WordType" ? "Word Stack" : "VisualStack"));

            // Check if all documents have been moved
            if (documentPile.childCount == 0)
            {
                ShowPuzzleClearScreen();
                audioManager.PlaySFX(audioManager.taskComplete);

                durabilitySystem.IncreaseDurability(); // Increase Durability when puzzle is completed' above the 'Debug.Log("Puzzle cleared!");
                //puzzleCompleted = true;
                Debug.Log("Game Cleared!");
                resetManager.StartResetCoroutine(this); // Start the reset coroutine via the reset manager
            }
        }
        else
        {
            Debug.Log("Wrong key pressed! Expected tag: " + type + ", but got: " + currentDocument.tag);
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
