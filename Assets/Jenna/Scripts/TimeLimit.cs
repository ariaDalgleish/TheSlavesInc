using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class TimeLimit : MonoBehaviourPunCallbacks, IPunObservable
{
    PhotonView photonView;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;  // 300f = 5-minute timer
    public GameObject nextStageScreen;


    public DurabilitySystem durabilitySystem;
    private bool isDurabilityStarted = false;
    /*  private bool isSceneLoadTriggered = false;*/  // Prevent multiple scene loads

    void Start()
    {
        durabilitySystem = FindObjectOfType<DurabilitySystem>();

        if (PhotonNetwork.IsMasterClient)
        {
            remainingTime = 15f;  // Initialize timer
            durabilitySystem.enabled = false;
        }

        PhotonNetwork.AutomaticallySyncScene = true;  // Ensure all players sync scenes
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (remainingTime > 0)
            {
                
                remainingTime -= Time.deltaTime;

                // Start durability when timer starts
                if (remainingTime < 300f && !durabilitySystem.isDecreasing && !isDurabilityStarted)
                {
                    durabilitySystem.StartDecreasingDurability();
                    isDurabilityStarted = true;
                }



            }
            
        }
        
        if (remainingTime <= 0)
        {
            remainingTime = 0;

            durabilitySystem.StopDecreasingDurability();
            nextStageScreen.SetActive(true);

            DisablePlayerMovement();

        }

        // Update the timer UI for all players
        UpdateTimerUI();
    }
    private void DisablePlayerMovement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            ADPlayerMovement movementScript = player.GetComponent<ADPlayerMovement>();
            if (movementScript != null)
            {
                movementScript.canMove = false; // Disable player movement
            }
        }
    }

    // Scene transition logic based on the current scene
    //private void HandleSceneLoading()
    //{
    //    string currentScene = SceneManager.GetActiveScene().name;

    //    if (currentScene == "Stage1")
    //    {
    //        Debug.Log("Time's up in Stage 1. Loading ScoreScene.");
    //        photonView.RPC("LoadScoreSceneForAll", RpcTarget.All);
    //    }
    //    else if (currentScene == "Stage2")
    //    {
    //        Debug.Log("Time's up in Stage 2. Loading GameOver scene.");
    //        photonView.RPC("LoadGameOverSceneForAll", RpcTarget.All);
    //    }
    //}

    // Update timer display
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 5)
        {
            timerText.color = Color.red;
        }
       
    }

    // Sync the timer across the network
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(remainingTime);  // MasterClient sends timer
        }
        else
        {
            remainingTime = (float)stream.ReceiveNext();  // Other clients receive timer
        }
    }

    // RPC to load the ScoreScene for all players
    //[PunRPC]
    //void LoadScoreSceneForAll()
    //{
    //    if (SceneManager.GetActiveScene().name != "ScoreScene")  // Ensure it doesn't get called again in the ScoreScene
    //    {
    //        Debug.Log("Loading ScoreScene for all players.");
    //        PhotonNetwork.LoadLevel("ScoreScene");
    //    }
    //}

    // RPC to load the GameOver scene for all players
    //[PunRPC]
    //void LoadGameOverSceneForAll()
    //{
    //    if (SceneManager.GetActiveScene().name != "GameOver")  // Ensure it doesn't get called again in the GameOver scene
    //    {
    //        Debug.Log("Loading GameOver scene for all players.");
    //        PhotonNetwork.LoadLevel("GameOver");
    //    }
    //}
}
