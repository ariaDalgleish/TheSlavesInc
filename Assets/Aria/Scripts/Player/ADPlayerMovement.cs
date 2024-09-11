using Photon.Pun;
using System.Collections;
using Unity.VisualScripting;
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
    [SerializeField] private TrailRenderer tr;

    public bool canMove = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashDuration = 0.2f;
    private float dashCooldown = 2f;
    private Vector3 moveDirection;
    private float currentSpeed;
    private float slowdownEndTime;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
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
        if (!canMove || isDashing) return;  // Stop movement if canMove is false or player is dashing

        movement = playerControls.BaseControls.BaseControls.Movement.ReadValue<Vector2>(); // Get the movement input

        if (Time.time > slowdownEndTime)
        {
            ResetSpeed();
        }

        if (playerControls.BaseControls.BaseControls.Dash.triggered && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    

    private void FixedUpdate()
    {
        if (!canMove || isDashing) return;  // Stop movement if canMove is false or player is dashing

        moveDirection = new Vector3(movement.x, 0, movement.y).normalized; // Normalize the movement vector

        Vector3 movementVelocity = moveDirection * currentSpeed * Time.deltaTime * 100; // Calculate the movement velocity
        movementVelocity.y = rb.velocity.y; // Preserve the y velocity

        if (moveDirection != Vector3.zero) // Rotate the player to face the movement direction
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            visuals.rotation = Quaternion.Lerp(visuals.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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

    private IEnumerator Dash() 
    {
        canDash = false;
        isDashing = true;

        Vector3 dashDirection = moveDirection != Vector3.zero ? moveDirection : visuals.forward; 
        
        rb.velocity = dashDirection * dashingPower;
        tr.emitting = true;

        yield return new WaitForSeconds(dashDuration);
        tr.emitting = false;

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}