using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class AssignCard : MonoBehaviour
{
    // Handles card movement animations
    AssignCardAnimation cardAnim;

    // Name of this card in the deck dictionary
    public string cardNameFromList;

    // Actual UI button attached to the card
    public Button cardButton;

    // Script that handles drag/hold behavior
    public HoldCardBehavior hold;

    // Tracks if card has been used this turn
    public bool cardUsed;

    // References to battle and character action systems
    private BattleSystem BSystem;
    private ActionsKnight knightCardAttks;
    private ActionsChemist actionsChemist;
    private ActionsWizzard actionsWizzard;

    // Deck system reference (card drawing)
    private DeckDraw cardDraw;
    public bool cardSet; // Tracks if card is set up on screen

    bool resetForNewTurn;

    // Reference to Energy system
    EnergySystem energy;

    // Card sprite UI image
    public Image cardImage;

    // Shows card description on hold
    CardDescription cardDescription;

    private void OnEnable()
    {
        // Fetch all required components
        cardAnim = GetComponent<AssignCardAnimation>();
        cardImage = GetComponent<Image>();
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        cardDraw = GameObject.Find("CardManager").GetComponent<DeckDraw>();
        cardButton = GetComponent<Button>();
        hold = GetComponent<HoldCardBehavior>();
        cardDescription = GetComponent<CardDescription>();
        energy = GameObject.Find("Managers").GetComponentInChildren<EnergySystem>();

        // Reset states
        cardUsed = false;
        resetForNewTurn = false;

        // Initialize card once all components are ready
        StartCoroutine(InitializeCard());
    }

    private IEnumerator InitializeCard()
    {
        // Wait until card systems are loaded
        yield return new WaitUntil(() =>
            cardDraw.GetComponent<ActionsKnight>() != null ||
            cardDraw.GetComponent<ActionsKnight>().cardAttaks.Count > 0 ||
            energy.GetComponent<EnergySystem>() != null &&
            hold.GetComponent<HoldCardBehavior>() != null);

        // Assign correct character action set
        switch (cardDraw.characterClass)
        {
            case CharacterClass.KNIGHT:
                knightCardAttks = cardDraw.GetComponent<ActionsKnight>();
                break;
            case CharacterClass.CHEMIST:
                actionsChemist = cardDraw.GetComponent<ActionsChemist>();
                break;
            case CharacterClass.WIZZARD:
                actionsWizzard = cardDraw.GetComponent<ActionsWizzard>();
                break;
        }

        // Give time for animations or load delays
        yield return new WaitForSeconds(.5f);

        // Set up card visuals and button
        SetupCardButton();
        cardSet = true;
    }

    void Update()
    {
        if (BSystem == null) return;

        // Reset card at the start of player's turn
        if (BSystem.state == BattleState.PLAYERTURN && cardUsed)
        {
            resetForNewTurn = false;
        }

        // If turn ends and the card was NOT used → discard it
        if (BSystem.state == BattleState.ENDPLAYERTURN && !cardUsed && gameObject.activeInHierarchy && !resetForNewTurn)
        {
            ResetForNewTurn();
        }

        // Auto discard if enemy died but card wasn't used
        if (BSystem.enemyHP != null)
        {
            if (BSystem.enemyHP.dead() && !resetForNewTurn && !cardUsed)
            {
                ResetForNewTurn();
            }
        }
    }

    void ResetForNewTurn()
    {
        resetForNewTurn = true;
        DiscardAndReset();      // Visually discard + animate
        ResetCardForNewCombat(); // Reset state for next draw
    }

    public void SetupCardButton()
    {
        Debug.Log("card tried to run set up ");

        // Enable image and assign correct sprite
        cardImage.enabled = true;
        cardImage.sprite = cardDraw.allPossibleSprites[cardNameFromList];

        // Enable description text
        cardDescription.txt.gameObject.SetActive(true);

        // Clear existing click listeners to avoid double-calling
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClicked);
        cardButton.interactable = true;

        // Setup hold (press & hold) description popup
        hold.HoldingButton.RemoveAllListeners();
        hold.HoldingButton.AddListener(OnCardHold);

        Debug.Log("card set up ");
    }

    void OnCardHold()
    {
        // Show card description when holding
        hold.ShowExplanation(cardDraw.allCardDescriptions[cardNameFromList]);
    }

    private void OnCardClicked()
    {
        // Call correct character class action
        switch (cardDraw.characterClass)
        {
            case CharacterClass.KNIGHT:
                if (knightCardAttks == null)
                    knightCardAttks = cardDraw.GetComponent<ActionsKnight>();

                // Enough energy?
                if (knightCardAttks.cardEnergyCost[cardNameFromList] <= energy.energyCounter)
                {
                    if (knightCardAttks.cardAttaks.ContainsKey(cardNameFromList))
                    {
                        knightCardAttks.cardAttaks[cardNameFromList].Invoke();
                        cardUsed = true;
                        DiscardAndReset();
                        cardButton.interactable = false;
                    }
                    else Debug.LogWarning("No action found for key: " + cardNameFromList);
                }
                else Debug.LogWarning("Not enough Energy");
                break;

            case CharacterClass.CHEMIST:
                if (actionsChemist == null)
                    actionsChemist = cardDraw.GetComponent<ActionsChemist>();

                if (actionsChemist.cardEnergyCost[cardNameFromList] <= energy.energyCounter)
                {
                    if (actionsChemist.cardAttaks.ContainsKey(cardNameFromList))
                    {
                        actionsChemist.cardAttaks[cardNameFromList].Invoke();
                        cardUsed = true;
                        DiscardAndReset();
                        cardButton.interactable = false;
                    }
                }
                else Debug.LogWarning("Not enough Energy");
                break;

            case CharacterClass.WIZZARD:
                if (actionsWizzard == null)
                    actionsWizzard = cardDraw.GetComponent<ActionsWizzard>();

                if (actionsWizzard.cardEnergyCost[cardNameFromList] <= energy.energyCounter)
                {
                    if (actionsWizzard.cardAttaks.ContainsKey(cardNameFromList))
                    {
                        actionsWizzard.cardAttaks[cardNameFromList].Invoke();
                        cardUsed = true;
                        DiscardAndReset();
                        cardButton.interactable = false;
                    }
                }
                else Debug.LogWarning("Not enough Energy");
                break;
        }

        Debug.Log($"After Using Card RuntimeDeck {cardDraw.runtimeDeck.Count}, Discarded: {cardDraw.discardedCards.Count}");
    }

    void DiscardAndReset()
    {
        // Add card to discard pile
        cardDraw.DiscardCard(cardNameFromList);

        // removes functionality and avoids making it clickable again 
        cardButton.onClick.RemoveAllListeners();

        cardUsed = true;
        cardSet = false;

        // Play discard animation
        StartCoroutine(cardAnim.StartingAnimation(1));
    }

    void ResetCardForNewCombat()
    {
        // Reset card state
        cardUsed = false;
        cardSet = false;

        // Clear name (waiting for next card assignment)
        cardNameFromList = null;

        // Re-enable button for next draw
        if (cardButton != null)
        {
            cardButton.interactable = true;
        }
    }

    // Called by DeckDraw when dealing a new card to this slot
    public IEnumerator AssignNewCard(string newCardName)
    {
        yield return new WaitForSeconds(.15f);

        cardNameFromList = newCardName;
        cardUsed = false;
        resetForNewTurn = false;

        SetupCardButton();
    }
}
