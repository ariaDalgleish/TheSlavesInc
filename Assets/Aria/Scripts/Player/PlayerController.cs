using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    PhotonView photonView;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jump = false;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();

        // Enable the script only if this is the local player
        if (!photonView.IsMine)
        {
            enabled = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (photonView.IsMine)
        {
            movementInput = context.ReadValue<Vector2>(); // Read the movement input from the player input script and store it in the movement variable
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (photonView.IsMine)
        {
            if (context.performed)
            {
                jump = true;
            }
            else if (context.canceled)
            {
                jump = false;
            }
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return; // Ensure this script only runs for the local player
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player
        if (jump && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jump = false; // Reset jump flag
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
