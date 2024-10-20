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
    public GameObject[] playerSkins; // ���ڴ洢Ƥ�� prefab
    private List<int> usedIndices = new List<int>(); // ���ڸ����ѷ����Ƥ������

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        allPlayers = PhotonNetwork.PlayerList; // ��ȡ����������������ҵ�����

        foreach (Player p in allPlayers) // �����������
        {
            if (p != PhotonNetwork.LocalPlayer)
            {
                myNumberInRoom++;
            }
        }

        // ��ʼ��Ƥ������
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
            // ���ѡ��Ƥ��
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
