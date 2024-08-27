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
        movement = playerControls.BaseControls.BaseControls.Movement.ReadValue<Vector2>();

        

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