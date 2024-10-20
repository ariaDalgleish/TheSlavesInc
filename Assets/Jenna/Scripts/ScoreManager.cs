using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public static ScoreManager Instance;

    private int teamScore = 0;
    private int leftoverDurabilityScore = 0;

    public DurabilitySystem durabilitySystem; // Reference to the durability system
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPuzzleSolvedScore()
    {
        // Add 10 points when a puzzle is solved
        photonView.RPC("UpdateTeamScore", RpcTarget.All, 10);
    }

    [PunRPC]
    public void UpdateTeamScore(int scoreToAdd)
    {
        teamScore += scoreToAdd;
        Debug.Log("Team score updated: " + teamScore);
    }

    public void CalculateFinalScore()
    {
        // Calculate final score when time is up
        leftoverDurabilityScore = (int)durabilitySystem.currentDurability;
        int finalScore = teamScore + leftoverDurabilityScore;
        Debug.Log("Final team score: " + finalScore);

        if (scoreText != null)
        {
            scoreText.text = "Your Score: " + finalScore.ToString();
        }
    }
}
