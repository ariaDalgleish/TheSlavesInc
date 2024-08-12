using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public GameObject puzzlePanel;
    public Transform player;
    public float interactDistance = 3f; //distance within which the player can interact with objects
    private bool isPuzzleActive = false; //track if the puzzle is active

    Playerrr playerScript; //actual player movement script


    void Start()
    {
        playerScript = player.GetComponent<Playerrr>();

        if (playerScript == null)
        {
            Debug.LogError("Player script not found on the player object.");
        }
    }

    void Update()
    {
        //check distance between player and the puzzle object
        if (Vector3.Distance(player.position, transform.position) <= interactDistance)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(!isPuzzleActive)
                {
                    OpenPuzzlePanel();
                }
                else
                {
                    ClosePuzzlePanel();
                }
            }
        }

    }

    void OpenPuzzlePanel()
    {
        isPuzzleActive = true;
        puzzlePanel.SetActive(true);
        FreezePlayer();
    }


    void ClosePuzzlePanel()
    {
        isPuzzleActive = false;
        puzzlePanel.SetActive(false);
        UnFreezePlayer();
    }

    void FreezePlayer()
    {
        if (playerScript != null)
        {
            playerScript.enabled = false;
            Debug.Log("player frozen");
        }

    }

    void UnFreezePlayer()
    {
        if (playerScript != null)
        {
            playerScript.enabled = true;
            Debug.Log("player unfrozen");
        }
    }


}
