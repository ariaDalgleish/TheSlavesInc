using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    public Text playerUsername; // Check if using Text or TMP_Text

    Player player;

    public void SetUp(Player _player)
    {
        player = _player;
        playerUsername.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) // Destory other player list item if other player leaves
    {
        if(player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom() // Destroy own player list item when leaving
    {
        Destroy(gameObject);
    }
}
