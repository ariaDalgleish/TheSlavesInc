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
    private const float timeLimit = 300f;  // 5 minutes in seconds
    private bool isLoadingScene = false;   // Prevent multiple loads

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;  // Sync scenes across all clients
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Only MasterClient controls the scene load when the timer reaches the limit in Stage 1
        if (SceneManager.GetActiveScene().name != "Stage2" && PhotonNetwork.IsMasterClient && timer >= timeLimit && !isLoadingScene)
        {
            isLoadingScene = true;  // Set the loading flag
            StartCoroutine(WaitAndLoadScoreScene());  // Load the Score Scene
            return;  // Ensure no further execution
        }

        // Stage 2: Time reaches 0, load GameOver scene after 3 seconds
        if (SceneManager.GetActiveScene().name == "Stage2" && timer >= timeLimit && !isLoadingScene && PhotonNetwork.IsMasterClient)
        {
            isLoadingScene = true;  // Prevent multiple loads
            StartCoroutine(WaitAndLoadGameOverScene());
            return;  // Ensure no further execution
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
                StartCoroutine(WaitAndLoadNextScene());  // Load Stage 2
            }
        }
    }

    // Coroutine to wait and load the GameOver Scene in Stage 2
    private IEnumerator WaitAndLoadGameOverScene()
    {
        PhotonNetwork.IsMessageQueueRunning = false;  // Pause network messages while loading

        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);  // Wait for the transition to finish
        }

        yield return new WaitForSeconds(3f);  // Delay before loading GameOver scene
        PhotonNetwork.LoadLevel("GameOver");  // Load GameOver scene
    }

    // Coroutine to wait and load the Score Scene when time is up in Stage 1
    private IEnumerator WaitAndLoadScoreScene()
    {
        PhotonNetwork.IsMessageQueueRunning = false;  // Pause network messages while loading

        // Play transition animation if there is one
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);  // Wait for the transition to complete
        }

        yield return new WaitForSeconds(1f);  // Add a brief delay before loading the ScoreScene
        PhotonNetwork.LoadLevel("ScoreScene");  // MasterClient loads, and all other players sync automatically
    }

    // Coroutine to wait and load the next stage (Stage 2)
    public IEnumerator WaitAndLoadNextScene()
    {
        PhotonNetwork.IsMessageQueueRunning = false;  // Pause network messages while loading

        foreach (var enemy in FindObjectsOfType<EnemyMovement>())
        {
            Destroy(enemy.gameObject);
        }

        yield return new WaitForSeconds(3f);  // Delay before loading Stage 2
        PhotonNetwork.LoadLevel("Stage2");  // MasterClient loads Stage 2
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
