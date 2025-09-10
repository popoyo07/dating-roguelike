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
    [SerializeField] private GameObject Canvas;
    [SerializeField] private TMP_Text nameText;

    private DialogueUI dialogueUI;
    public void UpdateDialogueObject (DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void Awake()
    {
        Canvas = GameObject.Find("Canvas");


        if (Canvas != null)
        {
            dialogueUI = Canvas.GetComponent<DialogueUI>();
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
            nameText = GameObject.Find("NameTxt").GetComponent<TMP_Text>();
            Debug.Log("HappenTwice");
        }

        if (dialogueObject != null && dialogueObject.NameText.Length > 0)
        {
            nameText.text = dialogueObject.NameText;
        }
    }
}
