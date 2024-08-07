using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTest : MonoBehaviour
{
    public int playerID;
    public MultiplayerInputManager inputManager;
    public Vector2 movement;

    InputControls inputControls;

    // Start is called before the first frame update
    void Start()
    {
        inputManager.onPlayerJoined += AssignInputs;
    }

    private void OnDisable()
    {
        if (inputManager != null)
        {
            inputControls.MasterActions.Movement.performed -= MovementPerformed;
            inputControls.MasterActions.Movement.canceled -= MovementCanceled;
        }
        else
        {
            inputManager.onPlayerJoined -= AssignInputs;
        }
    }

    void AssignInputs(int ID)
    {
        if(playerID == ID)
        {
            inputManager.onPlayerJoined -= AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.MasterActions.Movement.performed += MovementPerformed;
            inputControls.MasterActions.Movement.canceled += MovementPerformed;
        }
    }

    private void MovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        movement = Vector2.zero;
    }

    private void MovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        movement = obj.ReadValue<Vector2>();
    }

}
