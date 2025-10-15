using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

// Character selector should assign the character class enum as one of this after choosing character 
public enum CharacterClass { KNIGHT, ROGUE, WIZZARD, PLAYERLOST} // set all character classes 
public class DeckManagement : MonoBehaviour, IDataPersistence
{
    #region Save and Load
    public void LoadData(GameData data)
    {
        this.characterClass = data.playerClass;
    }

    public void SaveData(ref GameData data)
    {
        
    }

#endregion
    DataPersistenceManager Manager;

    public CharacterClass characterClass; // refenrece for enum 

    public int startingCards;
    public int currentDeckSize;

    [Header("Scriptable Objects")]
    [SerializeField]private AllCardsOfCharacter[] theDatabaseArray;
    [SerializeField]private AllCardsOfCharacter[] theStartingDeckArray;

    [Header("References")]
    // Reference to the ScriptableObject
    public AllCardsOfCharacter cardDatabase; // all posible cards for said class 
    public AllCardsOfCharacter startingDeck; // starting set of cards for said class 
 
    public BattleSystem BSystem;

    // stores all the sprites 
    public Dictionary<string, Sprite> allPossibleSprites = new Dictionary<string, Sprite>();

    // stores all cards descriptions 
    public Dictionary<string, string> allCardDescriptions = new Dictionary<string, string>();   

    [Header("Cards in Run Tiem")]
    public List<string> runtimeDeck;
    public List<string> discardedCards;
    public bool discardDeck;
    public  bool deckWasSet;
    void Awake()
    {
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    } // does not do shit on child 

    void FixedUpdate() // does not do shit on child 
    {
        if (BSystem != null)
        {
            switch (BSystem.state)
            {
                case BattleState.LOST:
                    //  reset character enum
                    characterClass = CharacterClass.PLAYERLOST;
                    BSystem.state = BattleState.DEFAULT;
                    startingDeck = null;
                    cardDatabase = null;
                    break;
                case BattleState.START:


                    break;
                case BattleState.STARTRUN:
                    if (!deckWasSet)
                    {
                        FindAndAssignCharacter();
                       

                    }
                    break;
            }
        }       
    }

    public void DiscardCard(string cardName)
    {
        // adds card to discarded pile 
        discardedCards.Add(cardName);
        // remove visual
        //GameObject.FindWithTag("DUM").GetComponent<DeckUIManager>().RemoveCardUI(cardName);
        Debug.Log($"Card discarded: {cardName}. Discarded pile now has {discardedCards.Count} cards");
    }


    public IEnumerator FindAndAssignCharacter()
    {
        yield return new WaitForSeconds(.2f);
        runtimeDeck.Clear(); 
        allPossibleSprites.Clear();
     
        // discardedCards.Clear();
        switch (characterClass)
        {
            case CharacterClass.KNIGHT:
                Debug.Log("Should assing this");
                cardDatabase = theDatabaseArray[0];
                startingDeck = theStartingDeckArray[0];
                break;
            case CharacterClass.ROGUE: 
                cardDatabase = theDatabaseArray[1];
                startingDeck = theStartingDeckArray[1];
                break;
            case CharacterClass.WIZZARD:
                cardDatabase = theDatabaseArray[2];
                startingDeck = theStartingDeckArray[2]; 
                break;

        }
        // Debug the source data
        if (startingDeck != null && startingDeck.allCards != null)
        {
            Debug.Log($"Starting deck has {startingDeck.allCards.Count} cards:");
            foreach (var card in startingDeck.allCards)
            {
                Debug.Log($"- {card}");
            }
        }

        runtimeDeck = new List<string>(startingDeck.allCards);

        for (int i = 0; i < cardDatabase.allCardSprites.Count; i++)
        {
            allPossibleSprites.Add(cardDatabase.allCards[i], cardDatabase.allCardSprites[i]);
        }

        for (int i = 0; i < cardDatabase.descriptionCard.Count; i++)
        {
            allCardDescriptions.Add(cardDatabase.allCards[i], cardDatabase.descriptionCard[i]);
        }
        //visual runtime deck show starting cards
        GameObject.FindWithTag("DUM").GetComponent<DeckUIManager>().PopulateDeckUI(runtimeDeck);

        discardedCards = new List<string>();
        Debug.Log($"Runtime deck now has {runtimeDeck.Count} cards");
     
    }


}


