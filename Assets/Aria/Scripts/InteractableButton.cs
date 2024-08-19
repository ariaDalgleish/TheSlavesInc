using Photon.Pun;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    PhotonView photonView;
    public bool holdingButton;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
               
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
