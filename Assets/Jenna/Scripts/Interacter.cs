using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Interacter : MonoBehaviour
{
    PhotonView pV;

    public GameObject puzzlePanel;
    public Button closeButton; // Reference to the close button

    private bool isPlayerInRange = false;  //track if player is within interaction range
    private bool isPanelActive = false; //track if the puzzle panel is currently active

    private bool playerUsing;
    private int playerID = -1;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        puzzlePanel.SetActive(false);

        // Assign the ClosePuzzlePanel method to the button's onClick event
        closeButton.onClick.AddListener(ClosePuzzlePanel);
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
        playerUsing = true;
        if (playerID >= 0)
        {
            return;
        }
        playerID = id;
    }

    [PunRPC]
    public void StopInteraction()
    {
        playerUsing = false;
        playerID = -1;
    }

    private void TogglePuzzlePanel()
    {
        if (playerUsing)
        {
            return;
        }
        if (ADNetworkManager.instance == null)
        {
            Debug.Log("huh??");
        }
        pV.RPC("PlayerInteracted", RpcTarget.All, ADNetworkManager.instance.PlayerID);
        if (playerID == ADNetworkManager.instance.PlayerID)
        {
            isPanelActive = !puzzlePanel.activeSelf; //properly toggle the panel's active state
            puzzlePanel.SetActive(isPanelActive);
            Cursor.visible = isPanelActive; // Makes the cursor visible only when the panel is active
            Cursor.lockState = isPanelActive ? CursorLockMode.None : CursorLockMode.Locked; // Unlocks the cursor from the center of the screen

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
        puzzlePanel.SetActive(false); // Deactivate the puzzle panel
        pV.RPC("StopInteraction", RpcTarget.All);
        EnablePlayerMovement(); // Reenable player movement after closing the panel
        isPanelActive = false; // Mark the panel as inactive
        Cursor.visible = false; // Hide the cursor again after closing the panel
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor back to the center of the screen
    }


    public void HidePuzzlePanel()
    {
        puzzlePanel.SetActive(false);
        pV.RPC("StopInteraction", RpcTarget.All);
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
