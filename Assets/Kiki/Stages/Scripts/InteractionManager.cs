using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public float detectionRange = 5f;
    public string interactionMessage = "Press E to interact";
    public Transform playerTransform; // Reference to the player GameObject

    private bool isPlayerInRange;

    void Start()
    {
        interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        Debug.Log("Distance to player: " + distance);

        if (distance <= detectionRange)
        {
            if (!isPlayerInRange)
            {
                ShowInteractionText();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                HideInteractionText();
            }
        }
    }

    void ShowInteractionText()
    {
        interactionText.gameObject.SetActive(true);
        interactionText.text = interactionMessage;
        isPlayerInRange = true;
    }

    void HideInteractionText()
    {
        interactionText.gameObject.SetActive(false);
        isPlayerInRange = false;
    }

    void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
