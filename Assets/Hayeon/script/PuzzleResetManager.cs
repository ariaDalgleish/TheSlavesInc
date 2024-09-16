using System.Collections;
using UnityEngine;

public class PuzzleResetManager : MonoBehaviour
{
    public float resetDelay = 10f; // Time to wait before resetting the puzzle
    private MonoBehaviour puzzleScript; // Reference to the puzzle script


    public void StartResetCoroutine(MonoBehaviour puzzleScript)
    {
        this.puzzleScript = puzzleScript;
        StartCoroutine(ResetPuzzleWithDelay());
    }

    public IEnumerator ResetPuzzleCoroutine(DocumentSorter documentSorter)
    {
        Debug.Log("Starting reset coroutine...");
        yield return new WaitForSeconds(resetDelay);

        if (documentSorter != null)
        {
            Debug.Log("Resetting document sorter.");
            documentSorter.ResetPuzzle();
        }

        else
        {
            Debug.Log("Document sorter reference is null!");
        }
    }

    private IEnumerator ResetPuzzleWithDelay()
    {
        yield return new WaitForSeconds(resetDelay); // Wait for the reset delay
        IPuzzle puzzle = puzzleScript as IPuzzle; // Cast the script to IPuzzle
        if (puzzle != null)
        {
            puzzle.ResetPuzzle(); // Reset the puzzle after the delay
        }
    }

}

