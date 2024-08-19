using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class ADPlayerInputControls : MonoBehaviour
{
    // This script is responsible for the player input. It reads the input from the player and sends it to the player movement script.

    PhotonView photonView;
    public PlayerControls BaseControls;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            BaseControls = new PlayerControls();
            BaseControls.Enable();
        }
        else
        {
            enabled = false;
        }
    }
}
