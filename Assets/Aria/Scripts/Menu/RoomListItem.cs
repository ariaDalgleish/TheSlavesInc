using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviourPunCallbacks
{
    public Text roomNameText;

    public RoomInfo info;

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        roomNameText.text = _info.Name;
    }

    public void onClick()
    {
        Launcher.instance.JoinRoom(info);
    }
}
