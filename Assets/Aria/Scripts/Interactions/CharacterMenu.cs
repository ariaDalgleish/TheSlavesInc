using Photon.Pun;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    PhotonView photonView;

    [SerializeField] private GameObject menu;
    private bool isMenuActive = false;
    private bool isPlayerInRange = false;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            menu.SetActive(isMenuActive);
        }
        else
        {
            enabled = false; // Disable this script for other players
        }
    }

    void Update()
    {
        // Check if 'E' is pressed and the player is in range AND the menu is not active
        if (isPlayerInRange && !isMenuActive && Input.GetKeyDown(KeyCode.E))
        {
            ToggleMenu(true); // Open the menu
        }
    }

    private void ToggleMenu(bool state)
    {
        isMenuActive = state;
        menu.SetActive(isMenuActive);

        if (isMenuActive)
        {
            DisablePlayerMovement();
        }
        else
        {
            EnablePlayerMovement();
        }

        photonView.RPC("SyncMenuState", RpcTarget.Others, isMenuActive);
    }

    private void DisablePlayerMovement()
    {
        ADPlayerMovement movementScript = photonView.GetComponent<ADPlayerMovement>();
        if (movementScript != null)
        {
            movementScript.canMove = false; // Disable player movement
        }
    }

    private void EnablePlayerMovement()
    {
        ADPlayerMovement movementScript = photonView.GetComponent<ADPlayerMovement>();
        if (movementScript != null)
        {
            movementScript.canMove = true; // Enable player movement
        }
    }

    [PunRPC]
    private void SyncMenuState(bool state)
    {
        isMenuActive = state;
        menu.SetActive(isMenuActive);
    }

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

    public void CloseMenu() // Method to close the menu
    {
        ToggleMenu(false);
    }
}
