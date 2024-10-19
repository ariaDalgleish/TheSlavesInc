using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Photon.Realtime;
using System.Linq;


public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;   
    [SerializeField] TMP_InputField roomNameInputField;
    //[SerializeField] string gameVersion;
    //private string displayName = "Loading...";

    public int PlayerID;

    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject musicMenu;
    public GameObject startButton;

    private void Awake()
    {
        // Singleton pattern to manage a single instance of Launcher
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

    private void Start()
    {
        
        Debug.Log("Connecting to Master...");
        PhotonNetwork.ConnectUsingSettings(); //Connect to the Photon server

    }

    public override void OnConnectedToMaster()
    {
        // Join the default lobby after connecting
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        //OnJoinedLobby();
        PhotonNetwork.AutomaticallySyncScene = true; //Automatically load scene for all clients
    }

    public override void OnJoinedLobby()
    {
        MenuManager.instance.OpenMenu("TitleMenu");// Make sure menu is labelled correctly so it opens the Title Menu
        musicMenu.SetActive(true);
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 10000).ToString("0000"); // Provide random Nickname to player
    }

    // Create a new room with the specified name
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }

        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.instance.OpenMenu("LoadingMenu");
    }

    public override void OnJoinedRoom()
    {
        // Setup UI for the current room and display players
        MenuManager.instance.OpenMenu("RoomMenu");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        PlayerID = PhotonNetwork.PlayerList.Length - 1;
        

        // Clear previous player list items and populate with current players
        Player[] players = PhotonNetwork.PlayerList;

        
        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < players.Count(); i++) 
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);

        }

        // if master client is true start button will be visible
        startButton.SetActive(PhotonNetwork.IsMasterClient); 
    }

    // if master client leaves room then the master client changes to the new current master client!
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string errorMessage)
    {
        // Display error message if room creation fails
        errorText.text = "Room Generation Unsuccesful" + errorMessage;
        MenuManager.instance.OpenMenu("ErrorMenu");
    }

    // Join a specified room
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.instance.OpenMenu("LoadingMenu");

        //SpawnPlayer();
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1); // Load scene 1 in build settings
    }

    public void LeaveGame()
    {
        Debug.Log("GameLeft");
        Application.Quit();
    }
    // Leave the current room
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.instance.OpenMenu("LoadingMenu");
    }

    public override void OnLeftRoom()
    {
        // Return to the title menu after leaving a room
        MenuManager.instance.OpenMenu("TitleMenu");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Update the UI with the current list of available rooms

        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }


        for(int i = 0; i < roomList.Count; i++)
        {
            // Because Photon Pun cant delete room, this removes from list disabling the room
            if (roomList[i].RemovedFromList)    
                continue; // check if room is disabled then continue
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    // When player enters room the player list item spawns
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer); // Pass on set up from PlayerListItem script
    }

    //public async void stageChange()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        photonView.RPC("SyncLoadedScene", RpcTarget.AllBuffered);
    //    }
    //}

    //private async void SyncLoadedScene()
    //{
    //    Debug.Log("Stage2 called");
    //    //Load the Addressable sccene asset bundle on non-Master Clients
    //    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(stage2)
    //}

    //private void SpawnPlayer()
    //{
    //    PhotonNetwork.Instantiate("AriaPlayer", Vector3.zero, Quaternion.identity);
    //}

    //public void SetDisplayName(string displayName)
    //{
    //    this.displayName = displayName;
    //}
}
