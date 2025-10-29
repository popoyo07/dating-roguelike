using UnityEngine;
using TMPro;
using System.Collections;

public class CardDescription : MonoBehaviour
{
    AssignCard assignCard;
    TextMeshProUGUI txt;
    DeckDraw deckDraw;
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

    IEnumerator SetText()
    {
        yield return new WaitUntil(() => 
        !string.IsNullOrEmpty(assignCard.cardNameFromList) &&
        deckDraw.allCardDescriptions.ContainsKey(assignCard.cardNameFromList)
        );
    
        txt.text = deckDraw.allCardDescriptions[assignCard.cardNameFromList];

    }
}
