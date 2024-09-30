using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private bool menuActive = false;
    private bool isPlayerInRange = false;

    //void Start()
    //{
    //    photonView = GetComponent<PhotonView>();

    //    if (photonView.IsMine)
    //    {
    //        menu.SetActive(menuActive);
    //    }
    //    else
    //    {
    //        enabled = false; 
    //    }
    //}

    void Update()
    {
        // Check if 'E' is pressed and the player is in range AND the menu is not active
        if (isPlayerInRange && !menuActive && Input.GetKeyDown(KeyCode.E))
        {
            ToggleMenu(true); // Open the menu
        }
    }

    private void ToggleMenu(bool state)
    {
        menuActive = state;
        menu.SetActive(menuActive);

        if (menuActive)
        {
            DisablePlayerMovement();
        }
        else
        {
            EnablePlayerMovement();
        }
    }
    public void CloseMenu() // Method to close the menu
    {
        ToggleMenu(false);
    }

    // Find the PlayerMovement script using the "Player" tag.
    private void DisablePlayerMovement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            ADPlayerMovement movementScript = player.GetComponent<ADPlayerMovement>();
            if (movementScript != null)
            {
                movementScript.canMove = false; // Disable player movement
            }
        }
    }

    private void EnablePlayerMovement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            ADPlayerMovement movementScript = player.GetComponent<ADPlayerMovement>();
            if (movementScript != null)
            {
                movementScript.canMove = true; // Enable player movement
            }
        }
    }

    private void SyncMenuState(bool state)
    {
        menuActive = state;
        menu.SetActive(menuActive);
    }

    // Remove? Control the triggerEnter Collider through PlayerInteraction? 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure to set the tag on your player prefab
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }


}


