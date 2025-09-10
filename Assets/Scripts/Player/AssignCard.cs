using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignCard : MonoBehaviour
{
    public string cardNameFromList; // need to find a way to randomly assign them, taking
                                    // in consideration the ones taht are in hand, in deck
                                    // and  the ones that are not on neither 
    public int numbaerOnList;
    bool displayTxt;
    BattleSystem BSystem;
    Button cardButton;
    CardActionsCharacter1 cardAttks;
    public TextMeshProUGUI txt;
    bool cardUsed;
    DeckDraw cardDraw;
    void Start()
    {
        cardButton = GetComponent<Button>();
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        cardDraw = GameObject.Find("CardManager").GetComponent<DeckDraw>();
        txt = GetComponentInChildren<TextMeshProUGUI>();
       
        SetUpCard();

    }
    private void Awake()
    {
        displayTxt = false;
    }

    void FixedUpdate()
    {
        if (BSystem != null) // enable button in cambat 
        {
            switch (BSystem.state)
            {
                case BattleState.PLAYERTURN:
                    cardUsed = false;
                    SetUpCard();

                    break;
                case BattleState.ENDPLAYERTURN:
                    if (!cardUsed)
                    {
                        DiscardAndReset();
                    }
                    break;
                case BattleState.LOST:
                    cardAttks = null;
                    cardButton.onClick.RemoveAllListeners();
                    break;
                case BattleState.START:
                    if (cardAttks == null) 
                    {
                        AssignDeckAction();
                    }
                    break;
                default:

                    break;
            }


        }

        if (cardNameFromList != null && !displayTxt)
        {
            txt.SetText(cardNameFromList);
            displayTxt = true;
            Debug.Log("placed txt " + cardNameFromList);
        }


    }

    void SetUpCard()
    {
        // adds action to onclick 
        cardButton.onClick.AddListener(() =>
        {
            if (cardAttks.cardAttaks.ContainsKey(cardNameFromList))
            {
                cardAttks.cardAttaks[cardNameFromList].Invoke(); // invoke's function from dictionary 
                cardAttks.deckManagement.DiscardCard(cardNameFromList);

                DiscardAndReset();
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No action found for key: " + cardNameFromList);
            }
        });
        cardUsed = false;
        displayTxt = false;
    }

    void DiscardAndReset()
    {
        cardAttks.deckManagement.DiscardCard(cardNameFromList); // run discard card 
        cardButton.onClick.RemoveAllListeners();
        cardUsed = true;
    }

    void AssignDeckAction()
    {
        switch (cardDraw.characterClass)
        {
            case CharacterClass.PLAYER1:
                cardAttks = GameObject.Find("CardManager").GetComponent<CardActionsCharacter1>();
                break;
            default:
                break;
        }
    }
}