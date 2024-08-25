using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ElementScript : MonoBehaviour, IPointerClickHandler
{
    public Image targetImage;       // The UI Image component to change
    public Sprite[] newImages;      // Array of sprites to cycle through
    public Sprite targetSprite;     // The target sprite for matching
    public GameManager gameManager; // Reference to the GameManager (if needed)

    private int currentIndex = 0;   // Index for cycling through newImages
    private bool interactable = true; // Track if the element is interactable
    private bool puzzleCompleted = false; // Track if the puzzle has been completed

    private Sprite initialSprite; // Store the initial sprite for resetting

    public PRMElement resetManager;
    private void Start()
    {
        // Find the reset manager script in the Puzzle Panel
        resetManager = GetComponentInParent<PRMElement>();

        // Store the initial sprite
        initialSprite = targetImage.sprite;
    }

    // Method to change the image sprite
    public void ImageChange()
    {
        if (newImages.Length == 0 || !interactable) return; // Check if interactable
        targetImage.sprite = newImages[currentIndex];
        currentIndex = (currentIndex + 1) % newImages.Length;

        // Check if the current sprite matches the target sprite
        if (targetImage.sprite == targetSprite)
        {
            puzzleCompleted = true; // Mark the puzzle as completed
            gameManager?.CheckPuzzleCompletion(); // Notify GameManager
        }

    }

    // Handle pointer clicks
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return; // Prevent interaction if not interactable
        ImageChange();
    }

    // Set the initial image and target sprite
    public void SetInitialImage(Sprite initialSprite, Sprite target)
    {
        targetImage.sprite = initialSprite;
        targetSprite = target;
    }

    // Method to disable interaction
    public void DisableInteraction()
    {
        interactable = false;
        
    }

    public void EnableInteraction()
    {
        interactable = true;
    }

    // IPuzzle interface implementation
    public void ResetElement()
    {
        Debug.Log("ResetElement called for " + gameObject.name);

        // Reset the sprite to the initial sprite or perform other reset logic here
        currentIndex = 0; // Reset the index
        targetImage.sprite = initialSprite; // Reset the sprite to the initial image

        // Re-enable interaction if it was disabled
        interactable = true;
    }
}
