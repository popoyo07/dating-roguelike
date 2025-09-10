using UnityEngine;
using System;
using System.Collections.Generic;

// Character selector should assign the character class enum as one of this after choosing character 
public enum CharacterClass { PLAYER1, PLAYER2, PLAYER3, PLAYERLOST} // set all character classes 
public class DeckManagement : MonoBehaviour
{
    public List<String> discardedCards = new List<String>() { };

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
    public AllCardsOfCharacter playerDeck; // actuall player deck during the run
    public BattleSystem BSystem;

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
                        playerDeck.allCards.AddRange(startingDeck.allCards);
                        Debug.Log("this is the info assigned to player deck " + playerDeck.allCards);

                    }
                    break;
            }
        }       
    }

    public void DiscardCard(string cardName)
    {
        // adds card to discarded pile 
        discardedCards.Add(cardName);

    }

   public  void FindAndAssignCharacter()
    {
        switch (characterClass)
        {
            case CharacterClass.PLAYER1:
                Debug.Log("Should assing this");
                cardDatabase = theDatabaseArray[0];
                startingDeck = theStartingDeckArray[0];
                break;
            case CharacterClass.PLAYER2: 
                cardDatabase = theDatabaseArray[1];
                startingDeck = theStartingDeckArray[1];
                break;
            case CharacterClass.PLAYER3:
                cardDatabase = theDatabaseArray[2];
                startingDeck = theStartingDeckArray[2]; 
                break;

        }
    }
}


