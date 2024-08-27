using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DragDropPuzzleManager : MonoBehaviour
{
    public GameObject[] objectHolder; //left side
    public GameObject[] toDrag;  //right side
    public int minObjectsToShow = 4;
    public int maxObjectsToShow = 5;

    public GameObject puzzlePanel;
    public GameObject puzzleClearPanel; // Assign the PuzzleClearPanel in the Inspector
    private bool puzzleCompleted = false; // Track if the puzzle has been completed


    private void Start()
    {
        SetRandomLeftObjects();
        puzzleClearPanel.SetActive(false); // Ensure the clear panel is hidden initially
    }

    private void Update()
    {
        // Allow the player to open/close the puzzle panel with 'E' key
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (puzzlePanel.activeSelf)
            {
                puzzlePanel.SetActive(false);
            }
            else if (!puzzleCompleted)
            {
                puzzlePanel.SetActive(true);
            }
        }
    }

    public void SetRandomLeftObjects()
    {
        //hide all left objects first
        foreach (GameObject obj in objectHolder)
        {
            obj.SetActive(false);
        }


        //randomly select 4-5 objects to show
        int objectsToShow = Random.Range(minObjectsToShow, maxObjectsToShow + 1);
        for (int i = 0; i < objectsToShow; i++)
        {
            int randomIndex = Random.Range(0, objectHolder.Length);
            while (objectHolder[randomIndex].activeSelf)
            {
                randomIndex = Random.Range(0, objectHolder.Length);
            }
            objectHolder[randomIndex].SetActive(true);
        }


    }

    public void ResetPuzzle()
    {
        foreach (GameObject obj in toDrag)
        {
            obj.GetComponent<DragDropPuzzle>().UnlockObject();
        }
        puzzleCompleted = false;
        Debug.Log("Puzzle reset.");
        SetRandomLeftObjects();
    }

    public bool AreAllObjectsLocked()
    {
        foreach (GameObject obj in toDrag)
        {
            if (!obj.GetComponent<DragDropPuzzle>().IsLocked())
            {
                return false;
            }
        }
        return true;
    }

    public void CheckPuzzleCompletion()
    {
        if (AreAllObjectsLocked())
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
}
