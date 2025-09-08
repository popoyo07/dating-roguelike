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
                case BattleState.PLAYERTURN:
                    if (!cardsAssigned)
                    {
                       
                        for (int i = 0; i < cards.Length; i++)
                        {
                            if (cardsInDeck.Count == 0)
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

                default:
                    cardsAssigned = false;
                    break;
            }
        } else { Debug.Log("It is null "); }
    }


    void GetRandomFromDeck(AssignCard c)
    {
        int r = Random.Range(0, cardsInDeck.Count);
        c.cardNameFromList = cardsInDeck[r];
        Debug.Log(" The cards got assigned " +  c.cardNameFromList);
        // adds card to discarded pile 
        discardedCards.Add(cardsInDeck[r]);
        // removes cards from deck 
        cardsInDeck.RemoveAt(r);
    }

    void RecoverDeck() // recover deck and reset discarded cards 
    {
        cardsInDeck.AddRange(discardedCards); // add all discarded cards back
        discardedCards.Clear(); // empty the discarded pile
    }

}
