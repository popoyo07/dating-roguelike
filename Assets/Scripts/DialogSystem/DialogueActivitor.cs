using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class DialogueActivator : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private string Name;

    private DialogueUI dialogueUI;
    public void UpdateDialogueObject (DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void Awake()
    {
        nameText.text = Name;

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

        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            dialogueUI.AddResponseEvenet(responseEvents.Events);
            break;
        }

        if (dialogueUI != null && dialogueObject != null)
        {
            dialogueUI.ShowDialogue(dialogueObject);
        }
    }
}
