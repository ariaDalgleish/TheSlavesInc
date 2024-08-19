using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    PhotonView photonView;
    ADPlayerInputControls playerControls;
    private InteractableButton currentInteraction;
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
        playerControls.BaseControls.BaseControls.Interact.performed += Interact_performed;
        playerControls.BaseControls.BaseControls.Interact.canceled += Interact_canceled;
    }

    private void Interact_canceled(InputAction.CallbackContext obj)
    {
        if (currentInteraction != null)
        {
            currentInteraction.ButtonReleased();
        }
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if (currentInteraction != null)
        {
            currentInteraction.ButtonPressed();
        }
    }

    void Update()
    {
        RaycastCheck();
    }

    private void RaycastCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, layerMask)) // Raycast to check if the player is looking at an interactable button
        {
            InteractableButton button = hit.collider.GetComponent<InteractableButton>(); 
            if (button != null)
            {
                currentInteraction = button;
                if (playerControls.BaseControls.BaseControls.Interact.ReadValue<float>() > 0)
                {
                    button.ButtonPressed();
                }
                else
                {
                    button.ButtonReleased();
                }
            }
        }
        else
        {
            if (currentInteraction != null)
            {
                currentInteraction.ButtonReleased();
                currentInteraction = null;
            }
        }
    }
}
