using Photon.Pun;
using UnityEngine;

public class InteractableButton : MonoBehaviour, IInteractable
{
    PhotonView photonView;
    public bool holdingButton;  // Keep this for elevator checks

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // This will be triggered when the player interacts (e.g., presses the button)
    public void OnInteract()
    {
        ButtonPressed();
    }

    // This will be triggered when the player stops interacting (e.g., releases the button)
    public void OnStopInteract()
    {
        ButtonReleased();
    }

    public void ButtonPressed()
    {
        holdingButton = true;
        photonView.RPC("RecieveButtonPressed", RpcTarget.Others);
    }

    public void ButtonReleased()
    {
        holdingButton = false;
        photonView.RPC("RecieveButtonReleased", RpcTarget.Others);
    }

    [PunRPC]
    public void RecieveButtonPressed()
    {
        holdingButton = true;
    }

    [PunRPC]
    public void RecieveButtonReleased()
    {
        holdingButton = false;
    }
}
