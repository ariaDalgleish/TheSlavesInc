using Cinemachine.Examples;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CharacterMenu : MonoBehaviourPun, IInteractable
{
    [SerializeField] private GameObject menu;
    public ADPlayerMovement player;
    private bool isMenuActive = false; // Initialize to false to start with menu closed

    void Start()
    {
        menu.SetActive(isMenuActive);
    }

    

    public void OnInteract()
    {
        if (!isMenuActive) // Only open the menu if it's not already active
        {
            ToggleMenu();
        }
    }

    public void OnStopInteract()
    {
        // Optional logic if needed when stopping interaction
    }

    private void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menu.SetActive(isMenuActive);

        if (player != null)
        {
            player.canMove = !isMenuActive;
        }

        photonView.RPC("SyncMenuState", RpcTarget.Others, isMenuActive);
    }

    // Method to close the menu from the UI button
    public void CloseMenu()
    {
        if (isMenuActive)
        {
            isMenuActive = false;
            menu.SetActive(false);

            if (player != null)
            {
                player.canMove = true;
            }

            photonView.RPC("SyncMenuState", RpcTarget.Others, isMenuActive);
        }
    }

    [PunRPC]
    private void SyncMenuState(bool state)
    {
        isMenuActive = state;
        menu.SetActive(isMenuActive);

        if (player != null)
        {
            player.canMove = !isMenuActive;
        }
    }
}