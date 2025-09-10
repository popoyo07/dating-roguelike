using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;


public class DeckDraw : DeckManagement
{
    GameObject[] cardGameObj;
    AssignCard[] cards;
    bool cardsAssigned;
    bool combatEnded;
   

    private void Awake()
    {

        cardGameObj = GameObject.FindGameObjectsWithTag("Cards"); // find card game objects 
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        FindAndAssignCharacter();
       

        cards = new AssignCard[cardGameObj.Length];
        if (cardGameObj != null)
        {
            for (int i = 0; i < cardGameObj.Length; i++)
            {
                cards[i] = cardGameObj[i].GetComponent<AssignCard>(); // get their assign card reference 
            }
        }
      
    }
    void GetRandomFromDeck(AssignCard c)
    {
        if (runtimeDeck.Count == 0)
        {
            Debug.LogWarning("Trying to draw from empty deck!");
            return;
        }

        int r = Random.Range(0, runtimeDeck.Count);
        string cardName = runtimeDeck[r];

        // Use the new method to assign the card
        c.AssignNewCard(cardName);

        Debug.Log("The cards got assigned " + cardName);
        runtimeDeck.RemoveAt(r);
    }

    private void FixedUpdate()
    {
        if (BSystem != null)
        {
            switch (BSystem.state)
            {
                case BattleState.PLAYERTURN:
                    if (!cardsAssigned)
                    {
                        for (int i = 0; i < cards.Length; i++)
                        {
                            // Make sure card is active before assigning
                            cards[i].gameObject.SetActive(true);

                            if (runtimeDeck.Count == 0)
                            {
                                RecoverDeck();
                            }
                            GetRandomFromDeck(cards[i]);
                        }
                        cardsAssigned = true;
                    }
                    break;
                case BattleState.STARTRUN:
                    if (!deckWasSet)
                    {
                        FindAndAssignCharacter();
                       

                    }
                    break;

               
                case BattleState.WON: 
                    if (discardedCards.Count != 0) // check if there are cards in discard pile 
                    {
                        RecoverDeck();
                    }
                    cardsAssigned = true;
                    break;
                case BattleState.LOST:
                    //  reset character enum
                    characterClass = CharacterClass.PLAYERLOST;
                    BSystem.state = BattleState.DEFAULT;
                    startingDeck = null;
                    cardDatabase = null;
                    deckWasSet = false;
                    break;
                default:
                    cardsAssigned = false;
                    break;
            }
        } else { Debug.Log("It is null "); }
    }



    void RecoverDeck() // recover deck and reset discarded cards 
    {
        Debug.Log($"Before recovery - Runtime: {runtimeDeck.Count}, Discarded: {discardedCards.Count}");

        // Add all discarded cards back to runtime deck
        runtimeDeck.AddRange(discardedCards);

        // Clear the discarded pile
        discardedCards.Clear();

        Debug.Log($"After recovery - Runtime: {runtimeDeck.Count}, Discarded: {discardedCards.Count}");

    }


}
