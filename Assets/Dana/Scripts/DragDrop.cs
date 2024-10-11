using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject objectToDrag;
    public GameObject holder; // Corresponding Holder object for this drag object
    public float Dropdistance = 50f;
    public bool isLocked;

    private Vector2 objectInitPos;
    public GameObject puzzleClearPanel; // Reference to the puzzle clear panel
    //private bool puzzleCompleted = false; // Track if the puzzle has been completed

    void Start()
    {
        objectInitPos = objectToDrag.transform.position;

        // Hide all initially
        holder.SetActive(false);
        puzzleClearPanel.SetActive(false);

        // Randomly activate 4-5 holders
        if (RandomHolderActivation())
        {
            holder.SetActive(true);
        }
    }

    void Update()
    {
        // Check for 'E' key press to hide the panel
        if (Input.GetKeyDown(KeyCode.E))
        {
            HidePuzzleClearScreen();
            RandomHolderActivation();
        }
    }

    public void DragObject()
    {
        if (!isLocked)
        {
            objectToDrag.transform.position = Input.mousePosition;
        }
    }

    public void DropObject()
    {
        float Distance = Vector3.Distance(objectToDrag.transform.position, holder.transform.position);
        if (Distance < Dropdistance)
        {
            isLocked = true;
            objectToDrag.transform.position = holder.transform.position; // Snap to holder position

            // Check if all objects are locked after this one is dropped correctly
            CheckGameClear();
        }
        else
        {
            objectToDrag.transform.position = objectInitPos; // Return to initial position if not dropped correctly
        }
    }

    void CheckGameClear()
    {
        DragDrop[] allDragDrops = FindObjectsOfType<DragDrop>();
        foreach (DragDrop dragDrop in allDragDrops)
        {
            if (!dragDrop.isLocked)
            {
                return; // Exit if any object is not locked, meaning the game is not yet cleared
            }
        }

        //puzzleCompleted = true;
        ShowPuzzleClearScreen();
        Debug.Log("Game Cleared!"); // This will only run when all objects are locked
        // Trigger additional game clear actions here, if needed
    }

    // Method to randomly determine if a holder should be activated
    bool RandomHolderActivation()
    {
        // Randomly activate between 4 and 5 holders
        int randomValue = Random.Range(0, 8); // Random value between 0 and 7 (8 possibilities)
        return randomValue < 5; // True for 4-5 times out of 8
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