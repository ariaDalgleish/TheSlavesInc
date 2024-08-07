using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[System.Serializable]

public class IndividualPlayerControls 
{
    public int playerID;
    public InputDevice inputDevice;
    InputUser inputUser;

    public InputControls playerControls;
    public ControllerType controllerType;

    public enum ControllerType
    {
        Keyboard,
        PlayStation,
        Xbox,
        Switch
    }

    public void SetupPlayer(InputAction.CallbackContext obj,int ID)
    {
        playerID = ID;
        inputDevice = obj.control.device;

        playerControls = new InputControls();

        inputUser = InputUser.PerformPairingWithDevice(inputDevice);
        inputUser.AssociateActionsWithUser(playerControls);

        playerControls.Enable();
        SetControllerType();
    }

    void SetControllerType()
    {
        if(inputDevice is UnityEngine.InputSystem.DualShock.DualShockGamepad)
        {
            controllerType = ControllerType.PlayStation;
        }
        else if(inputDevice is UnityEngine.InputSystem.XInput.XInputControllerWindows)
        {
            controllerType = ControllerType.Xbox;
        }
        else if (inputDevice is UnityEngine.InputSystem.Switch.SwitchProControllerHID)
        {
            controllerType = ControllerType.Switch;
        }
        else if (inputDevice is UnityEngine.InputSystem.Keyboard)
        {
            controllerType = ControllerType.Keyboard;
        }
    }

    public void DisableControls()
    {
        playerControls.Disable();
    }
    
}
