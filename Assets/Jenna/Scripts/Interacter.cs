using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Interacter : MonoBehaviour
{
    public GameObject puzzlePanel;
    public ADPlayerMovement playerMovement; //reference to player movement script
    private bool isPlayerInRange = false;  //track if player is within interaction range
    private bool isPanelActive = false; //track if the puzzle panel is currently active


    private void Start()
    {
        puzzlePanel.SetActive(false);
        playerMovement.enabled = true;
    }

    private void Update()
    {

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TogglePuzzlePanel(); // Calls the TogglePuzzlePanel method
        }


    }

    private void TogglePuzzlePanel()
    {

        isPanelActive = !puzzlePanel.activeSelf; //properly toggle the panel's active state
        puzzlePanel.SetActive(isPanelActive);
        Cursor.visible = isPanelActive; // Makes the cursor visible only when the panel is active
        Cursor.lockState = isPanelActive ? CursorLockMode.None : CursorLockMode.Locked; // Unlocks the cursor from the center of the screen

        if (isPanelActive)
        {
            FreezePlayerMovement();
        }
        else
        {
            UnFreezePlayerMovement();
        }
    }

    private void FreezePlayerMovement()
    {
        Debug.Log("player is frozen,,,");
    }

    private void UnFreezePlayerMovement()
    {
        playerMovement.enabled = true;
    }

    public void HidePuzzlePanel()
    {
        puzzlePanel.SetActive(false);
       
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
            isPlayerInRange =false;
            puzzlePanel.SetActive(false );
            isPanelActive=false;
            UnFreezePlayerMovement();
        }
    }



}
