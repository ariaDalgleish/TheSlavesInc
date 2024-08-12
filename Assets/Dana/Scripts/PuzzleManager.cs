using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzlePanel;
    public Transform player;  // Assign this via the Inspector to your player object
    private bool isPuzzleActive = false;
    private SolarPanel solarPanel;

    private Player playerScript; // Reference to the Player script

    void Start()
    {
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
            solarPanel = puzzlePanel.GetComponent<SolarPanel>();
            if (solarPanel == null)
            {
                Debug.LogError("solar panel script not found on the puzzlepanel");
            }
        }
        else
        {
            Debug.LogError("puzzle panel is not assigned in the inspector");
        }


        // Attempt to get the Player script from the player GameObject
        playerScript = player.GetComponent<Player>();

        if (playerScript == null)
        {
            Debug.LogError("Player script not found on the player object.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isPuzzleActive)
            {
                TogglePuzzlePanel();
            }
            else
            {
                ClosePuzzlePanel();
            }
        }
    }

    void TogglePuzzlePanel()
    {
        if (puzzlePanel != null && solarPanel !=null)
        {
            isPuzzleActive = true;
            puzzlePanel.SetActive(true);
            FreezePlayer();
            solarPanel.ResetPuzzle(); //reset puzzle when toggle the panel
        }
 
    }

    void ClosePuzzlePanel()
    {
        isPuzzleActive = false;
        puzzlePanel.SetActive(false);
        UnfreezePlayer();
    }

    void FreezePlayer()
    {
        if (playerScript != null)
        {
            playerScript.enabled = false;
            Debug.Log("Player frozen.");
        }
    }

    void UnfreezePlayer()
    {
        if (playerScript != null)
        {
            playerScript.enabled = true;
            Debug.Log("Player unfrozen.");
        }
    }
}



