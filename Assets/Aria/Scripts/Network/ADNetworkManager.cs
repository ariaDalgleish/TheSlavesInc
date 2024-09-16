using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ADNetworkManager : MonoBehaviourPunCallbacks
{
    //[SyncVar]
    private string displayName = "Loading...";
    public ADNetworkManager instance;
    [SerializeField] string gameVersion;
    string connectionStatus;

    public int PlayerID;

    

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
        OnConnectedToServer();
    }

    private void OnConnectedToServer()
    {
        connectionStatus = "Connected to server";

        //Set the game version - only players with the same game version can play together
        PhotonNetwork.GameVersion = gameVersion;

        //Connect to the Photon server
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
        connectionStatus = "Joined Lobby";

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionStatus = "failed to join room";
        PhotonNetwork.CreateRoom("New Room");
        connectionStatus = "Creating new room";
    }

    public override void OnJoinedRoom()
    {
        connectionStatus = "Room Joined";
        PlayerID = PhotonNetwork.PlayerList.Length - 1;
        connectionStatus = $"Player ID : {PlayerID}";

        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("AriaPlayer", Vector3.zero, Quaternion.identity);
    }


    private void OnGUI()
    {
        GUILayout.Label(connectionStatus);
    }


    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

}
