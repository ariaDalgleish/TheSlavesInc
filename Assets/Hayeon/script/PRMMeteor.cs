using System.Collections;
using UnityEngine;

public class PRMMeteor : MonoBehaviour
{
    public float resetDelay = 10f; // TIme to wait before resetting the puzzle
    private MeteorScript meteorScript; // Reference to the MeteorScript puzzle script

   // Start the reset coroutine for the meteor puzzle
   public void StartResetCoroutine(MeteorScript meteorScript)
    {
        this.meteorScript = meteorScript;
        StartCoroutine(ResetPuzzleWithDelay());
    }

    // Coroutine that handles the delay before resetting the puzzle
    private IEnumerator ResetPuzzleWithDelay()
    {
        Debug.Log("Starting meteor puzzle reset coroutine...");
        yield return new WaitForSeconds(resetDelay); // Wait for the specified delay

        if (meteorScript != null )
        {
            Debug.Log("Resetting meteor puzzle...");
            meteorScript.ResetPuzzle(); // Call the reset function in the MeteorScript
        }
        else
        {
            Debug.Log("MeteorScript reference is null!");
        }
    }
}
