/*using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControls : MonoBehaviour
{
    PhotonView photonView;
    public MasterActions masterActions;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            masterActions = new MasterActions();
            masterActions.Enable();
        }
        else
        {
            enabled = false;
        }
       
    }
}
*/