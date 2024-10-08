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
    private bool isLoadingScene = false; // Prevent multiple loads

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeLimit && !isLoadingScene) // Check the timer and the loading flag
        {
            isLoadingScene = true; // Set loading flag
            StartCoroutine(WaitAndLoadNextScene());
            return;
        }

        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {
            if (!qPressed && Input.GetKeyDown(KeyCode.Q))
            {
                qPressed = true;
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "QPressed", true } });
            }

            if (AllPlayersPressedQ() && PhotonNetwork.IsMasterClient && !isLoadingScene)
            {
                isLoadingScene = true; // Set loading flag
                StartCoroutine(WaitAndLoadNextScene());
            }
        }
    }

    public IEnumerator WaitAndLoadNextScene()
    {
        foreach (var enemy in FindObjectsOfType<EnemyMovement>())
        {
            Destroy(enemy.gameObject);
        }

        yield return new WaitForSeconds(3f);
        PhotonNetwork.LoadLevel("Stage2");
    }

    private bool AllPlayersPressedQ()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            object qPressedValue;
            if (!player.CustomProperties.TryGetValue("QPressed", out qPressedValue) || (bool)qPressedValue == false)
            {
                return false;
            }
        }
        return true;
    }
    //// This method can still be used to load the ScoreScene from Stage1
    //public void LoadScoreScene()
    //{
    //    // Start loading the "ScoreScene" with a 2-second delay
    //    StartCoroutine(LoadLevelWithDelay("ScoreScene", 2f));
    //}

    //// Coroutine to load a scene after a delay, using scene name
    //public IEnumerator LoadLevelWithDelay(string sceneName, float delay)
    //{
    //    yield return new WaitForSeconds(delay);  // Wait for the specified delay

    //    // Directly load the scene without transition
    //    SceneManager.LoadScene(sceneName);
}
