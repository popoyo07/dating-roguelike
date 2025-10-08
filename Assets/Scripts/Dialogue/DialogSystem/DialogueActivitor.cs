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
    [SerializeField] private ResponseHandle responseHandle;

    public bool showLovyUI;

    private MenuButtons DeckUI;
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
            DeckUI = Canvas.GetComponent<MenuButtons>();
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
        responseHandle = Canvas.GetComponent<ResponseHandle>();
        
        responseHandle.SetCurrentActivator(this);

        if (dialogueUI != null && dialogueObject.Length > 0 && dialogueObject[0] != null)
        {

            DialogueObject currentDialogue = dialogueObject[progression.currentDialogueIndex];
            dialogueUI.ShowDialogue(currentDialogue);

            nameText = GameObject.Find("NameTxt").GetComponent<TMP_Text>();
            characterImage = GameObject.Find("CharacterImage").GetComponent<Image>();

            //Display Name and 2D Image
            if (currentDialogue.Dialogue.Length > 0)
            {
                nameText.text = currentDialogue.Dialogue[0].CharacterName;
                characterImage.sprite = currentDialogue.Dialogue[0].CharacterImage;
            }

/*            if (currentDialogueIndex < dialogueObject.Length - 1)
            {
                progression.currentDialogueIndex++;
            }*/


            /*            else if(currentDialogueIndex == dialogueObject.Length - 1)
                        {
                            responseHandle.ResetLovyCount();
                        }*/

        }
    }

    public void ContinueDialogue(int number)
    {
        Debug.Log("ContinueDialogue Activated");

        if (dialogueObject == null || dialogueObject.Length == 0) return;

        // Get current index
        int index = progression.currentDialogueIndex;

        if (index < dialogueObject.Length - 1)
        {
            progression.currentDialogueIndex += number;

            index = progression.currentDialogueIndex;
        }

        if (index >= dialogueObject.Length)
        {
            Debug.Log("No more dialogue left for this character.");
            return;
        }

        // Get current DialogueObject
        DialogueObject currentDialogue = dialogueObject[index];

        // Show dialogue
        dialogueUI.ShowDialogue(currentDialogue);

        // Update name and image
        if (currentDialogue.Dialogue.Length > 0)
        {
            nameText.text = currentDialogue.Dialogue[0].CharacterName;
            characterImage.sprite = currentDialogue.Dialogue[0].CharacterImage;
        }
    }

    public void ResetDialogueIndex()
    {
        progression.currentDialogueIndex = 0;
    }

    public void TriggerLovyCardSelection()
    {
        showLovyUI = true;
        Debug.Log("LovyPlus++");
        DeckUI.ShowDeck();
    }
}
