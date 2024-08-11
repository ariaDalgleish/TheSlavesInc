using UnityEngine;

public class AriaGameManager : MonoBehaviour
{
    public GameObject playerToSpawn;

    public void SpawnPlayer() 
    {
        Instantiate(playerToSpawn);
    }
}
