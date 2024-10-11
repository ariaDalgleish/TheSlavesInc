using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class TimeLimit : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 300f;

    public DurabilitySystem durabilitySystem;
    private bool isDurabilityStarted = false;
    private bool isSceneLoadTriggered = false; // Flag for scene load trigger, changed the name to avoid conflicts

    void Start()
    {
        durabilitySystem = FindObjectOfType<DurabilitySystem>();

        if (PhotonNetwork.IsMasterClient)
        {
            remainingTime = 300f;
            durabilitySystem.enabled = false;
        }

        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("PhotonNetwork.AutomaticallySyncScene is set to: " + PhotonNetwork.AutomaticallySyncScene);
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;

                if (remainingTime < 300f && !durabilitySystem.isDecreasing && !isDurabilityStarted)
                {
                    durabilitySystem.StartDecreasingDurability();
                    isDurabilityStarted = true;
                    Debug.Log("Timer started. Durability system activated.");
                }
            }
            else if (!isSceneLoadTriggered)
            {
                remainingTime = 0;
                durabilitySystem.StopDecreasingDurability();
                Debug.Log("Time's up. Loading the ScoreScene.");
                isSceneLoadTriggered = true;

                Debug.Log("Current scene: " + SceneManager.GetActiveScene().name);

                if (SceneManager.GetActiveScene().name != "ScoreScene")
                {
                   
                    photonView.RPC("LoadScoreSceneForAll", RpcTarget.All);
                }
                else
                {
                    Debug.LogWarning("Already in the ScoreScene, skipping load.");
                }
            }
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 0)
        {
            timerText.color = Color.red;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(remainingTime);
        }
        else
        {
            remainingTime = (float)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void LoadScoreSceneForAll()
    {
        Debug.Log("PhotonNetwork.LoadLevel called for ScoreScene.");

        // We only set the flag after calling PhotonNetwork.LoadLevel
        PhotonNetwork.LoadLevel("ScoreScene");
    }
}
