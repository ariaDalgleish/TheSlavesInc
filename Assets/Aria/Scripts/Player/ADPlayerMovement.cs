using Photon.Pun;
using UnityEngine;

public class ADPlayerMovement : MonoBehaviour
{
    PhotonView photonView;
    ADPlayerInputControls playerControls;
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] Transform visuals;
    [SerializeField] public float speed; // ÐÞ¸ÄÎª public
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask groundLayer; // Layer mask to identify the ground
    [SerializeField] Transform groundCheck; // A transform positioned at the player's feet to check for ground
    private bool groundedPlayer;
    private bool jump = false;
    private float currentSpeed;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            rb = GetComponent<Rigidbody>();
            playerControls = GetComponent<ADPlayerInputControls>();
            currentSpeed = speed; // Initialize the current speed
            //Debug.Log("Player script started. Current speed: " + currentSpeed);

        }
        else
        {
            enabled = false;
        }
    }

    void Update()
    {
        movement = playerControls.BaseControls.BaseControls.Movement.ReadValue<Vector2>(); // Get movement input

        // Check if the player is grounded
        groundedPlayer = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Detect jump input and set jump flag if grounded
        if (groundedPlayer && playerControls.BaseControls.BaseControls.Jump.triggered)
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        // Calculate movement direction based on input
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y).normalized;

        // Apply movement
        Vector3 movementVelocity = moveDirection * currentSpeed * Time.deltaTime * 100;
        movementVelocity.y = rb.velocity.y; // Retain the current vertical velocity

        // Rotate towards the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            visuals.rotation = Quaternion.Lerp(visuals.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Apply jump force if jump is true and the player is grounded
        if (jump && groundedPlayer)
        {
            movementVelocity.y = jumpForce;
            jump = false; // Reset jump flag
        }

        rb.velocity = movementVelocity; // Apply movement to the Rigidbody
    }

    public void SetSpeed(float factor)
    {
        //currentSpeed = speed * factor;
        Debug.Log("Speed Set to: " + currentSpeed + " (Factor: " + factor + ")");
    }

    public void ResetSpeed()
    {
        //currentSpeed = speed;
        Debug.Log("Speed Reset to Normal: " + currentSpeed);
    }
}
