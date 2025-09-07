using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;


public class CardActions : Cards
{
    public Dictionary<string, Action> cardAttaks = new Dictionary<string, Action>();
    private void Awake()
    {
        cardAttaks.Clear();
        cardAttaks.Add(deckManagement.allCards[0], AttackOnce);
        cardAttaks.Add(deckManagement.allCards[1], AttackTwice);
        cardAttaks.Add(deckManagement.allCards[2], SingleShield);

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
    
}
