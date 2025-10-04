using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResponseHandle : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    private int LovyPlus;

    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;
    private LovyCounting LovyCounting;

    private List<GameObject> tempResponseButton = new List<GameObject>();

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
            LovyPlus++;
            Debug.Log("LovyPlus = " + LovyPlus);
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
}
