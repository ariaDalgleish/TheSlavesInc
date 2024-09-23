using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class ADPlayerInteraction : MonoBehaviour
{
    PhotonView photonView;

    private ADPlayerInputControls playerControls;
    InteractableButton currentInteractable;
    public LayerMask layerMask;
    [SerializeField] Transform visuals;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            playerControls = GetComponent<ADPlayerInputControls>();
            InitialiseInputs();
        }
        else
        {
            enabled = false;
        }
    }

    private void InitialiseInputs()
    {
        playerControls.BaseControls.BaseControls.Interact.performed += InteractPerformed;
        playerControls.BaseControls.BaseControls.Interact.canceled += InteractCanceled;
    }

    private void InteractPerformed(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.ButtonPressed();
        }
    }

    private void InteractCanceled(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.ButtonReleased();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider is on the Interactable layer
        if (((1 << other.gameObject.layer) & layerMask) != 0)
        {
            // Check for InteractableButton component
            InteractableButton interactable = other.GetComponent<InteractableButton>();
            if (interactable != null)
            {
                currentInteractable = interactable; // Store the current interactable
            }

            // Check for CharacterMenu component
            CharacterMenu characterMenu = other.GetComponent<CharacterMenu>();
            if (characterMenu != null)
            {
                // Handle menu interaction if needed
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider was the current interactable
        if (other.GetComponent<InteractableButton>() == currentInteractable)
        {
            currentInteractable = null; // Reset the current interactable
        }
        // Handle exit for CharacterMenu if needed
        if (other.GetComponent<CharacterMenu>() != null)
        {
            // Additional logic if required
        }
    }
}
