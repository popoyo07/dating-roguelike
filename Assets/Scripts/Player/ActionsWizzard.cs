using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;
using UnityEditor;
using System.Drawing;
using System.Xml;

public class ActionsWizzard : Cards
{
    private void Start()
    {

        player = GameObject.FindWithTag("Player");
        playerHp = player.GetComponent<SimpleHealth>();
        deckManagement = gameObject.GetComponent<DeckManagement>();
        energy = GetComponent<EnergySystem>();
        cardAttaks.Clear();
        roundTracker = gameObject.GetComponent<RoundTracker>();
        pStatus = player.GetComponent<StatusEffects>();
        StartCoroutine(InitializeCardActions());
       
    }


    [Header("Somoke Screen")]
     private int smokeStun;
    [Range(1, 3)][SerializeField] private int smokeScreenECost;  
    
    [Header("Acid Splash")]
    [Range(1, 10)][SerializeField] private int acid;
    [Range(1, 3)][SerializeField] private int acidECost;

    [Header("Strenght Potion")]
    [Range(1, 5)][SerializeField] private int strenghtMultiplier;
    [Range(1, 3)][SerializeField] private int strenghtECost;

    [Header("Weakening Potion")]
    [Range(1, 3)][SerializeField] private int weakeningPotionECost; 
    
    [Header("Antidote")]   
    [Range(1, 3)][SerializeField] private int antidoteECost; 

    [Header("Empty Flask")]
    [Range(1, 10)][SerializeField] private int emptyFlask;
    [Range(1, 3)][SerializeField] private int emptyFlaskECost;
    
    [Header("Self-medicate")]
    [Range(1, 10)][SerializeField] private int selfMedicateShield;
    [Range(1, 3)][SerializeField] private int dmgMultiplier;
    [Range(1, 3)][SerializeField] private int selfMedicateECost;

      [Header("Shielding Potion")]
    [Range(1, 10)][SerializeField] private int shieldingPotion;
    [Range(1, 3)][SerializeField] private int shieldingPotionECost;

      [Header("Ultimate Brew")]
    [Range(1, 20)][SerializeField] private int ultimateBrew;
    [Range(1, 4)][SerializeField] private int ultimateDmgMultiplier;
    [Range(1, 3)][SerializeField] private int ultimateBrewECost;

    public void SmokeScreen()
    {
        ConsumeEnergy(smokeScreenECost);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.STUN;
        
    }
    public void AcidSplash()
    {
        ConsumeEnergy(acidECost);
        attkAmmount = acid;
        GenerateAttk(pStatus.currentStatus);
    }   
    public void StrenghtPotion()
    {
        ConsumeEnergy(acidECost);
        multStrenght = strenghtMultiplier;
       
    }
    public void WeakeningPotion()
    {
        ConsumeEnergy(weakeningPotionECost);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.VULNERABLE;
    }
    public void Antidote()
    {
        ConsumeEnergy(antidoteECost);
        // undo debuf or something 
    }
    public void EmptyFlask()
    {
        ConsumeEnergy(emptyFlaskECost);
        attkAmmount = emptyFlask;   
        GenerateAttk(pStatus.currentStatus);
    } 
    public void SelfMedicate()
    {
        ConsumeEnergy(selfMedicateECost);
        multStrenght = dmgMultiplier;
        GenerateShield(selfMedicateShield);
    }
    public void ShieldingPotion()
    {
        ConsumeEnergy(shieldingPotionECost);
        GenerateShield(shieldingPotion);
    }
    public void UltimateBrew()
    {
        ConsumeEnergy(ultimateBrewECost);
        GenerateAttk(pStatus.currentStatus);
        multStrenght = ultimateDmgMultiplier;
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.WEAK;
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
            { cards[0], (SmallHealing, smallHealingECost) },
            { cards[1], (BigHealing, healingMultiplier) },
            { cards[2], (Taunt,  tauntECost  ) },
            { cards[3], (PocketPebble,  tauntECost) },
            { cards[4], (SmokeScreen,  smokeScreenECost) },
            { cards[5], ( AcidSplash,  acidECost) },
            { cards[6], ( StrenghtPotion,  strenghtECost) },
            { cards[7], ( WeakeningPotion,  weakeningPotionECost) },
            { cards[8], ( Antidote,  antidoteECost) },
            { cards[9], ( EmptyFlask,  emptyFlaskECost) },
            { cards[10], ( SelfMedicate,  selfMedicateECost) },
            { cards[11], ( ShieldingPotion,  shieldingPotionECost) },
            { cards[12], ( UltimateBrew,  ultimateBrewECost) },
            { cards[13], ( AttackTwice,  doubleAttkECost) },
            { cards[14], (LoveyDoveyLogic, 1) },
            { cards[15], (LoveyDoveyLogic2, 1) },
            { cards[16], (LoveyDoveyLogic3, 1) },
            { cards[17], (LoveyDoveyLogic4, 1) },

        };
        Debug.Log("hello");
        foreach (var kvp in actionMap)
        {
            cardAttaks[kvp.Key] = kvp.Value.action;
            cardEnergyCost[kvp.Key] = kvp.Value.cost;
            Debug.Log(kvp.Key + " is the key");
        }


        

        Debug.Log($"Card actions for Chemist dictionary initialized with {cardAttaks.Count} entries");
    }
}
 
 
 
 
 
 
 
 
 
 