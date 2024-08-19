// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmoothSyncMovement.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Utilities, 
// </copyright>
// <summary>
//  Smoothed out movement for network gameobjects
// </summary>                                                                                             
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun.UtilityScripts
{
    /// <summary>
    /// Smoothed out movement for network gameobjects
    /// </summary>
    [RequireComponent(typeof(PhotonView))]
    public class ADPlayerMovementSync : Photon.Pun.MonoBehaviourPun, IPunObservable
    {
        public float SmoothingDelay = 5;
        [SerializeField] Transform visuals;
        Transform elevator;
        [SerializeField] float teleportDistance;

        public void Awake()
        {
            bool observed = false;
            foreach (Component observedComponent in this.photonView.ObservedComponents)
            {
                if (observedComponent == this)
                {
                    observed = true;
                    break;
                }
            }
            if (!observed)
            {
                Debug.LogWarning(this + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used.");
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //We own this player: send the others our data
                stream.SendNext(transform.position); 
                stream.SendNext(visuals.rotation);
                stream.SendNext(elevator != null ? elevator.InverseTransformPoint(transform.position) : Vector3.zero);
            }
            else
            {
                //Network player, receive data
                correctPlayerPos = (Vector3)stream.ReceiveNext();
                correctPlayerRot = (Quaternion)stream.ReceiveNext();
                correctPlayerLocalPos = (Vector3)stream.ReceiveNext();
            }
        }

        private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
        private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
        private Vector3 correctPlayerLocalPos = Vector3.zero;

        public void Update()
        {
            if (!photonView.IsMine)
            {
                float currentDistance = Vector3.Distance(transform.position, correctPlayerPos);
                if(currentDistance >= teleportDistance)
                {
                    transform.position = correctPlayerPos;
                }
                
                //Update remote player (smooth this, this looks good, at the cost of some accuracy)
                if (elevator != null)
                {
                    /*
                    Vector3 localPos = elevator.TransformPoint(correctPlayerLocalPos);
                    localPos.x = transform.localPosition.x; // Retain the original X position
                    localPos.z = transform.localPosition.z; // Retain the original Z position
                    transform.localPosition = Vector3.Lerp(transform.localPosition, localPos, Time.deltaTime * this.SmoothingDelay);
                    */
                    transform.localPosition = Vector3.Lerp(transform.localPosition, elevator.TransformPoint(correctPlayerLocalPos), Time.deltaTime * this.SmoothingDelay);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
                }
                visuals.rotation = Quaternion.Lerp(visuals.rotation, correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
            }
        }

        [PunRPC]
        public void OnElevator(int id)
        {
            elevator = LevelManager.instance.elevators[id];
            transform.parent = elevator;
        }

        [PunRPC]
        public void OffElevator()
        {
            elevator = null;
            transform.parent = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(photonView.IsMine)
            {
                if (other.gameObject.CompareTag("Elevator"))
                {
                    elevator = other.transform;
                    int id = LevelManager.instance.GetElevatorID(elevator);
                    photonView.RPC("OnElevator", RpcTarget.Others, id);
                }
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (photonView.IsMine)
            {
                if (other.gameObject.CompareTag("Elevator"))
                {
                    photonView.RPC("OffElevator", RpcTarget.Others);
                }
            }
        }
    }
}