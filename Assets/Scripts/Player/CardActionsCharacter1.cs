using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;


public class CardActionsCharacter1 : Cards
{
    public Dictionary<string, Action> cardAttaks = new Dictionary<string, Action>();
    private void Start()
    {
        deckManagement = gameObject.GetComponent<DeckManagement>();
        // I could also move this dictioary to a scriptable object 
        cardAttaks.Clear();
        InitializeCardActions();

    }
   

    [Header("Double Attk DMG")]
    [Range(1, 5)]
    public int doubleAttk;
    
    [Header("Single Attk DMG")]
    [Range(1, 5)]
    public int singleAttk;
    
    [Header("Single Shield")]
    [Range(1,5)]
    public int singleShield;

    [Header("heal hability")]
    public int healing;

    public void SingleShield()
    {
        GenerateShield(singleShield);
        
    }

    public void AttackOnce()
    {
        attkAmmount = 2;
        Debug.Log("Attk should be " + singleAttk);
        GenerateAttk();
    }


    public void AttackTwice()
    {
        attkAmmount = 2;
        Debug.Log("Attk should be " + doubleAttk);

        GenerateAttk();
        GenerateAttk();
        
    }
    public void GainHealth()
    {
        player.GetComponent<SimpleHealth>().RecoverHP(healing);
    }

    public void LoveyDoveyLogic()
    {
        Debug.Log("Lovely");
    }

    private void InitializeCardActions()
    {
        cardAttaks.Clear();

        // Use the same source as runtimeDeck - startingDeck.allCards
        if (deckManagement.cardDatabase != null && deckManagement.cardDatabase.allCards != null)
        {
            // Make sure we have enough cards for the actions
            if (deckManagement.cardDatabase.allCards.Count >= 5)
            {
                cardAttaks.Add(deckManagement.cardDatabase.allCards[0], AttackOnce);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[1], AttackTwice);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[2], SingleShield);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[3], GainHealth);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[4], LoveyDoveyLogic);

                Debug.Log("Card actions dictionary initialized with " + cardAttaks.Count + " entries");
            }
            else
            {
                Debug.LogError("Not enough cards in starting deck for all actions!");
            }
        }
        else
        {
            Debug.LogError("Starting deck or allCards is null!");
        }
    }
}
