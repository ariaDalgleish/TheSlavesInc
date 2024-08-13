using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzlePanel;
    public Transform player;  // Assign this via the Inspector to your player object
    private bool isPuzzleActive = false;
    public float interactDistance = 3f;
    private SolarPanel solarPanel;
    private IPuzzle currentPuzzle;


    private Player playerScript; // Reference to the Player script

    void Start()
    {
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
            currentPuzzle = puzzlePanel.GetComponent<IPuzzle>();

            if (currentPuzzle == null)
            {
                Debug.LogError("No IPuzzle script found on the puzzlePanel.");
            }
        }
        else
        {
            Debug.LogError("puzzle panel is not assigned in the inspector");
        }

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
        if (puzzlePanel != null && currentPuzzle !=null)
        {
            isPuzzleActive = true;
            puzzlePanel.SetActive(true);
            FreezePlayer();
            currentPuzzle.ResetPuzzle(); //reset puzzle when toggle the panel
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



