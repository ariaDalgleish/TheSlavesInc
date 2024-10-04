using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerControllerManager : MonoBehaviour
{
    PhotonView photonView; 


    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Start()
    {
        if (photonView.IsMine)
        {
            CreateController();

        }
    }

    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AriaPlayer"), Vector3.zero, Quaternion.identity);
    }
}
