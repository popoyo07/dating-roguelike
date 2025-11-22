using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;


public class DeckDraw : DeckManagement, IDataPersistence
{
    #region Save and Load
    public void LoadData(GameData data)
    {
        // Load the saved character class from GameData
        this.characterClass = data.playerClass;
    }

    public void SaveData(ref GameData data)
    {
        // Currently unused, but required by IDataPersistence
    }

    #endregion

    GameObject[] cardGameObj;          // Stores all card GameObjects found in the scene
    public AssignCard[] cards;         // Stores AssignCard references for each card slot
    bool cardsAssigned;                // Tracks if cards were already assigned for the current turn
    bool combatEnded;                  // Tracks if the combat has ended (used for deck recovery)

    private void Awake()
    {
        // Find all card GameObjects by tag
        cardGameObj = GameObject.FindGameObjectsWithTag("Cards");

        // Get reference to BattleSystem
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        // Start initial card assignment setup
        StartCoroutine(AsigningCards());
    }

    IEnumerator AsigningCards()
    {
        // Wait until character class + deck data is assigned
        yield return StartCoroutine(FindAndAssignCharacter());

        // Create array matching amount of card objects
        cards = new AssignCard[cardGameObj.Length];

        // Assign AssignCard component for each card UI slot
        if (cardGameObj != null)
        {
            for (int i = 0; i < cardGameObj.Length; i++)
            {
                cards[i] = cardGameObj[i].GetComponent<AssignCard>();
            }
        }
    }

    IEnumerator GetRandomFromDeck(AssignCard c)
    {
        // Prevent drawing from an empty deck by waiting for recovery
        if (runtimeDeck.Count == 0)
        {
            yield return new WaitUntil(() => runtimeDeck.Count != 0);
        }

        // Pick a random card from runtimeDeck
        int r = Random.Range(0, runtimeDeck.Count);
        string cardName = runtimeDeck[r];

        // Assign the selected card visually + functionally
        StartCoroutine(c.AssignNewCard(cardName));

        // Remove card from deck after drawing it
        runtimeDeck.RemoveAt(r);
    }

    private void Update()
    {
        // Only run logic if BattleSystem is available
        if (BSystem != null)
        {
            switch (BSystem.state)
            {
                case BattleState.PLAYERTURN:

                    // Assign new cards at the start of player's turn
                    if (!cardsAssigned)
                    {
                        // Safety check: ensure card slots exist
                        if (cards == null || cards.Length == 0)
                        {
                            Debug.LogWarning("Cards array is empty or not assigned!");
                            return;
                        }

                        // Assign each card slot from runtime deck
                        for (int i = 0; i < cards.Length; i++)
                        {
                            // If deck is empty, recover it before drawing
                            if (runtimeDeck.Count == 0)
                            {
                                Debug.LogWarning("Recover for " + i);
                                StartCoroutine(RecoverDeck(0f));
                            }

                            // Draw a new card
                            StartCoroutine(GetRandomFromDeck(cards[i]));
                        }

                        cardsAssigned = true;
                        combatEnded = true;
                    }
                    break;

                case BattleState.STARTRUN:
                    // When the run starts, set deck for the selected character
                    if (!deckWasSet)
                    {
                        StartCoroutine(FindAndAssignCharacter());
                    }
                    break;

                case BattleState.ENEMYTURN:
                    // Reset card assignment on enemy turn
                    cardsAssigned = false;
                    cardPlayedCount = 0;
                    break;

                case BattleState.WON:
                    // After winning, recover the deck one time
                    if (combatEnded)
                    {
                        combatEnded = false;
                        StartCoroutine(RecoverDeck(.2f));
                    }
                    cardsAssigned = false;
                    break;

                case BattleState.LOST:
                    // Reset character data and deck on player loss
                    BSystem.state = BattleState.DEFAULT;
                    startingDeck = null;
                    cardDatabase = null;
                    deckWasSet = false;
                    break;

                default:
                    break;
            }
        }
        else
        {
            // Rare case: BSystem missing in scene
            Debug.Log("It is null ");
        }
    }

    IEnumerator RecoverDeck(float delay)
    {
        // Wait before recovering deck (used to avoid instant redraw)
        yield return new WaitForSeconds(delay);

        // Add discarded cards back into runtime deck
        runtimeDeck.AddRange(discardedCards);

        // Clear discard pile
        discardedCards.Clear();
    }
}
