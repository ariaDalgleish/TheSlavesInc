using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRMDatasending : MonoBehaviour
{
    public PipeScript[] pipeScripts;
    public PuzzleClearManager puzzleClearManager;
    public float resetDelay = 15f; // Time delay before resetting the puzzle


    private bool resetInProgress = false;
    

    private void Start()
    {
        if (pipeScripts == null || pipeScripts.Length == 0)
        {
            pipeScripts = GetComponentsInChildren<PipeScript>();
        }

        if (puzzleClearManager == null)
        {
            puzzleClearManager = GetComponentInParent<PuzzleClearManager>();
        }
    }

    public void StartResetCoroutine()
    {
        if (!resetInProgress)
        {
            StartCoroutine(ResetPuzzleAfterDelay());
        }
    }

    private IEnumerator ResetPuzzleAfterDelay()
    {
        Debug.Log("Waitting for reset delay...");
        resetInProgress = true;

        Debug.Log("Waiting for " + resetDelay + " seconds...");
        yield return new WaitForSeconds(resetDelay);

        foreach (PipeScript pipe in pipeScripts)
        {
            Debug.Log("Resetting pipe: " + pipe.name);
            pipe.ResetPipe();
        }

        Debug.Log("Resetting the PuzzleClearManager...");
        puzzleClearManager.HidePuzzleClearScreen();

        resetInProgress = false;
        Debug.Log("Puzzle has been reset.");

    }

}
