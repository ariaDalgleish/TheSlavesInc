using System.Collections.Generic;
using UnityEngine;

public class ADGameManager : MonoBehaviour
{
    public static ADGameManager instance;  // Singleton pattern

    public List<PlayerData> players = new List<PlayerData>(); // List of players

              
    // Add methods for spawning players, setting data, etc.
    public void SetPlayer(int playerID, int characterIndex, string playerName)
    {
        players[playerID].SetPlayerName(playerName);
        players[playerID].SetCharacter(characterIndex);
        players[playerID].SetPlayerID(playerID);
    }

    public void SpawnPlayer()
    {
        // Spawn player logic here (can be expanded later)
    }
}
