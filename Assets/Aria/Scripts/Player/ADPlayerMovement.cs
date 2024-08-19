using Photon.Pun;
using UnityEngine;

public class ADPlayerMovement : MonoBehaviour
{
    PhotonView photonView;
    ADPlayerInputControls playerControls;
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] Transform visuals;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    float rotY;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            rb = GetComponent<Rigidbody>();
            playerControls = GetComponent<ADPlayerInputControls>();
        }
        else
        {
            enabled = false;
        }
    }

    void Update()
    {
        movement = playerControls.BaseControls.BaseControls.Movement.ReadValue<Vector2>(); // Ensure you reference the correct action name
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = visuals.forward * movement.y * Time.deltaTime * speed * 100; // Move the player forward and backward
        moveDirection.y = rb.velocity.y; // Keep the player grounded

        rotY += movement.x * Time.deltaTime * rotationSpeed * 100; // Rotate the player left and right

        rb.velocity = moveDirection; // Apply the movement to the player
        visuals.localEulerAngles = new Vector3(0, rotY, 0); // Apply the rotation to the player
    }
}
