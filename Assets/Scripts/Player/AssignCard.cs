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
    private CardActionsCharacter1 cardAttks;
    private DeckDraw cardDraw;
    private bool displayTxt;
    private bool cardSet;

    void Start()
    {
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        cardDraw = GameObject.Find("CardManager").GetComponent<DeckDraw>();
        cardButton = GetComponent<Button>();
        cardUsed = false;

        StartCoroutine(InitializeCard());
    }

    private IEnumerator InitializeCard()
    {
        // Wait for card actions to be initialized
        yield return new WaitUntil(() =>
            cardDraw.GetComponent<CardActionsCharacter1>() != null &&
            cardDraw.GetComponent<CardActionsCharacter1>().cardAttaks.Count > 0);

        cardAttks = cardDraw.GetComponent<CardActionsCharacter1>();
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
            ResetCardForNewTurn();
        }

        // Auto-discard if card wasn't used by end of turn
        if (BSystem.state == BattleState.ENDPLAYERTURN && !cardUsed && gameObject.activeInHierarchy)
        {
            DiscardAndReset();
        }
    }

    public void SetupCardButton()
    {
        if (cardButton == null || cardAttks == null) return;

        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClicked);
        cardButton.interactable = true;
    }

    private void OnCardClicked()
    {
        if (cardAttks.cardAttaks.ContainsKey(cardNameFromList))
        {
            cardAttks.cardAttaks[cardNameFromList].Invoke();
            cardAttks.deckManagement.DiscardCard(cardNameFromList);
            DiscardAndReset();
            // Don't set inactive - just disable interaction
            cardButton.interactable = false;
        }
        else
        {
            Debug.LogWarning("No action found for key: " + cardNameFromList);
        }
    }

    void DiscardAndReset()
    {
        if (cardAttks != null && cardAttks.deckManagement != null)
        {
            cardAttks.deckManagement.DiscardCard(cardNameFromList);
        }

        cardUsed = true;
        cardSet = false;
        displayTxt = false;
    }

    void ResetCardForNewTurn()
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

        // Ensure the card is active
        gameObject.SetActive(true);
    }

    // This method should be called by DeckDraw when assigning a new card
    public void AssignNewCard(string newCardName)
    {
        cardNameFromList = newCardName;
        displayTxt = false; // Allow text to be updated in next Update
        cardUsed = false;
        SetupCardButton();
    }
}