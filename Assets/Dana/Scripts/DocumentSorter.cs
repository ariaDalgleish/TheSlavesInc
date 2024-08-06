using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentSorter : MonoBehaviour
{
    public Transform documentPile;
    public Transform wordStack;
    public Transform visualStack;

    private int initialDocumentCount;
    private bool isPaused = false;

    void Start()
    {
        initialDocumentCount = documentPile.childCount;
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
    }

    void SortDocument(string type)
    {
        // Get the current document at the top of the pile
        if (documentPile.childCount == 0) return; // If no documents left, return

        Transform currentDocument = documentPile.GetChild(0); // Get the top document

        // Check if the current document's tag matches the expected type
        if (currentDocument.CompareTag(type))
        {
            // Move the document to the appropriate stack
            currentDocument.SetParent(type == "WordType" ? wordStack : visualStack);
            currentDocument.localPosition = Vector3.zero; // Reset the document's position
            currentDocument.SetAsLastSibling(); // Move the document to the end of the stack

            // Check if all documents have been moved
            if (documentPile.childCount == 0)
            {
                Debug.Log("Game Cleared!");
            }
        }
        else
        {
            // Log a message if the wrong key is pressed and start the pause coroutine
            Debug.Log("Wrong key pressed!");
            StartCoroutine(PauseGame(1f)); // Pause for 1 second
        }
    }

    IEnumerator PauseGame(float duration)
    {
        isPaused = true; // Set the game to paused
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        isPaused = false; // Unpause the game
    }
}
