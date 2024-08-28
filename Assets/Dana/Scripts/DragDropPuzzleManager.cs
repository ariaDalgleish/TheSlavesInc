using UnityEngine;

public class DragDropPuzzleManager : MonoBehaviour
{
    public GameObject[] objectHolder; //left side
    public GameObject[] toDrag;  //right side
    public int minObjectsToShow = 4;
    public int maxObjectsToShow = 5;

    private bool puzzleCompleted = false; // Track if the puzzle has been completed

    public GameObject puzzleClearPanel; // Reference to the puzzle clear panel


    private void Start()
    {
        SetRandomLeftObjects();
        puzzleClearPanel.SetActive(false); // Ensure the panel is hidden initially
    }

    private void Update()
    {
        if (!puzzleCompleted && VisibleObjectsLocked())
        {
            puzzleCompleted = true;
            ShowPuzzleClearScreen();
            Debug.Log("puzzle cleared!");
        }

        // Check for 'E' key press to hide the panel
        if (Input.GetKeyDown(KeyCode.E))
        {
            HidePuzzleClearScreen();
        }
    }

    private bool VisibleObjectsLocked()
    {
        foreach (GameObject obj in toDrag)
        {
            DragDropPuzzle dragDropPuzzle = obj.GetComponent<DragDropPuzzle>();
            if (dragDropPuzzle.targetObject.activeSelf && !dragDropPuzzle.IsLocked())  //check if the right side objects is linked to an active leftside object, and is locked
            {
                return false; //if any visible object isnt locked, return false
            }
        }
        return true; //all visible objects are locked
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
