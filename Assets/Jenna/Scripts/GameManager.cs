using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image[] topImages; // Array of top images
    public ElementScript[] bottomImageScripts; // Array of ElementScripts for bottom images
    public Sprite[] allSprites; // Array of all possible sprites

    private void Start()
    {
        SetupPuzzle();
    }

    // Method to set up the puzzle
    public void SetupPuzzle()
    {
        // Randomly select 3 sprites from the array for the top images
        List<Sprite> selectedTopSprites = new List<Sprite>();
        while (selectedTopSprites.Count < 3)
        {
            Sprite randomSprite = allSprites[Random.Range(0, allSprites.Length)];
            if (!selectedTopSprites.Contains(randomSprite))
            {
                selectedTopSprites.Add(randomSprite);
            }
        }

        // Assign the selected sprites to the top images
        for (int i = 0; i < topImages.Length; i++)
        {
            topImages[i].sprite = selectedTopSprites[i];
        }

        // Randomly select 3 different sprites from the array for the bottom images
        List<Sprite> selectedBottomSprites = new List<Sprite>();
        while (selectedBottomSprites.Count < 3)
        {
            Sprite randomSprite = allSprites[Random.Range(0, allSprites.Length)];
            if (!selectedBottomSprites.Contains(randomSprite))
            {
                selectedBottomSprites.Add(randomSprite);
            }
        }

        // Shuffle the selected bottom sprites
        ShuffleList(selectedBottomSprites);

        // Assign the shuffled sprites to the bottom images with the corresponding target sprites
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
        foreach (ElementScript elementScript in bottomImageScripts)
        {
            if (elementScript.targetImage.sprite != elementScript.targetSprite)
            {
                return; // Puzzle is not yet completed
            }
        }

        Debug.Log("Puzzle Completed!");
        // Add logic to handle puzzle completion (e.g., close the panel)
    }
}

