using Photon.Pun;
using UnityEngine;

public class ADlayerMovement : MonoBehaviour
{
    /* This script is responsible for the movement of the player. It reads the input from the player input script and moves the player accordingly. 
     * 
    PhotonView photonView;
    PlayerInputControls playerInputs;
    Rigidbody rb;
    Vector2 movement;
    [SerializeField] Transform visuals;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    float rotY;



    // Start is called before the first frame update
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            rb = GetComponent<Rigidbody>();
            playerInputs = GetComponent<PlayerInputControls>();
        }
        else
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement = playerInputs.masterControls.BaseControls.Movement.ReadValue<Vector2>(); // Read the movement input from the player input script and store it in the movement variable 
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = visuals.forward * movement.y * Time.deltaTime* speed *100; // Move the player forward and backward
        moveDirection.y = rb.velocity.y; // Keep the player grounded

        rotY += movement.x * Time.deltaTime * rotationSpeed * 100; // Rotate the player left and right

        rb.velocity = moveDirection; // Apply the movement to the player
        visuals.localEulerAngles = new Vector3(0, rotY, 0); // Apply the rotation to the player
    }
    */
}
