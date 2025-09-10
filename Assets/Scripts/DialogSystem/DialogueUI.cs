using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    //[SerializeField] private DialogueObject testDialogue;

    private ResponseHandle responseHandle;
    private TextEffect textEffect;
    public bool isTalking { get; private set; }


    private void Start()
    {
        textEffect = GetComponent<TextEffect>();
        responseHandle = GetComponent<ResponseHandle>();

        if (textEffect == null) Debug.LogError("TextEffect component is missing!");
        if (responseHandle == null) Debug.LogError("ResponseHandle component is missing!");
        if (textLabel == null) Debug.LogError("TextLabel is not assigned!");
        if (dialogueBox == null) Debug.LogError("DialogueBox is not assigned!");

        CloseDialogueBox();
    }


    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isTalking = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvenet(ResponseEvent[] responseEvent)
    {
        responseHandle.AddRespnoseEvents(responseEvent);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        //yield return new WaitForSeconds(2);

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return textEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;


            //Next dialogue by pressing space key
            yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
        }

        if (dialogueObject.HasResponses)
        {
            responseHandle.ShowResponses(dialogueObject.Responses);
        }
        else 
        {
            CloseDialogueBox();
        }

    }

    //Close Dialogue
    public void CloseDialogueBox()
    {
        isTalking = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
