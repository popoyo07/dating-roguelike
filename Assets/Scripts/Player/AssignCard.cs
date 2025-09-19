using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AssignCard : MonoBehaviour
{
    public string cardNameFromList;
    public Button cardButton;
    public TextMeshProUGUI txt;
    public bool cardUsed;

    private BattleSystem BSystem;
    private ActionsCharacter1 theCardAttks;
    private DeckDraw cardDraw;
    private bool displayTxt;
    private bool cardSet;

    bool resetForNewTurn;
    EnergySystem energy;

    private void OnEnable()
    {
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        cardDraw = GameObject.Find("CardManager").GetComponent<DeckDraw>();
        cardButton = GetComponent<Button>();
        cardUsed = false;
        resetForNewTurn = false;
        energy = GameObject.Find("Managers").GetComponentInChildren<EnergySystem>();
        StartCoroutine(InitializeCard());

    }

    void Start()
    {
     
    }

    private IEnumerator InitializeCard()
    {
        // Wait for card actions to be initialized
        yield return new WaitUntil(() =>
            cardDraw.GetComponent<ActionsCharacter1>() != null &&
            cardDraw.GetComponent<ActionsCharacter1>().cardAttaks.Count > 0 && 
            energy.GetComponent<EnergySystem>() != null );

        theCardAttks = cardDraw.GetComponent<ActionsCharacter1>();
        cardSet = true;
        SetupCardButton();
    }

    void Update()
    {
        if (BSystem == null) return;

        // Handle card text display
        if (cardNameFromList != null && !displayTxt)
        {
            txt.SetText(cardNameFromList);
            displayTxt = true;
        }

        // Reset card for new turn
        if (BSystem.state == BattleState.PLAYERTURN && cardUsed)
        {
            resetForNewTurn = false;
        }

        // Auto-discard if card wasn't used by end of turn
        if (BSystem.state == BattleState.ENDPLAYERTURN && !cardUsed && gameObject.activeInHierarchy && !resetForNewTurn)
        {
            ResetForNewTurn();

        }

        if (BSystem.state == BattleState.WON && !resetForNewTurn && !cardUsed)
        {
            ResetForNewTurn();
        }
    }
    void ResetForNewTurn()
    {
        resetForNewTurn = true;
        DiscardAndReset();
        ResetCardForNewCombat();
    }
    public void SetupCardButton()
    {
        if (cardButton == null || theCardAttks == null) return;

        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClicked);
        cardButton.interactable = true;
    }
    private void OnCardClicked()
    {
            

        if (theCardAttks.cardEnergyCost[cardNameFromList] <= energy.energyCounter)
        {
            Debug.Log("Enrgy cost is " + theCardAttks.cardEnergyCost[cardNameFromList] + " and the current energy is " + energy.energyCounter);
            if (theCardAttks.cardAttaks.ContainsKey(cardNameFromList))
            {
                theCardAttks.cardAttaks[cardNameFromList].Invoke();

                cardUsed = true; // <-- mark the card as used immediately
                DiscardAndReset();
                cardButton.interactable = false;
            }
            else
            {
                Debug.LogWarning("No action found for key: " + cardNameFromList);
            }
        } else
        {
            Debug.LogWarning("Not enough Energy");
        }
        
    }


    void DiscardAndReset()
    {
        if (theCardAttks != null && theCardAttks.deckManagement != null)
        {
            theCardAttks.deckManagement.DiscardCard(cardNameFromList);
        }

        cardUsed = true;
        cardSet = false;
        displayTxt = false;
    }

    void ResetCardForNewCombat()
    {
        // Reset card state for new turn
        cardUsed = false;
        cardSet = false;
        displayTxt = false;

        // Clear old card name and prepare for new assignment
        cardNameFromList = null;
        txt.SetText("");

        // Reactivate button
        if (cardButton != null)
        {
            cardButton.interactable = true;
        }

       
    }

    // This method should be called by DeckDraw when assigning a new card
    public void AssignNewCard(string newCardName)
    {
        cardNameFromList = newCardName;
        displayTxt = false; // Allow text to be updated in next Update
        cardUsed = false; 
        resetForNewTurn = false;
        SetupCardButton();
    }
}