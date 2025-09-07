using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;


public class CardActions : Cards
{
    public Dictionary<string, Action> cardAttaks = new Dictionary<string, Action>();
    private void Start()
    {
        cardAttaks.Clear();
        cardAttaks.Add(deckManagement.allCards[0], AttackOnce);
        cardAttaks.Add(deckManagement.allCards[1], AttackTwice);
        cardAttaks.Add(deckManagement.allCards[2], SingleShield);
        cardAttaks.Add(deckManagement.allCards[3], GainHealth);
        cardAttaks.Add(deckManagement.allCards[4], LoveyDoveyLogic);

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
    
}
