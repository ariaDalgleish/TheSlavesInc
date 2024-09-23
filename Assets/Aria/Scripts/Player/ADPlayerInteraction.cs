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
        // Check if the other collider has an InteractableButton component
        InteractableButton interactable = other.GetComponent<InteractableButton>();
        if (interactable != null)
        {
            currentInteractable = interactable; // Store the current interactable
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider was the current interactable
        if (other.GetComponent<InteractableButton>() == currentInteractable)
        {
            currentInteractable = null; // Reset the current interactable
        }
    }

}
 