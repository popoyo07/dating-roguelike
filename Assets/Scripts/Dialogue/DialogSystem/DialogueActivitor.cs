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
    [SerializeField] public DialogueProgression progression;
    [SerializeField] private ResponseHandle responseHandle;
    [SerializeField] private SpriteRenderer enemy;
    [SerializeField] private Canvas eCanvas;
    [SerializeField] private NextAttackUI nextAttackUI;

    [Header("Ending Dialogue")]
    [SerializeField] private DialogueObject[] dialogueEnd;

    public bool showLovyUI;

    private MenuButtons DeckUI;
    private DialogueUI dialogueUI;

    private void Awake()
    {
        nextAttackUI = GameObject.Find("NextAttack").GetComponent<NextAttackUI>();
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

        if (dialogueEnd == null)
        {
            Debug.LogError("dialogueEnd not assigned to Enemy!");
        }

        enemy = transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>();
        eCanvas = transform.GetChild(0).gameObject.GetComponentInChildren<Canvas>();

        progression.phase += 1;
        currentDialogueIndex = progression.currentDialogueIndex;
    }

    private IEnumerator Start()
    {
        // Wait one frame to ensure DialogueUI has run its Start() and initialized all components
        yield return null;

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
        }
    }

    public void EndingDialogue()
    {
        Debug.Log("EndingDialogue Activated");

        int index = 0;

        DialogueObject endingDialogue = dialogueEnd[index];

        dialogueUI.ShowDialogue(endingDialogue);

        // Update name and image
        if (endingDialogue.Dialogue.Length > 0)
        {
            nameText.text = endingDialogue.Dialogue[0].CharacterName;
            characterImage.sprite = endingDialogue.Dialogue[0].CharacterImage;
        }
    }

    public void ContinueDialogue(int number, int nextArray)
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

        switch (number)
        {
            case 1:
                if (progression.currentDialogueIndex < 2)
                {
                    progression.currentDialogueIndex += nextArray;
                }
                else
                {
                    progression.currentDialogueIndex += nextArray;
                }
                break;
            case 2:
                progression.currentDialogueIndex += nextArray;
                break;
        
        }

    }

    public void enemyAble()
    {
        nextAttackUI.ShowNextAttack();
        enemy.enabled = true;
        eCanvas.enabled = true;
    }

    public void enemyDisable()
    {
        nextAttackUI.HideNextAttack();
        enemy.enabled = false;
        eCanvas.enabled = false;
    }

    public void ResetDialogueIndex()
    {
        progression.currentDialogueIndex = 0;
        progression.phase = 0;
    }

    public void TriggerLovyCardSelection()
    {
        showLovyUI = true;
        DeckUI.ShowDeck();
    }
}