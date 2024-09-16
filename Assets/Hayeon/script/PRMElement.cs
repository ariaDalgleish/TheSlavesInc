using System.Collections;
using UnityEngine;

public class PRMElement : MonoBehaviour
{
    public ElementScript[] elementScripts; // Reference to the ElementScript components
    public GameManager gameManager; // Reference to the GameManager
    public float resetDelay = 5; // Delay before puzzle resets

    private bool resetInProgress = false; // To prevent multiple reset triggers

    private void Start()
    {
        if (elementScripts == null || elementScripts.Length == 0)
        {
            elementScripts = GetComponentsInChildren<ElementScript>();
        }

        // Optionally, find the GameManager in the parent
        if (gameManager == null)
        {
            gameManager = GetComponentInParent<GameManager>();
        }
    }

    public void StartResetCoroutine()
    {
        if (!resetInProgress) // Only start the timer if it's not already in progress
        {
            StartCoroutine(ResetPuzzleWithDelay());
        }
    }

    private IEnumerator ResetPuzzleWithDelay()
    {
        Debug.Log("waiting for reset delay...");
        resetInProgress = true;

        // wait for the reset delay
        Debug.Log("Waiting for " + resetDelay + " seconds...");
        yield return new WaitForSeconds(resetDelay);

        // Reset each ElementScript to its initial state
        foreach (ElementScript element in elementScripts)
        {
            Debug.Log("Resetting puzzle...");
            element.ResetElement();
        }

        // Reset the GameManager to set up the puzzle again
        Debug.Log("Resetting the GameManager...");
        gameManager.SetupPuzzle();

        resetInProgress = false; // Reset process completed, allow for future resets
        Debug.Log("Reset completed.");
    }


}


