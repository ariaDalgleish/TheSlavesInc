using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropPuzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject targetObject;
    private bool isLocked = false;
    private Vector3 originalPosition;


    private void Start()
    {
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        // Check if the target object is active (visible) and close enough
        if (targetObject.activeSelf && Vector3.Distance(transform.position, targetObject.transform.position) < 50f)
        {
            transform.position = targetObject.transform.position;
            isLocked = true;

            DragDropPuzzleManager dragDropPuzzleManager = FindObjectOfType<DragDropPuzzleManager>();
            if (dragDropPuzzleManager != null && dragDropPuzzleManager.AreAllObjectsLocked())
            {
                Debug.Log("puzzle cleared!");
            }
        }
        else
        {
            transform.position = originalPosition;  // If the drop is wrong, return the object to its original position
        }
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    public void UnlockObject()
    {
        isLocked = false;
        transform.position = originalPosition; // Reset the position when unlocking
    }


}
