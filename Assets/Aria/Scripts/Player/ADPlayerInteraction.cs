using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class ADPlayerInteraction : MonoBehaviour
{
    PhotonView photonView;

    private ADPlayerInputControls playerControls;
    InteractableButton buttonInteractable;
    CharacterMenu menuInteractable;
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
            gameObject.tag = "Untagged";
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
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
        if (buttonInteractable != null)
        {
            buttonInteractable.ButtonPressed();
        }
    }

    private void InteractCanceled(InputAction.CallbackContext context)
    {
        if (buttonInteractable != null)
        {
            buttonInteractable.ButtonReleased();
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
                buttonInteractable = interactable; // Store the current interactable
            }

            // Check for CharacterMenu component
            CharacterMenu characterMenu = other.GetComponent<CharacterMenu>();
            if (characterMenu != null)
            {
                // menuInteractable = interactable; interactable = the layer? idk
                // PlayerIsInRange ? 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider was the current interactable
        if (other.GetComponent<InteractableButton>() == buttonInteractable)
        {
            buttonInteractable = null; // Reset the current interactable
        }
        // Handle exit for CharacterMenu if needed
        if (other.GetComponent<CharacterMenu>() != null)
        {
            // Additional logic if required
        }
    }
}
