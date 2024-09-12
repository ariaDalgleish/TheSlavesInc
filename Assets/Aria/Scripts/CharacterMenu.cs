using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMenu : MonoBehaviourPun
{
    [SerializeField] private GameObject menu;  // The character selection menu to enable/disable
    [SerializeField] private ADPlayerMovement playerMovement;  // Reference to your player movement script
    private ADPlayerInputControls playerControls;
    private bool isPlayerInRange = false;

    private void Start()
    {
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
        playerControls.BaseControls.BaseControls.Interact.performed += OnInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (isPlayerInRange)
        {
            ToggleMenu();
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // Update logic if needed
        }
    }

    private void ToggleMenu()
    {
        bool isActive = menu.activeSelf;
        menu.SetActive(!isActive);  // Toggle menu visibility

        if (playerMovement != null)
        {
            playerMovement.canMove = isActive;  // Enable movement when the menu is closed
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
}
