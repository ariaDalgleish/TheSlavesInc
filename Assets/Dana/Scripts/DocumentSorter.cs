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
                Debug.Log("Game Cleared!");
            }
        }
        else
        {
            Debug.Log("Wrong key pressed!");
        }
    }

}
