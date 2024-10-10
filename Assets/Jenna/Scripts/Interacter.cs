using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Interacter : MonoBehaviour
{
    PhotonView pV;

    public GameObject puzzlePanel;
    //public Button closeButton; // Reference to the close button

    private bool isPlayerInRange = false;  //track if player is within interaction range
    private bool isPanelActive = false; //track if the puzzle panel is currently active

    private bool playerUsing;
    private int playerID = -1;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        puzzlePanel.SetActive(false);

        // Assign the ClosePuzzlePanel method to the button's onClick event
        //closeButton.onClick.AddListener(ClosePuzzlePanel);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TogglePuzzlePanel(); // Calls the TogglePuzzlePanel method
        }
    }

    [PunRPC]
    public void PlayerInteracted(int id)
    {
        Debug.Log("RPC PlayerInteracted called. Player ID: " + id);
        playerUsing = true;
        if (playerID >= 0)
        {
            Debug.Log("Another player is already interacting.");
            return;
        }
        playerID = id;
    }

    [PunRPC]
    public void StopInteraction()
    {
        Debug.Log("RPC StopInteraction called.");
        playerUsing = false;
        playerID = -1;
    }


    private void TogglePuzzlePanel()
    {
        if (playerUsing)
        {
            return;
        }

        Debug.Log("Toggling panel, PhotonView isMine: " + pV.IsMine);

        pV.RPC("PlayerInteracted", RpcTarget.All, Launcher.instance.PlayerID);

        if (PhotonNetwork.IsMasterClient || playerID == Launcher.instance.PlayerID)
        {
            Debug.Log("Panel interaction for Master Client or Player.");

            isPanelActive = !puzzlePanel.activeSelf; // Properly toggle the panel's active state
            puzzlePanel.SetActive(isPanelActive);

            Cursor.visible = isPanelActive; // Show cursor if the panel is active
            Cursor.lockState = isPanelActive ? CursorLockMode.None : CursorLockMode.Locked; // Unlock cursor if the panel is active

            if (isPanelActive)
            {
                DisablePlayerMovement();
            }
            else
            {
                EnablePlayerMovement();
            }
        }
    }


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

    // Method to close the puzzle panel via the button
    public void ClosePuzzlePanel()
    {
        // Sync the action across all clients
        pV.RPC("StopInteraction", RpcTarget.AllBuffered);

        // Ensure that the logic is executed locally for the master client and the player
        if (playerID == Launcher.instance.PlayerID || PhotonNetwork.IsMasterClient)
        {
            puzzlePanel.SetActive(false); // Deactivate the puzzle panel
            EnablePlayerMovement(); // Reenable player movement after closing the panel
            isPanelActive = false; // Mark the panel as inactive
            //Cursor.visible = false; // Hide the cursor again after closing the panel
            //Cursor.lockState = CursorLockMode.Locked; // Lock the cursor back to the center of the screen
        }
    }



    public void HidePuzzlePanel()
    {
        puzzlePanel.SetActive(false);
        pV.RPC("StopInteraction", RpcTarget.AllBuffered);
        EnablePlayerMovement();
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
            puzzlePanel.SetActive(false);
            isPanelActive = false;
        }
    }
}
