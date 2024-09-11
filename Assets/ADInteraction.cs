using UnityEngine;
using System.Collections;
using Photon.Pun;

public class ADInteraction : MonoBehaviour
{
    PhotonView photonView;

    public GameObject menu;  
    private ADPlayerMovement playerMovement;  
    private bool isPlayerInRange = false;  
    private ADPlayerInputControls playerControls;  

    private void Start()
    {
        StartCoroutine(InitializePlayer());
    }

    private IEnumerator InitializePlayer()
    {
        // Continuously search for the player GameObject
        while (playerMovement == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerMovement = player.GetComponent<ADPlayerMovement>();
                playerControls = player.GetComponent<ADPlayerInputControls>();

                if (playerMovement != null && playerControls != null)
                {
                    SetupInputs();
                    yield break;  // Exit the coroutine once initialization is complete
                }
            }

            yield return null;  // Wait one frame before trying again
        }
    }

    private void SetupInputs()
    {
        if (playerControls != null)
        {
            playerControls.BaseControls.BaseControls.MenuInteract.performed += OnInteractPerformed;
        }
    }

    private void OnInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (isPlayerInRange)
        {
            ToggleMenu();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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

    private void ToggleMenu()
    {
        bool isActive = menu.activeSelf;
        menu.SetActive(!isActive);
        if (playerMovement != null)
        {
            playerMovement.canMove = !isActive;
        }
    }
}
