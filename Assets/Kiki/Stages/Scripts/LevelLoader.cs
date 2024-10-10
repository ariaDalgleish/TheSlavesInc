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
        PhotonNetwork.AutomaticallySyncScene = true; // Sync scenes across all clients
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Stage 2: Time reaches 0, load GameOver scene after 3 seconds
        if (SceneManager.GetActiveScene().name == "Stage2" && timer >= timeLimit && !isLoadingScene && PhotonNetwork.IsMasterClient)
        {
            isLoadingScene = true;
            StartCoroutine(WaitAndLoadGameOverScene());
        }

        // Stage 1: Time reaches 0, load ScoreScene
        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().name != "Stage2" && timer >= timeLimit && !isLoadingScene)
        {
            isLoadingScene = true;
            StartCoroutine(WaitAndLoadScoreScene()); // Load the Score Scene
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
                isLoadingScene = true;
                StartCoroutine(WaitAndLoadNextScene()); // Load Stage 2
            }
        }
    }

    // Coroutine to wait and load the GameOver Scene in Stage 2
    private IEnumerator WaitAndLoadGameOverScene()
    {
        // Stop receiving network messages while loading
        PhotonNetwork.IsMessageQueueRunning = false;

        // Play transition if necessary
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);  // Wait for the transition to finish
        }

        yield return new WaitForSeconds(3f);  // Delay before loading GameOver scene
        PhotonNetwork.LoadLevel("GameOver");  // Load GameOver scene
    }

    // Coroutine to wait and load the Score Scene
    private IEnumerator WaitAndLoadScoreScene()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        PhotonNetwork.LoadLevel("ScoreScene");  // MasterClient loads, clients sync automatically
    }

    // Coroutine to wait and load the next stage (Stage 2)
    public IEnumerator WaitAndLoadNextScene()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        foreach (var enemy in FindObjectsOfType<EnemyMovement>())
        {
            Destroy(enemy.gameObject);
        }

        yield return new WaitForSeconds(3f);  // Delay before loading Stage 2
        PhotonNetwork.LoadLevel("Stage2");
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
