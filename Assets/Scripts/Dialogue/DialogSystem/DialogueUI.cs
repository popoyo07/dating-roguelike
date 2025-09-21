using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private GameObject CardUI;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image characterImage;



    //[SerializeField] private DialogueObject testDialogue;

    private ResponseHandle responseHandle;
    private TextEffect textEffect;
    public bool isTalking;
    public bool isTalkingTake2;

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
    public IEnumerator DelayDisable(float i)
    {
        yield return new WaitForSeconds(i);
        //Debug.LogWarning("Delaying Card UI");
        CardUI.SetActive(false);
    }

    public IEnumerator DelayAble(float i)
    {
        yield return new WaitForSeconds(i);
     //   Debug.LogWarning("Assigning Card UI");

        CardUI.SetActive(true);
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
        yield return new WaitForSeconds(2);

        Color c = characterImage.color;
        string lastSpeaker = null;
        Sprite lastSprit = null;

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            Dialogue currentDialogue = dialogueObject.Dialogue[i];

            if (lastSpeaker != currentDialogue.CharacterName)
            {
                nameText.text = currentDialogue.CharacterName;
                lastSpeaker = currentDialogue.CharacterName;
            }

            if (lastSprit != currentDialogue.CharacterImage)
            {
                characterImage.sprite = currentDialogue.CharacterImage;
                lastSprit = currentDialogue.CharacterImage;

            }

            if (currentDialogue.CharacterImage != null)
            {
                c.a = 1f;
            }
            else
            {
                c.a = 0f;
            }
            characterImage.color = c;

            if (currentDialogue.DialogueEnd == true)
            {
                isTalkingTake2 = currentDialogue.DialogueEnd;
                Debug.Log("Current talking End:" + isTalkingTake2);
            }
            else
            {
                isTalkingTake2 = false;
                Debug.Log("Current talking End:" + isTalkingTake2);

            }


            yield return textEffect.Run(currentDialogue.DialogueText, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses)
                break;

            // Next dialogue by pressing space key or tapping
            yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame || Input.touchCount > 0);
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
