using System.Collections;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class TimeLimit : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] TextMeshProUGUI timerText; // UI element to display the timer
    [SerializeField] float remainingTime = 10f; // Initial timer value (5 minutes)

    public DurabilitySystem durabilitySystem; // Reference to DurabilitySystem

    private bool isDurabilityStarted = false; // To ensure durability starts only once

    void Start()
    {
        durabilitySystem = FindObjectOfType<DurabilitySystem>(); // Find DurabilitySystem

        if (PhotonNetwork.IsMasterClient)
        {
            // Only the Master Client controls the timer and durability system
            remainingTime = 10f; // For example, 5 minutes
            durabilitySystem.enabled = false; // Disable durability decrease initially
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // MasterClient controls the timer countdown
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;

                // Start the durability decrease when the timer starts
                if (remainingTime < 300f && !durabilitySystem.isDecreasing)
                {
                    durabilitySystem.StartDecreasingDurability();
                    isDurabilityStarted = true;
                    Debug.Log("Timer started. Durability system activated.");
                }
            }
            else
            {
                // Time's up, stop durability and load the scene for everyone
                remainingTime = 0;
                durabilitySystem.StopDecreasingDurability();
                Debug.Log("Time's up. Loading the ScoreScene.");

                // MasterClient tells all clients to load the ScoreScene
                photonView.RPC("LoadScoreSceneForAll", RpcTarget.All);
            }
        }

        // Update the timer UI for all clients
        UpdateTimerUI();
    }

    // Update the timer display
    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Change the text color to red when time reaches 0
        if (remainingTime <= 0)
        {
            timerText.color = Color.red;
        }
    }

    // Synchronize the timer across the network
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // MasterClient sends the remaining time to other clients
            stream.SendNext(remainingTime);
        }
        else
        {
            // Clients receive the remaining time from the MasterClient
            remainingTime = (float)stream.ReceiveNext();
        }
    }

    // Use Photon RPC to tell all players to load the ScoreScene
    [PunRPC]
    void LoadScoreSceneForAll()
    {
        // Load the ScoreScene for all players after 3 seconds
        StartCoroutine(LoadSceneAfterDelay("ScoreScene", 3f));
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 3 seconds
        PhotonNetwork.LoadLevel(sceneName); // Load the specified scene for all players
    }
}
