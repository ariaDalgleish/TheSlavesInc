using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DoorController : MonoBehaviour
{
    public Transform doorTransform;
    public Vector3 doorOpenPosition;
    public Vector3 doorClosedPosition;
    public float moveSpeed = 2f;
    PhotonView photonView;

    public void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void OpenDoor(bool open)
    {
        /*photonView.RPC("RecieveOpenDoor", RpcTarget.Others);*/
        StopAllCoroutines();
        if (open)
        {
            StartCoroutine(MoveToPosition(doorTransform, doorOpenPosition));
        }
        else
        {
            StartCoroutine(MoveToPosition(doorTransform, doorClosedPosition));
        }
    }

    private IEnumerator MoveToPosition(Transform transform, Vector3 targetposition)
    {
        while (transform.position != targetposition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetposition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    [PunRPC]
    public void ReceiveOpenDoor(bool open)
    {
        StopAllCoroutines();
        if (open)
        {
            StartCoroutine(MoveToPosition(doorTransform, doorOpenPosition));
        }
        else
        {
            StartCoroutine(MoveToPosition(doorTransform, doorClosedPosition));
        }
    }

}
