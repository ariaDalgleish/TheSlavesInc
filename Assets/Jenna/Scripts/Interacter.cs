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
            TogglePuzzlePanel();
        }
    }

    private void TogglePuzzlePanel()
    {
        puzzlePanel.SetActive(!puzzlePanel.activeSelf);
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
