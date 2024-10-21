using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ElementScript : MonoBehaviour, IPointerClickHandler
{
    AudioManager audioManager;
    public Image targetImage;       // The UI Image component to change
    public Sprite[] newImages;      // Array of sprites to cycle through
    public Sprite targetSprite;     // The target sprite for matching
    public GameManager gameManager; // Reference to the GameManager (if needed)

    private int currentIndex = 0;   // Index for cycling through newImages
    private bool interactable = true; // Track if the element is interactable
    //private bool puzzleCompleted = false; // Track if the puzzle has been completed

    private Sprite initialSprite; // Store the initial sprite for resetting

    public PRMElement resetManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Find the reset manager script in the Puzzle Panel
        resetManager = GetComponentInParent<PRMElement>();

        // Store the initial sprite
        initialSprite = targetImage.sprite;
    }

    // Method to change the image sprite
    public void ImageChange()
    {
        audioManager.PlaySFX(audioManager.mouseClick);

        if (newImages.Length == 0 || !interactable) return; // Check if interactable
        targetImage.sprite = newImages[currentIndex];
        currentIndex = (currentIndex + 1) % newImages.Length;

        // Check if the current sprite matches the target sprite
        if (targetImage.sprite == targetSprite)
        {
            //puzzleCompleted = true; // Mark the puzzle as completed
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
    public void SetInitialImage(Sprite initialImage, Sprite target)
    {
        // Null check for initialImage
        if (initialImage == null)
        {
            Debug.LogError("Initial image is null in SetInitialImage!");
            return;
        }

        initialSprite = initialImage;
        targetSprite = target;
        targetImage.sprite = initialSprite;

    }

    // Disable interaction
    public void DisableInteraction()
    {
        interactable = false;
    }

    // Reset the element to its initial state
    public void ResetElement()
    {
        Debug.Log("Element reset to initial state");
        targetImage.sprite = initialSprite;
        currentIndex = 0;
        interactable = true;
        //puzzleCompleted = false; // Reset puzzle completed state
    }
}

