//This script is used as an example of how to recieve inputs from a single device
using Photon.Pun;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    /*
    //PhotonView photonView;

     [SerializeField] int playerID; //This is the playerID that it will search for

     //Below references are just to visualise inputs
     [SerializeField] GameObject waitingGrp; //GameObject grp to show that its waiting for a player
     [SerializeField] GameObject inputGrp; //GameObject grp to visualise inputs once a player has connected

     [SerializeField] GameObject northButton;
     [SerializeField] GameObject southButton;
     [SerializeField] GameObject westButton;
     [SerializeField] GameObject eastButton;
     [SerializeField] Transform analogStick;
     public Vector2 analogInput;

     InputControls playerControls;

     private void Start()
     {
        /*
        photonView = GetComponent<PhotonView>(); // Getting the photon view component
        if (photonView.IsMine)
        {
            
        }
        else
        {
               enabled = false;
        }
        

        InputManager.instance.onPlayerJoined += AssignInputs;
        //At the start we will create a callback using the delegate within the InputManager script
        //This will check for the PlayerJoin input to be pressed

    }

     private void OnDisable()
     {
         //Cleaning up :)
         InputManager.instance.onPlayerJoined -= AssignInputs;

         if (playerControls != null)
         {
             playerControls.MasterActions.NorthButton.performed -= NorthButton_performed;
             playerControls.MasterActions.NorthButton.canceled -= NorthButton_canceled;
             playerControls.MasterActions.SouthButton.performed -= SouthButton_performed;
             playerControls.MasterActions.SouthButton.canceled -= SouthButton_canceled;
             playerControls.MasterActions.EastButton.performed -= EastButton_performed;
             playerControls.MasterActions.EastButton.canceled -= EastButton_canceled;
             playerControls.MasterActions.WestButton.performed -= WestButton_performed;
             playerControls.MasterActions.WestButton.canceled -= WestButton_canceled;
             playerControls.MasterActions.Movement.performed -= Movement_performed;
             playerControls.MasterActions.Movement.canceled -= Movement_canceled;
         }
     }

     //This function will be called if the Joined Button has been detected from any device
     void AssignInputs(int ID)
     {
         //First we check if the player id matches the ID of the device that has been pressed
         if (playerID == ID)
         {
             //If it matches, we can move the callback since the player has been found
             InputManager.instance.onPlayerJoined -= AssignInputs;

             //Now we can grab the inputs and assign them to this script
             playerControls = InputManager.instance.players[playerID].playerControls;

             playerControls.MasterActions.NorthButton.performed += NorthButton_performed;
             playerControls.MasterActions.NorthButton.canceled += NorthButton_canceled;

             playerControls.MasterActions.SouthButton.performed += SouthButton_performed;
             playerControls.MasterActions.SouthButton.canceled += SouthButton_canceled;

             playerControls.MasterActions.EastButton.performed += EastButton_performed;
             playerControls.MasterActions.EastButton.canceled += EastButton_canceled;

             playerControls.MasterActions.WestButton.performed += WestButton_performed;
             playerControls.MasterActions.WestButton.canceled += WestButton_canceled;

             playerControls.MasterActions.Movement.performed += Movement_performed;
             playerControls.MasterActions.Movement.canceled += Movement_canceled;

             waitingGrp.SetActive(false);
             inputGrp.SetActive(true);
         }
     }

*/
}