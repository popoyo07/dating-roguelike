using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

// Character selector assigns one of these classes
public enum CharacterClass { KNIGHT, CHEMIST, WIZZARD, PLAYERLOST }

public class DeckManagement : MonoBehaviour
{
    public int cardPlayedCount;

    DataPersistenceManager Manager;

    public CharacterClass characterClass; // holds selected player class

    public int startingCards;
    public int currentDeckSize;

    [Header("Scriptable Objects")]
    public AllCardsOfCharacter[] theDatabaseArray;      // database of all cards (per class)
    public AllCardsOfCharacter[] theStartingDeckArray;  // starting deck (per class)

    [Header("References")]
    public AllCardsOfCharacter cardDatabase;   // active class card database
    public AllCardsOfCharacter startingDeck;   // active class starting deck

    public BattleSystem BSystem;

    // stores every sprite for cards in dictionary form (key = card name)
    public Dictionary<string, Sprite> allPossibleSprites = new Dictionary<string, Sprite>();

    // stores descriptions for all cards (key = card name)
    public Dictionary<string, string> allCardDescriptions = new Dictionary<string, string>();

    [Header("Cards in Run Tiem")]
    public List<string> runtimeDeck;     // deck actively used during combat
    public List<string> discardedCards;  // cards moved to discard pile
    public bool discardDeck;
    public bool deckWasSet;

    void Awake()
    {
        // Find BattleSystem reference on load
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }

    void FixedUpdate()
    {
        // Run only if BattleSystem exists
        if (BSystem != null)
        {
            switch (BSystem.state)
            {
                case BattleState.LOST:
                    // Reset class and deck data on defeat
                    characterClass = CharacterClass.PLAYERLOST;
                    BSystem.state = BattleState.DEFAULT;
                    startingDeck = null;
                    cardDatabase = null;
                    break;

                case BattleState.START:
                    // No action on START (reserved for future use)
                    break;

                case BattleState.STARTRUN:
                    // Assign deck for selected character only once per run
                    if (!deckWasSet)
                    {
                        StartCoroutine(FindAndAssignCharacter());
                    }
                    break;
            }
        }
    }

    public void DiscardCard(string cardName)
    {
        // Adds card to discard pile
        discardedCards.Add(cardName);
    }

    public IEnumerator FindAndAssignCharacter()
    {
        // Small delay ensures save/load systems & objects finish initializing
        yield return new WaitForSeconds(.1f);

        // Clear previous deck and sprite references
        runtimeDeck.Clear();
        allPossibleSprites.Clear();

        // Assign appropriate deck & database based on selected class
        switch (characterClass)
        {
            case CharacterClass.KNIGHT:
                cardDatabase = theDatabaseArray[0];
                startingDeck = theStartingDeckArray[0];
                break;

            case CharacterClass.CHEMIST:
                cardDatabase = theDatabaseArray[1];
                startingDeck = theStartingDeckArray[1];
                break;

            case CharacterClass.WIZZARD:
                cardDatabase = theDatabaseArray[2];
                startingDeck = theStartingDeckArray[2];
                break;
        }

        // Initialize runtime deck using selected starting deck
        runtimeDeck = new List<string>(startingDeck.allCards);

        // Build dictionary linking card names with sprites
        for (int i = 0; i < cardDatabase.allCardSprites.Count; i++)
        {
            allPossibleSprites.Add(cardDatabase.allCards[i], cardDatabase.allCardSprites[i]);
        }

        // Build dictionary linking card names with descriptions
        for (int i = 0; i < cardDatabase.descriptionCard.Count; i++)
        {
            allCardDescriptions.Add(cardDatabase.allCards[i], cardDatabase.descriptionCard[i]);
        }

        // Show current deck in UI
        GameObject.FindWithTag("DUM").GetComponent<DeckUIManager>().PopulateDeckUI(runtimeDeck);

        // Initialize discard pile
        discardedCards = new List<string>();
    }
}
