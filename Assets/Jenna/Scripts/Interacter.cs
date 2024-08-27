using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    
public class Interacter : MonoBehaviour
{
    public GameObject puzzlePanel;
    private bool isPlayerInRange = false;
    

    private void Start()
    {
        puzzlePanel.SetActive(false);
       
    }

    private void Update()
    {
   
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            TogglePuzzlePanel(); // Calls the TogglePuzzlePanel method
        }

       


        /* if (!puzzlePanel.activeSelf) // If the puzzle panel is not active
         {
             Cursor.visible = false;
             Cursor.lockState = CursorLockMode.Locked;
         }*/
    } 

    private void TogglePuzzlePanel()
    {

        bool isActive = !puzzlePanel.activeSelf;
        puzzlePanel.SetActive(isActive);
        Cursor.visible = isActive; // Makes the cursor visible only when the panel is active
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked; // Unlocks the cursor from the center of the screen
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
        }
    }
}
