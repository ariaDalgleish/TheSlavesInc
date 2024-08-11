using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target, player;
    public float moveSpeed;
    public Vector3 offset;
    public float followDistance;
    public Quaternion rotation;

    /*
    public Transform Obstruction;
    float zoomSpeed = 2f;
    */

    public float teleDistanceThreshold = 100f;

    private void Start()
    { 
        //Obstruction = Target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen

    }


    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > teleDistanceThreshold)
        {
            transform.position = player.position + offset + -transform.forward * followDistance;
        }
        else
        {
            Vector3 pos = Vector3.Lerp(transform.position, player.position + offset + -transform.forward * followDistance, moveSpeed * Time.deltaTime);
            transform.position = pos;
        }

        transform.rotation = rotation;
    }

    /*
    private void LateUpdate()
    {
        ViewObstructed();
    }

    
    void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                
                if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (Vector3.Distance(transform.position, Target.position) < 4.5f)
                {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
               
            }
        }
    }
    */
}
