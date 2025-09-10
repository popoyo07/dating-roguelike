using UnityEngine;


public class DeckDraw : DeckManagement
{
    GameObject[] cardGameObj;
    AssignCard[] cards;
    bool cardsAssigned;
    bool combatEnded;
    
    private void Awake()
    {

        cardGameObj = GameObject.FindGameObjectsWithTag("Cards");
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        FindAndAssignCharacter();

        cards = new AssignCard[cardGameObj.Length];
        if (cardGameObj != null)
        {
            for (int i = 0; i < cardGameObj.Length; i++)
            {
                cards[i] = cardGameObj[i].GetComponent<AssignCard>();
            }
        }
      
    }

    private void FixedUpdate()
    {
        if (BSystem != null)
        {
            switch (BSystem.state)
            {
                case BattleState.STARTRUN:
                    if (!deckWasSet)
                    {
                        FindAndAssignCharacter();
                        playerDeck.allCards.AddRange(startingDeck.allCards);
                        Debug.Log("this is the info assigned to player deck " + playerDeck.allCards);

                    }
                    break;

                case BattleState.PLAYERTURN:
                    if (!cardsAssigned)
                    {
                       
                        for (int i = 0; i < cards.Length; i++)
                        {
                            if (playerDeck.allCards.Count == 0)
                            {
                                RecoverDeck();
                            }
                            GetRandomFromDeck(cards[i]);
                        }
                        cardsAssigned = true;
                    }
                    break;
                case BattleState.WON: 
                    if (!cardsAssigned) // just reusing the earlier bool for this 
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
                    break;
                default:
                    cardsAssigned = false;
                    break;
            }
        } else { Debug.Log("It is null "); }
    }


    void GetRandomFromDeck(AssignCard c)
    {
        int r = Random.Range(0, playerDeck.allCards.Count);
        c.cardNameFromList = playerDeck.allCards[r];        // Assign name for card 
        Debug.Log(" The cards got assigned " +  c.cardNameFromList);
        // removes cards from deck 
        playerDeck.allCards.RemoveAt(r);
    }

    void RecoverDeck() // recover deck and reset discarded cards 
    {
        playerDeck.allCards.Clear();
        playerDeck.allCards.AddRange(discardedCards); // add all discarded cards back
        discardedCards.Clear(); // empty the discarded pile
    }


}
