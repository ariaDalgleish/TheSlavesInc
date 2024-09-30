using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;  // This is needed for Photon hashtables

public class LevelLoader : MonoBehaviourPunCallbacks
{
    public Animator transition;  // Reference to the Animator controlling transition animations
    public float transitionTime = 1f;  // Time duration for the transition animation
    private bool qPressed = false;  // Track if the local player has pressed 'Q'

    void Update()
    {
        // Ensure this logic runs only in the ScoreScene
        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {
            // If the player hasn't pressed 'Q' yet and presses it now
            if (!qPressed && Input.GetKeyDown(KeyCode.Q))
            {
                qPressed = true;
                Debug.Log("Player pressed Q. Notifying others.");

                // Set custom property for the player to indicate they've pressed 'Q'
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "QPressed", true } });
            }

            // Master client checks if all players have pressed 'Q'
            if (AllPlayersPressedQ() && PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(WaitAndLoadNextScene());
            }
        }
    }

    // Coroutine to wait for 3 seconds before loading Stage 2
    public IEnumerator WaitAndLoadNextScene()
    {
        // Wait for 3 seconds before transitioning
        yield return new WaitForSeconds(3f);

        // The master client synchronizes the scene load for all players
        PhotonNetwork.LoadLevel("Stage2");  // Load Stage2 for all players
    }

    // Check if all players have pressed the 'Q' key
    private bool AllPlayersPressedQ()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            object qPressedValue;
            // If any player has not pressed 'Q' or the property is missing, return false
            if (!player.CustomProperties.TryGetValue("QPressed", out qPressedValue) || (bool)qPressedValue == false)
            {
                return false;  // Not all players have pressed 'Q'
            }
        }
        return true;  // All players have pressed 'Q'
    }

    // This method can still be used to load the ScoreScene from Stage1
    public void LoadScoreScene()
    {
        // Start loading the "ScoreScene" with a 2-second delay
        StartCoroutine(LoadLevelWithDelay("ScoreScene", 2f));
    }

    // Coroutine to load a scene after a delay, using scene name
    public IEnumerator LoadLevelWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay

        // Directly load the scene without transition
        SceneManager.LoadScene(sceneName);
    }
}
