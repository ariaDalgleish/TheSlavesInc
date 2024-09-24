using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class TimeLimit : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public DurabilitySystem durabilitySystem; // Reference to DurabilitySystem

    private PhotonView photonView;
    private LevelLoader levelLoader; // Reference to LevelLoader

    private bool isDurabilityStarted = false; // To ensure durability starts only once

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        levelLoader = FindObjectOfType<LevelLoader>(); // Find LevelLoader in the scene
        durabilitySystem = FindObjectOfType<DurabilitySystem>(); // Find DurabilitySystem 

        if (PhotonNetwork.IsMasterClient)
        {
            // Initialize timer only on the Master Client
            remainingTime = 300f; // For example, 5 minutes
            durabilitySystem.enabled = false; // Disable durability decrease initially
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;

                if (remainingTime < 300f && !durabilitySystem.isDecreasing) // Whem timer starts, start durability decrease
                {
                    durabilitySystem.StartDecreasingDurability(); // Enable durability decrease when timer starts
                    isDurabilityStarted = true;
                    Debug.Log("Timer started. Durability system activated.");
                }
            }
            else
            {
                remainingTime = 0;
                durabilitySystem.StopDecreasingDurability(); // Stop durability when time is up
                Debug.Log("Time's up. Durability system deactivated.");

                // Ensure the MasterClient triggers scene loading
                levelLoader.LoadScoreScene();
            }
        }

        // Synchronize the display on all clients
        UpdateTimerUI();
    }

    // Update the UI for the timer, including color change
    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Change the text color to red when time reaches 0 for all clients
        if (remainingTime <= 0)
        {
            timerText.color = Color.red;
        }
    }

    // This function is called to synchronize the remainingTime across the network
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // The MasterClient writes the remaining time to the stream
            stream.SendNext(remainingTime);
        }
        else
        {
            // Other clients read the remaining time from the stream
            remainingTime = (float)stream.ReceiveNext();
        }
    }
}
