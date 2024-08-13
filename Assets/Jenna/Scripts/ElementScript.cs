using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Build.Content;

public class ElementScript : MonoBehaviour, IPointerClickHandler
{
    public Image targetImage;       // The UI Image component to change
    public Sprite[] newImages;      // Array of sprites to cycle through
    public Sprite targetSprite;     // The target sprite for matching
    public GameManager gameManager; // Reference to the GameManager (if needed)

    private int currentIndex = 0;   // Index for cycling through newImages
    private bool interactable = true; // Track if the element is interactable

    // Method to change the image sprite
    public void ImageChange()
    {
        if (newImages.Length == 0 || !interactable) return; // Check if interactable
        targetImage.sprite = newImages[currentIndex];
        currentIndex = (currentIndex + 1) % newImages.Length;
    }

    // Handle pointer clicks
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return; // Prevent interaction if not interactable
        ImageChange();

        // Optionally, you can add a check here to inform the GameManager
        gameManager?.CheckPuzzleCompletion();
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
        // Optionally disable the Image component or Button component if used
        // targetImage.GetComponent<Button>().interactable = false; // Uncomment if using Buttons
    }
}