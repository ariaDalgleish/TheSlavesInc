using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LevelLoader : MonoBehaviourPunCallbacks
{
    public Animator transition;
    public float transitionTime = 1f;
    private bool qPressed = false;
    private float timer = 0f;
    private const float timeLimit = 300f; // 5 minutes in seconds
    private bool isLoadingScene = false;  // Prevent multiple loads

    void Start()
    {
        // Automatically sync the scene for all players when the MasterClient changes the scene
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Only MasterClient controls the scene load when the timer reaches the limit
        if (PhotonNetwork.IsMasterClient && timer >= timeLimit && !isLoadingScene)
        {
            isLoadingScene = true;  // Set the loading flag
            StartCoroutine(WaitAndLoadScoreScene());  // Load the Score Scene
        }

        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {
            // Check if Q was pressed and set the player's custom property
            if (!qPressed && Input.GetKeyDown(KeyCode.Q))
            {
                qPressed = true;
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "QPressed", true } });
            }

            // Only MasterClient triggers the scene load after all players press Q
            if (AllPlayersPressedQ() && PhotonNetwork.IsMasterClient && !isLoadingScene)
            {
                isLoadingScene = true;  // Set the loading flag
                StartCoroutine(WaitAndLoadNextScene()); // Load Stage 2
            }
        }
    }

    // Coroutine to wait and load the Score Scene when time is up
    private IEnumerator WaitAndLoadScoreScene()
    {
        // Stop receiving network messages while loading
        PhotonNetwork.IsMessageQueueRunning = false;

        // Play transition if necessary
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);  // Wait for the transition to finish
        }

        // MasterClient loads the Score Scene, others automatically sync
        PhotonNetwork.LoadLevel("ScoreScene");
    }

    // Coroutine to wait and load the next stage (Stage 2)
    public IEnumerator WaitAndLoadNextScene()
    {
        // Stop receiving network messages while loading
        PhotonNetwork.IsMessageQueueRunning = false;

        // Destroy enemies (if any exist)
        foreach (var enemy in FindObjectsOfType<EnemyMovement>())
        {
            Destroy(enemy.gameObject);
        }

        yield return new WaitForSeconds(3f);  // Delay before loading Stage 2
        PhotonNetwork.LoadLevel("Stage2");  // MasterClient loads, clients sync automatically
    }

    // Check if all players have pressed the Q key
    private bool AllPlayersPressedQ()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            object qPressedValue;
            if (!player.CustomProperties.TryGetValue("QPressed", out qPressedValue) || (bool)qPressedValue == false)
            {
                return false;  // If any player hasn't pressed Q, return false
            }
        }
        return true;  // All players pressed Q
    }
}
