using UnityEngine;

[System.Serializable]
public class PlayerData 
{
   
    public string playerName; // Stores player name
    public int playerID; // Stores player ID
    public int characterIndex; // Stores characters choice

    public void SetPlayerName(string n)
    {
        playerName = n;
    }

    public void SetPlayerID(int n) 
    {
        playerID = n;
    }

    public void SetCharacter(int index)
    {
        characterIndex = index;
    }
    
}
