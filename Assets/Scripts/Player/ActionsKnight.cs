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
        playerHp = player.GetComponent<SimpleHealth>();
        deckManagement = gameObject.GetComponent<DeckManagement>();
        if (deckManagement != null && deckManagement.characterClass != CharacterClass.KNIGHT)
        {
            Debug.Log("disable knight");
            gameObject.GetComponent<ActionsKnight>().enabled = false;
        }
        energy = GetComponent<EnergySystem>();
        cardAttaks.Clear();
        roundTracker = gameObject.GetComponent<RoundTracker>();
        pStatus = player.GetComponent<StatusEffects>();
        StartCoroutine(InitializeCardActions());
       
    }



    [Header("Single Attk DMG")]
    [Range(1, 10)][SerializeField] private int swordStrike;
    [Range(1, 3)][SerializeField] private int swordStrikeECost;

    [Header("Small Shield")]
    [Range(1, 5)][SerializeField] public int shield;
    [Range(1, 3)][SerializeField] public int shieldECost;


    [Header("Battle Cry")]
    [Range(2, 5)][SerializeField] public int battleCryXTdmg;
    [Range(0, 3)][SerializeField] public int battleCryECost;

    [Header("Parry ")]
    [Range(0, 3)][SerializeField] public int parryECost; 
    
    [Header("Shield Bash (stun) ")]
    [Range(2, 10)][SerializeField] public int stun;
    [Range(1, 3)][SerializeField] public int stunECost;

    [Header("Piercing Jab ")]
    [Range(2, 10)][SerializeField] public int piercing;
    [Range(1, 3)][SerializeField] public int piercingECost;

    [Header("Protective Oath")]
    [Range(1, 20)][SerializeField] public int protectiveOath;
    [Range(1, 3)][SerializeField] public int protectiveOathECost; 

    [Header("Second Wind")]
    [Range(1, 3)][SerializeField] public int secondWindECost;
    
    [Header("Iron Resolve")]
    [SerializeField] public int ironResolve;
    [Range(1, 3)][SerializeField] public int ironResolveECost;



    public void IronResolve()
    {
        ConsumeEnergy(ironResolveECost);
        ironResolve = playerHp.shield;
        attkAmmount = ironResolve;
        GenerateAttk(pStatus.currentStatus);
    }
    public void SecondWind()
    {
        ConsumeEnergy(secondWindECost);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.VULNERABLE;
    }
    public void ProtectiveOath()
    {
        ConsumeEnergy(protectiveOathECost);
        GenerateShield(protectiveOath);
    }
    public void Piercing()
    {
        ConsumeEnergy(piercingECost);
        LastStatus = pStatus.currentStatus;
        attkAmmount = piercing;
        pStatus.currentStatus = StatusEffect.SHIELDIGNORED;
        GenerateAttk(pStatus.currentStatus);
        pStatus.currentStatus = LastStatus; 
    }
    public void ShieldBash()
    {
        ConsumeEnergy(stunECost);
        attkAmmount = stun;
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.STUN;
    }
    public void ParryCard() // needs testing 
    {
        ConsumeEnergy(parryECost);
        pStatus.currentStatus = StatusEffect.IVINCIBLE; // should be invencible for one round
    }
    public void BattleCry() // buffs next attk 
    {
        ConsumeEnergy(battleCryECost);
        xtStrenght = battleCryXTdmg;
        Debug.Log("Gave player extra dmg " + xtStrenght);
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


  

    private IEnumerator InitializeCardActions()
    {
        yield return new WaitForSeconds(.2f);
        cardAttaks.Clear();
        cardEnergyCost.Clear();

        if (deckManagement.cardDatabase?.allCards == null)
        {
            Debug.LogError("Starting deck or allCards is null!");
            yield break;
        }

        var cards = deckManagement.cardDatabase.allCards;

        // Define a mapping between card names and their corresponding methods + costs
        var actionMap = new Dictionary<string, (Action action, int cost)>
        {
                // scriptable object should be in this order 
            { cards[0], (SwordStrike, swordStrikeECost) },
            { cards[1], (AttackTwice, doubleAttkECost) },
            { cards[2], (Shield, shieldECost) },
            { cards[3], (SmallHealing, smallHealingECost) },
            { cards[4], (LoveyDoveyLogic, 1) },
            { cards[5], (BigHealing, bigHealingECost) },
            { cards[6], (BattleCry, battleCryECost) },
            { cards[7], (ParryCard, parryECost) },
            { cards[8], (ShieldBash, stunECost) },
            { cards[9], (Piercing, piercingECost) },
            { cards[10], (ProtectiveOath, protectiveOathECost) },
            { cards[11], (SecondWind, secondWindECost) },
            { cards[12], (IronResolve, ironResolveECost) },
            { cards[13], (Taunt, tauntECost) },
            { cards[14], (IronResolve, ironResolveECost) },
            { cards[15], (LoveyDoveyLogic2, 1) },
            { cards[16], (LoveyDoveyLogic3, 1) },
            { cards[17], (LoveyDoveyLogic4, 1) },
            { cards[18], (LoveyDoveyLogic5, 1) },
            { cards[19], (LoveyDoveyLogic6, 1) }
        };
        Debug.Log("hello");

        foreach (var kvp in actionMap)
        {
            cardAttaks[kvp.Key] = kvp.Value.action;
            cardEnergyCost[kvp.Key] = kvp.Value.cost;
            Debug.Log(kvp.Key + " is the key");
        }


        

        Debug.Log($"Card actions for Knight dictionary initialized with {cardAttaks.Count} entries");
    }
}
