using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResponseHandle : MonoBehaviour
{

    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    [SerializeField] private LovyCounting LovyCounting;
    
    private DialogueActivator currentActivator;

    public void SetCurrentActivator(DialogueActivator activator)
    {
        currentActivator = activator;
    }

    public DialogueActivator CurrentActivator => currentActivator;

    public int LovyPlus;
    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;

    private List<GameObject> tempResponseButton = new List<GameObject>();

    private void Awake()
    {

    }

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        LovyPlus = LovyCounting.lovyCount;

    }

    public void AddRespnoseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;

       for (int i = 0; i < responses.Length; i++) 
        {
            Response response = responses[i];
            int responseIndex = i; 
            
            // Debug.LogWarning(responseIndex);

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResonpseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));

            tempResponseButton.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);

        if (responseIndex == 0)
        {
            LovyCountAdd(1);
            if (LovyPlus > 0) 
            {
                currentActivator.TriggerLovyCardSelection();

                
                //LovyDovyCardSelectionUI.Show(OnCardSelected);
            }
        }

        foreach (GameObject button in tempResponseButton)
        {
            Destroy(button);
        }
        tempResponseButton.Clear();

        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        responseEvents = null;

        if (response.DialogueObject)
        {
            dialogueUI.ShowDialogue(response.DialogueObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }
    }

    public void ResetLovyCount() 
    {
        LovyPlus = 0;
    }

    public void LovyCountAdd(int numberAdd)
    {
        LovyPlus += numberAdd;
    }
}
