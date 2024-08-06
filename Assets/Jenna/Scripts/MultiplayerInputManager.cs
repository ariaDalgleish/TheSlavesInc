using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerInputManager : MonoBehaviour
{
    public List<IndividualPlayerControls> players = new List<IndividualPlayerControls>();
    int maxPlayers = 4;

    public InputControls inputControls;

    public delegate void OnPlayerJoined(int playerID);
    public OnPlayerJoined onPlayerJoined;

    private void Awake()
    {
        InitializedInputs();
    }

    void InitializedInputs()
    {
        inputControls = new InputControls();
        inputControls.MasterActions.JoinButton.performed += JoinButtonPerformed;
        inputControls.Enable();
    }

    private void JoinButtonPerformed(InputAction.CallbackContext obj)
    {
        if(players.Count >= maxPlayers)
        {
            return;
        }

        foreach (IndividualPlayerControls player in players)
        {
            if(player.inputDevice == obj.control.device)
            {
                return;
            }
        }

        IndividualPlayerControls newPlayer = new IndividualPlayerControls();
        newPlayer.SetupPlayer(obj, players.Count);
        players.Add(newPlayer);

        if(onPlayerJoined != null)
        {
            onPlayerJoined.Invoke(newPlayer.playerID);
        }
      
    }
}
