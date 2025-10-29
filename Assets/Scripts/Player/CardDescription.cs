using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class CardDescription : MonoBehaviour
{
    AssignCard assignCard;
    public TextMeshProUGUI txt;
    DeckDraw deckDraw;
    bool runing;
    bool isItEnabled;
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
        if (assignCard != null && assignCard.cardUsed)
        {
            StartCoroutine(CardUsed());
        }
      
    }
    IEnumerator SetText()
    {
        yield return new WaitUntil(() => 
        !string.IsNullOrEmpty(assignCard.cardNameFromList) &&
        deckDraw.allCardDescriptions.ContainsKey(assignCard.cardNameFromList)
        );
    
        txt.text = deckDraw.allCardDescriptions[assignCard.cardNameFromList];

    }
   IEnumerator CardUsed()
    {
        if (runing)
        {
            yield break;
        }
        runing = true;
        yield return new WaitForSeconds(1);
       
        runing = false;
        isItEnabled = false;
        txt.gameObject.SetActive(isItEnabled);
    }
}
