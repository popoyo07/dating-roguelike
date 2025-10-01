using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeckDraw : DeckManagement
{
    GameObject[] cardGameObj;
    public AssignCard[] cards;
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
    IEnumerator GetRandomFromDeck(AssignCard c)
    {
        if (runtimeDeck.Count == 0)
        {
            //Debug.LogWarning("Trying to draw from empty deck!");
            yield return new WaitUntil(() => runtimeDeck.Count != 0); // wait for reset  
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

                            if (runtimeDeck.Count == 0)
                            {
                                Debug.LogWarning("Recover for " + i);
                                StartCoroutine(RecoverDeck(0f));

                            }
                            StartCoroutine(GetRandomFromDeck(cards[i]));
                            Debug.LogWarning("It assigned " + (i + 1) + " Cards");
                        }
                        cardsAssigned = true;
                        combatEnded = true;
                    }
                    break;

                case BattleState.STARTRUN:
                    if (!deckWasSet)
                    {
                        FindAndAssignCharacter();
                       

                    }
                    break;

                case BattleState.ENEMYTURN:
                    cardsAssigned = false;
                    break;


                case BattleState.WON: 
                    if (combatEnded) // just reusing the earlier bool for this 
                    {
                        combatEnded = false;
                        StartCoroutine(RecoverDeck(.2f));
                    }
                    cardsAssigned = false;
                    break;
                case BattleState.LOST:
                    //  reset character enum
                   
                    BSystem.state = BattleState.DEFAULT;
                    startingDeck = null;
                    cardDatabase = null;
                    deckWasSet = false;
                    break;
                default:
                    //cardsAssigned = false;
                    break;
            }
        } else { Debug.Log("It is null "); }
    }



    IEnumerator RecoverDeck(float delay) // recover deck and reset discarded cards 
    {
        yield return new WaitForSeconds(delay);
        Debug.Log($"Before recovery - Runtime: {runtimeDeck.Count}, Discarded: {discardedCards.Count}");

        // Add all discarded cards back to runtime deck
        runtimeDeck.AddRange(discardedCards);

        // Clear the discarded pile
        discardedCards.Clear();

        Debug.Log($"After recovery - Runtime: {runtimeDeck.Count}, Discarded: {discardedCards.Count}");

    }


}