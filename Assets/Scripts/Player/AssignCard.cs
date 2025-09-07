using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AssignCard : MonoBehaviour
{
    public string cardNameFromList; // need to find a way to randomly assign them, taking
                                    // in consideration the ones taht are in hand, in deck
                                    // and  the ones that are not on neither 
    public int numbaerOnList;

    BattleSystem BSystem;
    Button cardButton;
    CardActions cardAttks;


    void Awake()
    {
        cardAttks = GameObject.Find("CardManager").GetComponent<CardActions>();
        cardButton = GetComponent<Button>();
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        
        // adds action to onclick 
        cardButton.onClick.AddListener(() =>
        {
            if (cardAttks.cardAttaks.ContainsKey(cardNameFromList))
            {
                cardAttks.cardAttaks[cardNameFromList].Invoke(); // invoke's function from dictionary 
                cardAttks.DiscardCards(cardNameFromList);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No action found for key: " + cardNameFromList);
            }
        });

    }


    void FixedUpdate()
    {
        if (BSystem != null && BSystem.state == BattleState.PLAYERTURN) // enable button in cambat 
        {
            cardButton.enabled = true;
        } else
        {
            cardButton.enabled = false;
        }
    }
}
