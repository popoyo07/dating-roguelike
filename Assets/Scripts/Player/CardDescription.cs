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
        if (assignCard != null && !assignCard.cardImage.isActiveAndEnabled && txt.gameObject.activeSelf)
        {
            txt.gameObject.SetActive(false);
            Debug.LogWarning("Disable the text");
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
 
}
