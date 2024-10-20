using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerControllerManager : MonoBehaviour
{
    PhotonView photonView;
    GameObject controller;

    Player[] allPlayers; // Photon.Realtime
    int myNumberInRoom; // Figure out player number in room
    public GameObject[] playerSkins; // 用于存储皮肤 prefab
    private List<int> usedIndices = new List<int>(); // 用于跟踪已分配的皮肤索引

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        allPlayers = PhotonNetwork.PlayerList; // 获取房间中所有连接玩家的数组

        foreach (Player p in allPlayers) // 遍历所有玩家
        {
            if (p != PhotonNetwork.LocalPlayer)
            {
                myNumberInRoom++;
            }
        }

        // 初始化皮肤数组
        playerSkins = new GameObject[4]; 
        playerSkins[0] = Resources.Load<GameObject>("PhotonPrefabs/AriaPlayer");
        playerSkins[1] = Resources.Load<GameObject>("PhotonPrefabs/AriaPlayer 1");
        playerSkins[2] = Resources.Load<GameObject>("PhotonPrefabs/AriaPlayer 2");
        playerSkins[3] = Resources.Load<GameObject>("PhotonPrefabs/AriaPlayer 3"); 
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        AssignPlayerToSpawnArea();
    }

    void AssignPlayerToSpawnArea()
    {
        GameObject spawnArea = GameObject.Find("SpawnArea");

        if (spawnArea == null)
        {
            Debug.LogError("Spawn area not found");
            return;
        }

        Transform spawnPoint = spawnArea.transform.GetChild(Random.Range(0, spawnArea.transform.childCount));

        if (spawnPoint != null)
        {
            // 随机选择皮肤
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, playerSkins.Length);
            } while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);
            GameObject selectedSkin = playerSkins[randomIndex];

            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", selectedSkin.name), spawnPoint.position, spawnPoint.rotation, 0, new object[] { photonView.ViewID });
            Debug.Log("Instantiated Player Controller at spawn point with skin: " + selectedSkin.name);
        }
    }
}
