using UnityEngine;
using System;
using System.Collections.Generic;

public class DeckManagement : MonoBehaviour
{
    public List<String> cardsInDeck = new List<String>()
    { "SingleAttk",
       "SingleAttk",
       "DobleAttk", 
       "DobleAttk",
       "SingleShield",
       "SingleShield" };

    public List<String> allCards = new List<String>()
    {
       "SingleAttk",
       "DobleAttk",
       "SingleShield"
    };

    public List<String> discardedCards = new List<String>() {}; 
    public int startingCards;
    BattleSystem BSystem;
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
