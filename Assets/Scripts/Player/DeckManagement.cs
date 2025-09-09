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

  

    public List<String> discardedCards = new List<String>() {}; 
    public int startingCards;
    public int currentDeckSize;

    [Header("References")]
    public AllCardsOfCharacter cardDatabase; // Reference to the ScriptableObject
    public BattleSystem BSystem;

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


