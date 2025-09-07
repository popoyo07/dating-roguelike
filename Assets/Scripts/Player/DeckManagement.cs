using UnityEngine;
using System;
using System.Collections.Generic;

public class DeckManagement : MonoBehaviour
{
    public List<String> cardsInDeck = new List<String>()
    { "SingleAttk",
       "DobleAttk",
      "SingleAttk",
        "DobleAttk",
        "DobleAttk",
       "SingleShield",
       "GainHP","GainHP","GainHP",
       "SingleShield",
       "SingleShield"
     };

    // we need to have an exact amount of how many we have so we can assign drop rates to them 
    public List<String> allCards = new List<String>() 
    {
       "SingleAttk",
       "DobleAttk",
       "SingleShield", 
       "GainHP",
       "LoveyDovy"

    };

    public List<String> discardedCards = new List<String>() {}; 
    public int startingCards;
    public BattleSystem BSystem;
    public int currentDeckSize;


  
    void Awake()
    {
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }

    
    void FixedUpdate()
    {
        if (BSystem != null && BSystem.state == BattleState.LOST)
        { 
        //  reset cardsInDeck back to default logic         
        }
    }
}
