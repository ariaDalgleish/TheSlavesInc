using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PipeScript : MonoBehaviour, IPointerClickHandler
{
    float[] rotations = { 0, 90, 180, 270 };

    public float correctRotation;
    [SerializeField]
    private bool isPlaced = false;

    private Interacter interacter; // Reference to the Interacter script

    private void Start()
    {
        interacter = FindObjectOfType<Interacter>(); // Find the Interacter script in the scene
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
        Debug.Log("Initial Rotation: " + transform.eulerAngles.z);

        CheckIfPlaced();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Click on Pipe");
        RotatePipe();
    }

    private void RotatePipe()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        Debug.Log("Rotated to: " + transform.eulerAngles.z);
        CheckIfPlaced();
    }

    private void CheckIfPlaced()
    {
        // Use modulo to handle the rotations and floating-point comparison
        float currentRotation = Mathf.Round(transform.eulerAngles.z % 360);
        float targetRotation = Mathf.Round(correctRotation % 360);

        if (Mathf.Approximately(currentRotation, targetRotation))
        {
            isPlaced = true;
            Debug.Log("Pipe is correctly placed.");
        }
        else
        {
            isPlaced = false;
            Debug.Log("Pipe is not correctly placed.");
        }

        // Check if all pipes are correctly placed
        CheckAllPipes();
    }

    private void CheckAllPipes()
    {
        // Find all PipeScript components in the scene
        PipeScript[] allPipes = FindObjectsOfType<PipeScript>();

        // Check if all pipes are placed correctly
        foreach (PipeScript pipe in allPipes)
        {
            if (!pipe.isPlaced)
            {
                return;
            }
        }

        // If all pipes are placed correctly, hide the puzzle panel
        interacter.HidePuzzlePanel();
    }
}

