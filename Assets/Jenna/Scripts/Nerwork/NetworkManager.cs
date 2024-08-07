using Photon.Pun;
using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    [SerializeField] string gameVersion;
    string connectionStatus;

    public int playerID;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        OnConnectToServer();
    }

    private void OnConnectToServer()
    {
        connectionStatus = "Connecting to Server";

        PhotonNetwork.GameVersion = gameVersion;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        connectionStatus = "Connected to master";
        PhotonNetwork.JoinLobby();

        connectionStatus = "Connecting to Lobby";
    }

    public override void OnJoinedLobby()
    {
        connectionStatus = "Lobby Joined";
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionStatus = "Failed to join room";
        PhotonNetwork.CreateRoom("New Room");
        connectionStatus = "Creating Room";
    }

    public override void OnJoinedRoom()
    {
        connectionStatus = "Room Joined";
        playerID = PhotonNetwork.PlayerList.Length - 1;
        connectionStatus = $"playerID : {playerID}";

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    private void OnGUI()
    {
        GUILayout.Label(connectionStatus);
    }

}
