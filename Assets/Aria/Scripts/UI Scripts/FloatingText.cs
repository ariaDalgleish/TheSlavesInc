using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    Transform mainCam;
    Transform unit;
    Transform worldSpaceCanvas;

    public Vector3 offset;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    mainCam = Camera.main.transform;
    //    unit = transform.parent;
    //    worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").transform; // Find the world space canvas

    //    transform.SetParent(worldSpaceCanvas);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.rotation = Quaternion.LookRotation(transform.position - mainCam.position); // Make the text face the camera
    //    transform.position = unit.position + offset; // Set the position of the text to the unit's position
    //}
}
