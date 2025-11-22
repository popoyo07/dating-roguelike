using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class AssignCard : MonoBehaviour
{
    // Handles all card animation logic
    AssignCardAnimation cardAnim;

    // The card name key used to look up actions, sprites, descriptions
    public string cardNameFromList;

    // Reference to the card’s UI button
    public Button cardButton;

    // Allows dragging / holding card
    public HoldCardBehavior hold;

    // Tracks if card was played this turn
    public bool cardUsed;

    // References to battle & character action systems
    private BattleSystem BSystem;
    private ActionsKnight knightCardAttks;
    private ActionsChemist actionsChemist;
    private ActionsWizzard actionsWizzard;

    // Reference to the deck draw system
    private DeckDraw cardDraw;

    // Tracks if this card has been initialized visually
    public bool cardSet;

    // Prevents multiple resets in a single turn
    public bool resetForNewTurn;

    // Reference to energy manager
    EnergySystem energy;

    // Card UI sprite
    public Image cardImage;

    // Controls showing description on hold
    CardDescription cardDescription;

    private void OnEnable()
    {
        // Get required systems & components
        cardAnim = GetComponent<AssignCardAnimation>();
        cardImage = GetComponent<Image>();
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        cardDraw = GameObject.Find("CardManager").GetComponent<DeckDraw>();
        cardButton = GetComponent<Button>();
        hold = GetComponent<HoldCardBehavior>();
        cardDescription = GetComponent<CardDescription>();
        energy = GameObject.Find("Managers").GetComponentInChildren<EnergySystem>();

        // Reset internal states
        cardUsed = false;
        resetForNewTurn = false;

        // Card setup process begins when systems finish loading
        StartCoroutine(InitializeCard());
    }

    private IEnumerator InitializeCard()
    {
        // Wait until all required components exist on the deck manager
        yield return new WaitUntil(() =>
            cardDraw.GetComponent<ActionsKnight>() != null ||
            cardDraw.GetComponent<ActionsKnight>().cardAttaks.Count > 0 ||
            energy.GetComponent<EnergySystem>() != null &&
            hold.GetComponent<HoldCardBehavior>() != null);

        // Assign correct action dictionary based on player class
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

        // Give systems time to finish loading/animating
        yield return new WaitForSeconds(.5f);

        // Set visuals + assign button events
        SetupCardButton();
        cardSet = true;
    }

    void Update()
    {
        if (BSystem == null) return;

        // Player turn → allow card functions again
        if (BSystem.state == BattleState.PLAYERTURN && cardUsed)
        {
            resetForNewTurn = false;
        }

        // End of turn → discard unused cards
        if (BSystem.state == BattleState.ENDPLAYERTURN &&
            !cardUsed &&
            gameObject.activeInHierarchy &&
            !resetForNewTurn)
        {
            ResetForNewTurn();
        }

        // Auto-discard if the enemy died mid-turn
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
        // Make sure the card only resets once
        resetForNewTurn = true;

        // Discard visually + internally
        DiscardAndReset();

        // Prepare for next draw cycle
        ResetCardForNewCombat();
    }

    public void SetupCardButton()
    {
        Debug.Log("card tried to run set up ");

        // Enable and assign card art
        cardImage.enabled = true;
        cardImage.sprite = cardDraw.allPossibleSprites[cardNameFromList];

        // Turn on description text
        cardDescription.txt.gameObject.SetActive(true);

        // Remove old click listeners to avoid stacking
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClicked);
        cardButton.interactable = true;

        // Setup hold-to-show-description event
        hold.HoldingButton.RemoveAllListeners();
        hold.HoldingButton.AddListener(OnCardHold);

        Debug.Log("card set up ");
    }

    void OnCardHold()
    {
        // Reveal card description popup
        hold.ShowExplanation(cardDraw.allCardDescriptions[cardNameFromList]);
    }

    private void OnCardClicked()
    {
        // Executes the correct action depending on player class
        switch (cardDraw.characterClass)
        {
            // ---------------- KNIGHT ----------------
            case CharacterClass.KNIGHT:

                if (knightCardAttks == null)
                    knightCardAttks = cardDraw.GetComponent<ActionsKnight>();

                if (energy.energyCounter - knightCardAttks.cardEnergyCost[cardNameFromList] >= 0)
                {
                    // Does this card have a registered action?
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

            // ---------------- CHEMIST ----------------
            case CharacterClass.CHEMIST:

                if (actionsChemist == null)
                    actionsChemist = cardDraw.GetComponent<ActionsChemist>();

                if (energy.energyCounter - actionsChemist.cardEnergyCost[cardNameFromList] >= 0)
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

            // ---------------- WIZZARD ----------------
            case CharacterClass.WIZZARD:

                if (actionsWizzard == null)
                    actionsWizzard = cardDraw.GetComponent<ActionsWizzard>();

                if (energy.energyCounter - actionsWizzard.cardEnergyCost[cardNameFromList] >= 0)
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

        // Debug card state after use
        Debug.Log($"After Using Card RuntimeDeck {cardDraw.runtimeDeck.Count}, Discarded: {cardDraw.discardedCards.Count}");
    }

    void DiscardAndReset()
    {
        // Add this card to discard pile
        cardDraw.DiscardCard(cardNameFromList);

        // Prevent using it again
        cardButton.onClick.RemoveAllListeners();

        cardUsed = true;
        cardSet = false;

        // Trigger discard animation
        StartCoroutine(cardAnim.StartingAnimation(1));
    }

    void ResetCardForNewCombat()
    {
        // Reset local card state for next deal
        cardUsed = false;
        cardSet = false;

        // Clear old card name
        cardNameFromList = null;

        // Make button usable when a new card is assigned
        if (cardButton != null)
        {
            cardButton.interactable = true;
        }
    }

    // Called by DeckDraw when a new card is given to this slot
    public IEnumerator AssignNewCard(string newCardName)
    {
        yield return new WaitForSeconds(.15f);

        cardNameFromList = newCardName;
        cardUsed = false;
        resetForNewTurn = false;

        SetupCardButton();
    }
}
