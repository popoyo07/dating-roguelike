using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;
using UnityEditor;
using System.Drawing;
using System.Xml;

public class ActionsKnight : Cards
{
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        deckManagement = gameObject.GetComponent<DeckManagement>();
        energy = GetComponent<EnergySystem>();
        cardAttaks.Clear();
        roundTracker = gameObject.GetComponent<RoundTracker>();
        pStatus = player.GetComponent<StatusEffects>();
        InitializeCardActions();
       
    }


    [Header("Double Attk DMG")]
    [Range(1, 5)][SerializeField] private int doubleAttk;
    [Range(1, 3)][SerializeField] private int doubleAttkECost;

    [Header("Single Attk DMG")]
    [Range(1, 5)][SerializeField] private int swordStrike;
    [Range(1, 3)][SerializeField] private int swordStrikeECost;

    [Header("Single Shield")]
    [Range(1, 5)][SerializeField] public int shield;
    [Range(1, 3)][SerializeField] public int shieldECost;

    [Header("Small heal hability")]
    [Range(5, 10)][SerializeField] public int smallHealing;
    [Range(1, 3)][SerializeField] public int smallHealingECost;

    [Header("Big heal hability")]
    [Range(2, 3)][SerializeField] public int healingMultiplier;
    [Range(1, 3)][SerializeField] public int bigHealingECost;

    [Header("Battle Cry")]
    [Range(2, 5)][SerializeField] public int battleCryXTdmg;
    [Range(0, 3)][SerializeField] public int battleCryECost;

    [Header("Parry ")]
    [Range(0, 3)][SerializeField] public int parryECost;

    public void ParryCard() // needs testing 
    {
        pStatus.currentStatus = StatusEffect.IVENCIBLE; // should be invencible for one round
    }
    public void BattleCry() // buffs next attk 
    {
        xtStrenght = battleCryXTdmg;
        Debug.Log("Gave player extra dmg " + xtStrenght);
    }
    void ConsumeEnergy(int cost) // unsert enery cost in cost 
    {
        energy.energyCounter -= cost;
    }
    public void Shield()
    {
        ConsumeEnergy(shieldECost);
        GenerateShield(shield);

    }

    public void SwordStrike()
    {
        ConsumeEnergy(swordStrikeECost);
        attkAmmount = swordStrike;
        Debug.Log("Attk should be " + swordStrike);
        GenerateAttk(pStatus.currentStatus);
    }


    public void AttackTwice()
    {
        attkAmmount = doubleAttk;
        Debug.Log("Attk should be " + doubleAttk);
        ConsumeEnergy(doubleAttk);
        GenerateAttk(pStatus.currentStatus);
        GenerateAttk(pStatus.currentStatus);

    }
    public void SmallHealing()
    {
        ConsumeEnergy(smallHealingECost);
        player.GetComponent<SimpleHealth>().RecoverHP(smallHealing);
    }
    public void BigHealing()
    {
        int biggerHeal = smallHealing * 2;
        ConsumeEnergy(biggerHeal);
        player.GetComponent<SimpleHealth>().RecoverHP(biggerHeal);
    }
    public void LoveyDoveyLogic()  // do nothing type of cards 
    {
        ConsumeEnergy(1);
        Debug.Log("Lovely");
    }
    public void LoveyDoveyLogic2() // need to add to list   
    {
        ConsumeEnergy(1);
        Debug.Log("Lovely2");
    }

    private void InitializeCardActions()
    {
        cardAttaks.Clear();
        cardEnergyCost.Clear();

        if (deckManagement.cardDatabase?.allCards == null)
        {
            Debug.LogError("Starting deck or allCards is null!");
            return;
        }

        var cards = deckManagement.cardDatabase.allCards;

        // Define a mapping between card names and their corresponding methods + costs
        var actionMap = new Dictionary<string, (Action action, int cost)>
    {
        { cards[0], (SwordStrike, swordStrikeECost) },
        { cards[1], (AttackTwice, doubleAttkECost) },
        { cards[2], (Shield, shieldECost) },
        { cards[3], (SmallHealing, smallHealingECost) },
        { cards[4], (LoveyDoveyLogic, 1) },
        { cards[5], (BigHealing, bigHealingECost) },
        { cards[6], (BattleCry, battleCryECost) },
        { cards[7], (ParryCard, parryECost) },
        { cards[8], (BattleCry, battleCryECost) }
    };

        foreach (var kvp in actionMap)
        {
            cardAttaks[kvp.Key] = kvp.Value.action;
            cardEnergyCost[kvp.Key] = kvp.Value.cost;
        }

        Debug.Log($"Card actions dictionary initialized with {cardAttaks.Count} entries");
    }
}
