using Photon.Pun;
using System.Collections; 
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

    private Animator animator; // Animator reference

    public bool canMove = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashDuration = 0.2f;
    private float dashCooldown = 2f;
    private Vector3 moveDirection;
    private float currentSpeed;
    private float slowdownEndTime; // Track when to reset speed

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            rb = GetComponent<Rigidbody>();
            playerControls = GetComponent<ADPlayerInputControls>();
            animator = GetComponent<Animator>(); // Initialize Animator
            currentSpeed = speed;
           
        }
        else
        {
            enabled = false;
        }
    }


    private void Update()
    {
        if (!canMove || isDashing) return;

        movement = playerControls.BaseControls.BaseControls.Movement.ReadValue<Vector2>(); // Get the movement input

        // Update animator parameter for movement speed
        animator.SetFloat("MoveSpeed", movement.magnitude);

        if (Time.time > slowdownEndTime)
        {
            ResetSpeed(); // Reset speed if slowdown duration has ended
        }

        if (playerControls.BaseControls.BaseControls.Dash.triggered && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!canMove || isDashing) return;

        moveDirection = new Vector3(movement.x, 0, movement.y).normalized; // Normalize the movement vector

        Vector3 movementVelocity = moveDirection * currentSpeed * Time.deltaTime * 100; // Calculate movement velocity
        movementVelocity.y = rb.velocity.y; // Preserve y velocity

        if (moveDirection != Vector3.zero) // Rotate the player to face the movement direction
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            visuals.rotation = Quaternion.Lerp(visuals.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        rb.velocity = movementVelocity; // Apply movement velocity
    }

    public void SetSpeed(float factor)
    {
        currentSpeed = speed * factor; // Update current speed based on factor
    }

    public void ResetSpeed()
    {
        currentSpeed = speed; // Reset current speed to original speed
    }

    public void ApplySlowdown(float factor, float duration)
    {
        SetSpeed(factor); // Adjust speed based on factor
        slowdownEndTime = Time.time + duration; // Set when to reset speed
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector3 dashDirection = moveDirection != Vector3.zero ? moveDirection : visuals.forward;

        rb.velocity = dashDirection * dashingPower;

        if (tr != null)
        {
            tr.emitting = true; // Start emitting
        }

        yield return new WaitForSeconds(dashDuration);

        if (tr != null)
        {
            tr.emitting = false; // Stop emitting
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    
}