using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : MonoBehaviour
{

    private float moveSpeed = 9;
    private float rotateSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertiacl = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertiacl);
       // print(direction);
        direction = direction.normalized;
        //print(direction);

        transform.position += direction * Time.deltaTime * moveSpeed;

        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
            //transform.forward = direction;
        }


    }
}
