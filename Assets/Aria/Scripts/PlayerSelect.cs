using Photon.Pun;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
