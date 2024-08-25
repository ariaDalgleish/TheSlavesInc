using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRMElement : MonoBehaviour
{
    public float resetDelay = 5f; // Time to wait before resetting the puzzle
    public GameObject resetReadySprite; // The sprite that indicates the puzzle is ready to play again

    private ElementScript[] elementScripts;

    private void Awake()
    {
        elementScripts = GetComponentsInChildren<ElementScript>(); // Find all ElementScripts within the parent canvas
    }

    public void ResetPuzzle()
    {
        StartCoroutine(ResetPuzzleWithDelay());
    }

    private IEnumerator ResetPuzzleWithDelay()
    {
        yield return new WaitForSeconds(resetDelay); // Wait for the reset delay

        HashSet<int> usedIndexes = new HashSet<int>(); // To keep track of used sprite indexes

        foreach (var elementScript in elementScripts)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, elementScript.newImages.Length);
            } while (usedIndexes.Contains(randomIndex)); // Ensure a different sprite is chosen

            usedIndexes.Add(randomIndex); // Mark this index as used
            elementScript.SetInitialImage(elementScript.newImages[randomIndex], elementScript.newImages[randomIndex]);

            elementScript.ResetElement(); // Reset each element in the puzzle
        }

        ShowResetReadySprite(); // Show the sprite indicating the puzzle is ready
    }

    private void ShowResetReadySprite()
    {
        if (resetReadySprite != null)
        {
            resetReadySprite.SetActive(true); // Show the reset ready sprite
        }
    }
}


