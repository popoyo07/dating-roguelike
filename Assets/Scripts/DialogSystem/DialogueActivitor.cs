using UnityEngine;
using System.Collections;

public class DialogueActivator : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private GameObject dialogueCanvas;

    private DialogueUI dialogueUI;

    private void Awake()
    {
        if (dialogueCanvas != null)
        {
            dialogueUI = dialogueCanvas.GetComponent<DialogueUI>();
        }

        if (dialogueUI == null)
        {
            Debug.LogError("DialogueUI not found on the canvas!");
        }

        if (dialogueObject == null)
        {
            Debug.LogError("DialogueObject not assigned to Enemy!");
        }
    }

    private IEnumerator Start()
    {
        // Wait one frame to ensure DialogueUI has run its Start() and initialized all components
        yield return null;

        if (dialogueUI != null && dialogueObject != null)
        {
            dialogueUI.ShowDialogue(dialogueObject);
        }
    }
}
