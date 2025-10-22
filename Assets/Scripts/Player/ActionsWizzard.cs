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
        if (deckManagement != null && deckManagement.characterClass != CharacterClass.WIZZARD)
        {
            Debug.Log("disable wizzard");
           gameObject.GetComponent<ActionsWizzard>().enabled = false;
        }
        energy = GetComponent<EnergySystem>();
        cardAttaks.Clear();
        roundTracker = gameObject.GetComponent<RoundTracker>();
        pStatus = player.GetComponent<StatusEffects>();
        StartCoroutine(InitializeCardActions());
       
    }


    [Header("Double Shield  ")]
    [Range(1, 10)][SerializeField] private int blocks;
    [Range(1, 3)][SerializeField] private int blocksECost;  
    
    [Header("Rock skin")]
    [Range(1, 2)][SerializeField] private int shieldMultiplier;
    [Range(1, 3)][SerializeField] private int rockSkinECost;

    [Header("Rock Smash")]
    [Range(1, 5)][SerializeField] private int rockSmash;
    [Range(1, 3)][SerializeField] private int rockSmashECost;

    [Header("Healing Bubble")]
    [Range(1, 15)][SerializeField] private int bubbleHeal; 
    [Range(1, 3)][SerializeField] private int bubbleHealECost; 
    
    [Header("Water Jet")]   
    [Range(1, 10)][SerializeField] private int waterJet; 
    [Range(1, 3)][SerializeField] private int waterJetECost; 

    [Header("Water Clense")]
    [Range(1, 3)][SerializeField] private int waterClenseECost;
    
    [Header("Plant Blade")]
    [Range(1, 10)][SerializeField] private int plantBlade;
    [Range(1, 3)][SerializeField] private int plantBladeECost;

      [Header("Shoothing Plants")]
    [Range(1, 10)][SerializeField] private int shoothingPlantsDMG;
    [Range(1, 10)][SerializeField] private int shoothingPlantsXtDmg;
    [Range(1, 3)][SerializeField] private int shoothingPlantsECost;

      [Header("Plant Sap")]
    [Range(1, 20)][SerializeField] private int plantSapDMG;
    [Range(1, 3)][SerializeField] private int plantSapECost;

    public void DoubleShield()
    {
        ConsumeEnergy(blocksECost);
        GenerateShield(blocks);
        
    }
    public void RockSkin()
    {
        ConsumeEnergy(rockSkinECost);
        if (playerHp == null)
        {
            playerHp = player.GetComponent<SimpleHealth>();
        }
        playerHp.shield *= shieldMultiplier;
    }   
    public void RockSmash()
    {
        ConsumeEnergy(rockSmashECost);
        attkAmmount = rockSmash;
        GenerateAttk(pStatus.currentStatus);
    }
    public void HealingBubble()
    {
        ConsumeEnergy(bubbleHealECost);
        playerHp.RecoverHP(bubbleHeal);
    }
    public void WaterJet()
    {
        ConsumeEnergy(waterJetECost);
        attkAmmount = waterJet;
        GenerateAttk(pStatus.currentStatus);
    }
    public void WaterClense()
    {
        ConsumeEnergy(waterJetECost);
        // undo debuf or something 
    }
 
    public void PlantBlade()
    {
        ConsumeEnergy(plantBladeECost);
        attkAmmount = plantBlade;
        GenerateAttk(pStatus.currentStatus);

    }
    public void ShoothingPlants()
    {
        ConsumeEnergy(shoothingPlantsECost);
        attkAmmount = shoothingPlantsDMG;
        GenerateAttk(pStatus.currentStatus);
        xtStrenght = 5;
    }
    public void PlantSap()
    {
        ConsumeEnergy(plantSapECost);
        attkAmmount = plantSapDMG;
        GenerateAttk(pStatus.currentStatus);
        playerHp.RecoverHP(plantSapDMG / 2);
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
            { cards[4], (DoubleShield,  blocksECost) },
            { cards[5], ( RockSkin,  rockSkinECost) },
            { cards[6], ( RockSmash,  rockSmashECost) },
            { cards[7], ( HealingBubble,  bubbleHealECost) },
            { cards[8], ( WaterJet,  waterJetECost) },
            { cards[9], ( WaterClense,  waterClenseECost) },
            { cards[10], ( PlantBlade,  plantBladeECost) },
            { cards[11], ( ShoothingPlants,  shoothingPlantsECost) },
            { cards[12], ( PlantSap,  plantSapECost) },
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
 
 
 
 
 
 
 
 
 
 