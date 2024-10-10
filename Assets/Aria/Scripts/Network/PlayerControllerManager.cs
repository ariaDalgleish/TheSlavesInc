using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Linq;

public class PlayerControllerManager : MonoBehaviour
{
    PhotonView photonView;

    GameObject controller;

    Player[] allPlayers; //Photon.Realtime
    int myNumberInRoom; // Figure out player number in room

    void Awake()
     {
        photonView = GetComponent<PhotonView>();

        allPlayers = PhotonNetwork.PlayerList; // gets an array of all connected players in the room
        foreach (Player p in allPlayers) // loop goes through every player in the allPlayers array
        {
            if (p != PhotonNetwork.LocalPlayer)
            {
                myNumberInRoom++;
            }
        }
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            CreateController();

        }
    }

    void CreateController()
    {
        AssignPlayerToSpawnArea();
    }

    void AssignPlayerToSpawnArea()
    {
        GameObject spawnArea = GameObject.Find("SpawnArea");

        if (spawnArea == null)
        {
            Debug.LogError("Spawn area not found");
            return;
        }

        Transform spawnPoint = null;

        spawnPoint = spawnArea.transform.GetChild(Random.Range(0, spawnArea.transform.childCount));

        if (spawnPoint != null) 
        {
            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AriaPlayer"), spawnPoint.position, spawnPoint.rotation,0, new object[] { photonView.ViewID});
            Debug.Log("Instantiated Player Controller at spawn point");
        }

    }
}
