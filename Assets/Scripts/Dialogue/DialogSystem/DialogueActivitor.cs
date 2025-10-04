using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.UI;


public class DialogueActivator : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [SerializeField] private DialogueObject[] dialogueObject;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image characterImage;
    [SerializeField] private static int currentDialogueIndex;
    [SerializeField] private DialogueProgression progression;
    [SerializeField] private ResponseHandle response;

    public bool showLovyUI;

    private DialogueUI dialogueUI;

/*    public void UpdateDialogueObject(DialogueObject dialogueObject, int index)
    {
        if (index >= 0 && index < dialogueObject.Dialogue.Length)
        {
            this.dialogueObject[index] = dialogueObject;
        }
    }*/


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

        currentDialogueIndex = progression.currentDialogueIndex;
    }

    private IEnumerator Start()
    {
        // Wait one frame to ensure DialogueUI has run its Start() and initialized all components
        yield return null;

/*        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            dialogueUI.AddResponseEvenet(responseEvents.Events);
            break;
        }*/

        if (dialogueUI != null && dialogueObject.Length > 0 && dialogueObject[0] != null)
        {

            DialogueObject currentDialogue = dialogueObject[progression.currentDialogueIndex];
            dialogueUI.ShowDialogue(currentDialogue);

            nameText = GameObject.Find("NameTxt").GetComponent<TMP_Text>();
            characterImage = GameObject.Find("CharacterImage").GetComponent<Image>();

            if (currentDialogue.Dialogue.Length > 0)
            {
                nameText.text = currentDialogue.Dialogue[0].CharacterName;
                characterImage.sprite = currentDialogue.Dialogue[0].CharacterImage;
            }

            if (currentDialogueIndex < dialogueObject.Length - 1)
            {
                progression.currentDialogueIndex++;
            }
            else if(currentDialogueIndex == dialogueObject.Length - 1)
            {
                response.ResetLovyCount();
            }
                
        }
    }

    public void ResetDialogueIndex()
    {
        progression.currentDialogueIndex = 0;
    }
}
