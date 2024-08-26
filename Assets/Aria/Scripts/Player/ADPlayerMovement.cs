using Photon.Pun;
using UnityEngine;

public class ADPlayerMovement : MonoBehaviour
{
    PhotonView photonView;
    ADPlayerInputControls playerControls;
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] Transform visuals;
    [SerializeField] public float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    private bool groundedPlayer;
    private bool jump = false;
    private float currentSpeed;
    private float slowdownEndTime;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            rb = GetComponent<Rigidbody>();
            playerControls = GetComponent<ADPlayerInputControls>();
            currentSpeed = speed; // Initialize the current speed
        }
        else
        {
            enabled = false;
        }
    }

    private void Update()
    {
        movement = playerControls.BaseControls.BaseControls.Movement.ReadValue<Vector2>();

        groundedPlayer = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        if (groundedPlayer && playerControls.BaseControls.BaseControls.Jump.triggered)
        {
            jump = true;
        }

        if (Time.time > slowdownEndTime)
        {
            ResetSpeed();
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y).normalized;

        Vector3 movementVelocity = moveDirection * currentSpeed * Time.deltaTime * 100;
        movementVelocity.y = rb.velocity.y;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            visuals.rotation = Quaternion.Lerp(visuals.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        if (jump && groundedPlayer)
        {
            movementVelocity.y = jumpForce;
            jump = false;
        }

        rb.velocity = movementVelocity;
    }

    public void ApplySlowdown(float factor, float duration)
    {
        SetSpeed(factor);
        slowdownEndTime = Time.time + duration;
    }

    public void SetSpeed(float factor)
    {
        currentSpeed = speed * factor;
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}