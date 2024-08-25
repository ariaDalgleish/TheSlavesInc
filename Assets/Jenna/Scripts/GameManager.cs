using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image[] topImages; // Array of top images
    public ElementScript[] bottomImageScripts; // Array of ElementScripts for bottom images
    public Sprite[] allSprites; // Array of all possible sprites
    public GameObject puzzleClearPanel; // Reference to the puzzle clear panel

    private bool puzzleCompleted = false;
    private PRMElement resetManager;

    private void Start()
    {
        SetupPuzzle();
        puzzleClearPanel.SetActive(false); // Ensure the panel is hidden initially

        resetManager = FindObjectOfType<PRMElement>(); // Find the PRMElement script in the scene
    }

    private void Update()
    {
        // Check for the 'E' key press to hide the puzzle clear panel
        if (puzzleCompleted && Input.GetKeyDown(KeyCode.E))
        {
            HidePuzzleClearScreen();
        }
    }

    // Method to set up the puzzle
    public void SetupPuzzle()
    {
        // Create a list to store all possible sprites
        List<Sprite> allSpriteList = new List<Sprite>(allSprites);

        // Randomly select 3 unique sprites for the top images
        List<Sprite> selectedTopSprites = new List<Sprite>();
        while (selectedTopSprites.Count < 3)
        {
            int randomIndex = Random.Range(0, allSpriteList.Count);
            Sprite randomSprite = allSpriteList[randomIndex];
            if (!selectedTopSprites.Contains(randomSprite))
            {
                selectedTopSprites.Add(randomSprite);
            }
        }

        // Remove selected top sprites from the list
        foreach (Sprite sprite in selectedTopSprites)
        {
            allSpriteList.Remove(sprite);
        }

        // Ensure there are enough sprites left for the bottom images
        if (allSpriteList.Count < 3)
        {
            Debug.LogError("Not enough unique sprites available for bottom images.");
            return;
        }

        // Randomly select 3 unique sprites for the bottom images from the remaining sprites
        List<Sprite> selectedBottomSprites = new List<Sprite>();
        while (selectedBottomSprites.Count < 3)
        {
            int randomIndex = Random.Range(0, allSpriteList.Count);
            Sprite randomSprite = allSpriteList[randomIndex];
            if (!selectedBottomSprites.Contains(randomSprite))
            {
                selectedBottomSprites.Add(randomSprite);
            }
        }

        // Assign the selected sprites to the top images
        for (int i = 0; i < topImages.Length; i++)
        {
            topImages[i].sprite = selectedTopSprites[i];
        }

        // Shuffle the selected bottom sprites
        ShuffleList(selectedBottomSprites);

        // Assign the shuffled sprites to the bottom images
        for (int i = 0; i < bottomImageScripts.Length; i++)
        {
            bottomImageScripts[i].SetInitialImage(selectedBottomSprites[i], selectedTopSprites[i]);
        }
    }

    // Method to shuffle a list
    private void ShuffleList(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Method to check if the puzzle is completed
    public void CheckPuzzleCompletion()
    {
        if (puzzleCompleted) return; // Avoid re-checking if already completed

        foreach (ElementScript elementScript in bottomImageScripts)
        {
            if (elementScript.targetImage.sprite != elementScript.targetSprite)
            {
                return; // Puzzle is not yet completed
            }
        }

        Debug.Log("Puzzle Completed!");
        puzzleCompleted = true;
        ShowPuzzleClearScreen();
        DisableAllBottomImages();

        // Call the ResetPuzzle method from the reset manager after puzzle is completed
        if (resetManager != null)
        {
            resetManager.ResetPuzzle();
        }
    }

    // Show the puzzle clear panel
    private void ShowPuzzleClearScreen()
    {
        if (puzzleClearPanel != null)
        {
            puzzleClearPanel.SetActive(true);
        }
    }

    // Hide the puzzle clear panel
    private void HidePuzzleClearScreen()
    {
        if (puzzleClearPanel != null)
        {
            puzzleClearPanel.SetActive(false);
        }
    }

    // Disable interaction with bottom images
    private void DisableAllBottomImages()
    {
        foreach (ElementScript elementScript in bottomImageScripts)
        {
            elementScript.DisableInteraction(); // Method to disable interaction in ElementScript
        }
    }
}

