using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections;

public class ActionsChemist : Cards
{
    private void Start()
    {
        // Find main combat references
        player = GameObject.FindWithTag("Player");
        playerHp = player.GetComponent<SimpleHealth>();
        deckManagement = gameObject.GetComponent<DeckManagement>();

        // Disable this script if the current class is not Chemist
        if (deckManagement != null && deckManagement.characterClass != CharacterClass.CHEMIST)
        {
            Debug.Log("disable chemist");
            this.gameObject.GetComponent<ActionsChemist>().enabled = false;
        }

        energy = GetComponent<EnergySystem>();

        // Make sure actions won’t duplicate
        cardAttaks.Clear();

        // Extra references
        roundTracker = gameObject.GetComponent<RoundTracker>();
        pStatus = player.GetComponent<StatusEffects>();

        // Build dictionary of actions after systems initialize
        StartCoroutine(InitializeCardActions());
    }

    // -------------------- CARD VALUES --------------------

    [Header("Somoke Screen")]
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


    // -------------------- CARD LOGIC --------------------

    public void SmokeScreen()
    {
        ConsumeEnergy(smokeScreenECost);
        // Stun the enemy for one turn
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.STUN;
    }

    public void AcidSplash()
    {
        ConsumeEnergy(acidECost);
        attkAmmount = acid;
        // Deal damage influenced by current status
        GenerateAttk(pStatus.currentStatus);
    }

    public void StrenghtPotion()
    {
        ConsumeEnergy(strenghtECost);
        // Increases next attack
        multStrenght = strenghtMultiplier;
    }

    public void WeakeningPotion()
    {
        ConsumeEnergy(weakeningPotionECost);
        // Makes enemy take more damage
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.VULNERABLE;
    }

    public void Antidote()
    {
        ConsumeEnergy(antidoteECost);
        // Removes all positive/negative statuses from player
        pStatus.currentStatus = StatusEffect.NORMAL;
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
        // Buff next attack
        multStrenght = dmgMultiplier;
        // Give shield immediately
        GenerateShield(selfMedicateShield);
    }

    public void ShieldingPotion()
    {
        ConsumeEnergy(shieldingPotionECost);
        // Adds shield to player
        GenerateShield(shieldingPotion);
    }

    public void UltimateBrew()
    {
        ConsumeEnergy(ultimateBrewECost);

        // Big attack
        attkAmmount = ultimateBrew;
        GenerateAttk(pStatus.currentStatus);

        // Buff next attack
        multStrenght = ultimateDmgMultiplier;

        // Apply debuff to enemy
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.WEAK;
    }


    // -------------------- DICTIONARY SETUP --------------------

    private IEnumerator InitializeCardActions()
    {
        yield return new WaitForSeconds(.2f);

        // Reset dictionaries before refilling
        cardAttaks.Clear();
        cardEnergyCost.Clear();

        // Safety check — prevent null errors
        if (deckManagement.cardDatabase?.allCards == null)
        {
            Debug.LogError("Starting deck or allCards is null!");
            yield break;
        }

        var cards = deckManagement.cardDatabase.allCards;

        // Maps each card key to its function + energy cost
        var actionMap = new Dictionary<string, (Action action, int cost)>
        {
            // These refer to scriptable object order
            { cards[0], (SmallHealing, smallHealingECost) },
            { cards[1], (BigHealing, healingMultiplier) },
            { cards[2], (Taunt, tauntECost) },
            { cards[3], (PocketPebble, tauntECost) },

            { cards[4], (SmokeScreen, smokeScreenECost) },
            { cards[5], (AcidSplash, acidECost) },
            { cards[6], (StrenghtPotion, strenghtECost) },
            { cards[7], (WeakeningPotion, weakeningPotionECost) },
            { cards[8], (Antidote, antidoteECost) },
            { cards[9], (EmptyFlask, emptyFlaskECost) },
            { cards[10], (SelfMedicate, selfMedicateECost) },
            { cards[11], (ShieldingPotion, shieldingPotionECost) },
            { cards[12], (UltimateBrew, ultimateBrewECost) },

            { cards[13], (AttackTwice, doubleAttkECost) },
            { cards[14], (LoveyDoveyLogic, 1) },
            { cards[15], (LoveyDoveyLogic2, 1) },
            { cards[16], (LoveyDoveyLogic3, 1) },
            { cards[17], (LoveyDoveyLogic4, 1) },
            { cards[18], (LoveyDoveyLogic5, 1) },
            { cards[19], (LoveyDoveyLogic6, 1) }
        };

        Debug.Log("hello");

        // Push actions into dictionaries
        foreach (var kvp in actionMap)
        {
            cardAttaks[kvp.Key] = kvp.Value.action;
            cardEnergyCost[kvp.Key] = kvp.Value.cost;
            Debug.Log(kvp.Key + " is the key");
        }

        Debug.Log($"Card actions for Chemist dictionary initialized with {cardAttaks.Count} entries");
    }
}
