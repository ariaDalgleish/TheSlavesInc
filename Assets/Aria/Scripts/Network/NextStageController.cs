using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using TMPro;

public class NextStageController : MonoBehaviour
{
    PhotonView photonView;

    [SerializeField] float timeToStart;
    float timerToStart;
    bool readyToStart;

    [SerializeField] GameObject startButton;
    [SerializeField] TextMeshProUGUI countDownDisplay;
    [SerializeField] GameObject waitingText;
    [SerializeField] GameObject quitButton;

    [SerializeField] int nextLevel;


    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        timerToStart = timeToStart;
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    private void Update()
    {
               
        // for all players
            if (readyToStart)
            {
                
                timerToStart -= Time.deltaTime;
                waitingText.gameObject.SetActive(false);
                quitButton.gameObject.SetActive(false);
                countDownDisplay.text = ((int)timerToStart).ToString(); // show in seconds in a string
            }
            else
            {
                // reset timer
                timerToStart = timeToStart;
                countDownDisplay.text = ""; // empty string to hide 
            }

        if (PhotonNetwork.IsMasterClient)
        {
            // only master client checks timer <= 0
            // only master is loading the players into the game scene 

            if (timerToStart <= 0) // timer finish
            {

                timerToStart = 100; // only gets run through once
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.LoadLevel(nextLevel);
            }
        }
    }
      

    public void Play()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            
            photonView.RPC("RPC_Play", RpcTarget.All);
        }
    }

    // countdown for all players
    [PunRPC]
    void RPC_Play()
    {
        // toggle bool
        readyToStart = !readyToStart;
    }


    public void LeaveGame()
    {
        Debug.Log("GameLeft");
        Application.Quit();
    }
}
