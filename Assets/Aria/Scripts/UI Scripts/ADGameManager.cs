using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ADGameManager : MonoBehaviour
{
    
    
   
    public List<PlayerData> players = new List<PlayerData>(); // List of players

    public void SetPlayer(int playerID, int characterIndex, string playerName)
    {
        players[playerID].SetPlayerName(playerName);
        players[playerID].SetCharacter(characterIndex);
        players[playerID].SetPlayerID(playerID);
    }


    public void SpawnPlayer()
    {
        /*
        foreach (PlayerData player in players)
        {
           GameObject obj = Instantiate(playerPrefab, spawnPoints[player.playerID].position, Quaternion.identity);
           
        }
        */
    }
    
}
