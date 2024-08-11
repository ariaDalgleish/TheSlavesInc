/*using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PhotonView photonView;
    PlayerInputControls playerInputs;
    Rigidbody rb;
    Vertor2 movement;
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
            playerInputs = GetComponent<PlayerInputControls>();
        }
        else
        {
            enabled = false;
        }
       

    }

    private void Update()
    {
        movement = playerInputs.masterActions.BaseControls.Movement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = visuals.forward * movement.y * Time.deltaTime * speed * 100;
        moveDirection.y = rb.velocity.y;

        rotY += movement.x * Time.deltaTime * rotationSpeed * 100;

        rb.velocity = moveDirection;
        visuals.localEulerAngles = new Vector3(0, rotY, 0);
    }
}
*/