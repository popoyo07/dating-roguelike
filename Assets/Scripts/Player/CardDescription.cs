using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class CardDescription : MonoBehaviour
{
    public AssignCard assignCard;
    public TextMeshProUGUI txt;
    DeckDraw deckDraw;
    bool runing;
  
    void OnEnable()
    {
        if (assignCard == null)
            assignCard = GetComponent<AssignCard>();

        if (deckDraw == null)
            deckDraw = GameObject.Find("CardManager").GetComponent<DeckDraw>();
        
        if (txt == null)
            txt = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(SetText());

    }
    void Update()
    {
        if (assignCard != null  && txt.gameObject.activeSelf)
        {
            if (assignCard.cardUsed || assignCard.resetForNewTurn)
            StartCoroutine(DisableTxt());
           // Debug.LogWarning("Disable the text");
        }
        StartCoroutine(SetText());

    }
    IEnumerator DisableTxt()
    {
        yield return new WaitForSeconds(.5f);
        txt.text = "";
        txt.gameObject.SetActive(false);

    }
    IEnumerator SetText()
    {
        yield return new WaitUntil(() => 
        !string.IsNullOrEmpty(assignCard.cardNameFromList) &&
        deckDraw.allCardDescriptions.ContainsKey(assignCard.cardNameFromList)
        );
   
        if (assignCard.cardNameFromList != null)
        {
            if (txt.text != deckDraw.allCardDescriptions[assignCard.cardNameFromList])
            {
                txt.text = deckDraw.allCardDescriptions[assignCard.cardNameFromList];

            }
        }
      

    }
 
}
