using System.Collections;
using UnityEngine;

public class PuzzleResetManager : MonoBehaviour
{
    public float resetDelay = 10f; // Time to wait before resetting the puzzle
    private MonoBehaviour puzzleScript; // Reference to the puzzle script

    public SpriteRenderer resetReadySpriteRenderer; // Reference to the SpriteRenderer
    public Sprite resetReadySprite; // The sprite to display when the puzzle is reset

    public Vector3 spritePosition = new Vector3(0, 0, 0); // Desired position of the sprite
    public Vector3 spriteScale = new Vector3(1, 1, 1); // Desired scale of the sprite

    void Start()
    {
        if (resetReadySpriteRenderer != null)
        {
            resetReadySpriteRenderer.enabled = false; // Hide the sprite initially
            resetReadySpriteRenderer.transform.position = spritePosition; // Set the initial position
            resetReadySpriteRenderer.transform.localScale = spriteScale; // Set the initial scale
        }
    }

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
            ShowResetReadySprite(); // Show the sprite indicating the puzzle is ready
        }
    }

    private void ShowResetReadySprite()
    {
        if (resetReadySpriteRenderer != null && resetReadySprite != null)
        {
            resetReadySpriteRenderer.sprite = resetReadySprite; // Set the sprite
            resetReadySpriteRenderer.enabled = true; // Show the sprite
        }
    }

    private IEnumerator HideResetReadySpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (resetReadySpriteRenderer != null)
        {
            resetReadySpriteRenderer.enabled = false; // Hide the sprite after the delay
        }
    }
}

