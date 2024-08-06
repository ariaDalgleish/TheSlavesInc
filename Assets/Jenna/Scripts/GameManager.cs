using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Image[] topImages;
    public ElementScript[] bottomImageScripts;
    public Sprite[] allSprites;

    // Start is called before the first frame update
    void Start()
    {
        SetupPuzzle();
    }

    public void SetupPuzzle()
    {
        List<Sprite> selectedSprites = new List<Sprite>();
        while (selectedSprites.Count < 3)
        {
            Sprite randomSprite = allSprites[Random.Range(0, allSprites.Length)];
            if (!selectedSprites.Contains(randomSprite))
            {
                selectedSprites.Add(randomSprite);
            }
        }

        
        for (int i = 0; i < topImages.Length; i++)
        {
            topImages[i].sprite = selectedSprites[i];
        }

        
        List<Sprite> shuffledSprites = new List<Sprite>(selectedSprites);
        for (int i = 0; i < bottomImageScripts.Length; i++)
        {
            bottomImageScripts[i].SetInitialImage(shuffledSprites[i], selectedSprites[i]);
        }
    }

    public void CheckPuzzleCompletion()
    {
        foreach (ElementScript elementScript in bottomImageScripts)
        {
            if (elementScript.targetImage.sprite != elementScript.targetSprite)
            {
                return; // Puzzle is not yet completed
            }
        }

        Debug.Log("Puzzle Completed");
        
    }
}


