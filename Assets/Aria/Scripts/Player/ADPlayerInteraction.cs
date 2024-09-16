using UnityEngine;
using UnityEngine.InputSystem;

public class ADPlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;
    private ADPlayerInputControls playerControls;

    void Start()
    {
        playerControls = GetComponent<ADPlayerInputControls>();
        InitialiseInputs();
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
            currentInteractable.OnInteract();
        }
    }

    private void InteractCanceled(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnStopInteract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable.OnStopInteract();
            currentInteractable = null;
        }
    }
}
