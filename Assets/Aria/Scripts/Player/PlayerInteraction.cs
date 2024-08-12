using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    /* This script is responsible for the player interaction. It reads the input from the player input script and interacts with the interactable objects accordingly. 
     
    PhotonView photonView;

    PlayerInputControls playerInputs;
    private InteractableButton currentInteraction;
    public LayerMask layerMask;
    [SerializeField] Transform visuals;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            playerInputs = GetComponent<PlayerInputControls>();
            InitialiseInputs();
        }
        else
        {
            enabled = false;
        }
      

    }

    private void InitialiseInputs()
    {
        playerInputs.masterControls.BaseControls.Interact.performed += Interact_performed;
        playerInputs.masterControls.BaseControls.Interact.canceled += Interact_canceled;
    }

    private void Interact_canceled(InputAction.CallbackContext obj)
    {
        if (currentInteraction != null)
        {
            currentInteraction.ButtonReleased();
        }
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if (currentInteraction != null)
        {
            currentInteraction.ButtonReleased();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastCheck();
    }

    private void RaycastCheck()
    {
        RaycastHit hit;
    }

    private class InteractableButton
    {
        internal void ButtonReleased()
        {
            throw new NotImplementedException();
        }
    }
    
    */
}
