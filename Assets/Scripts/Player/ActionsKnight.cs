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
    bool attkDone;
    bool turnBuff; 
    
    public Dictionary<string, int> cardEnergyCost = new Dictionary<string, int>();

    public Dictionary<string, Action> cardAttaks = new Dictionary<string, Action>();
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        deckManagement = gameObject.GetComponent<DeckManagement>();
        energy = GetComponent<EnergySystem>();
        cardAttaks.Clear();
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

    [Header("small heal hability")]
    [Range(5, 10)][SerializeField] public int smallHealing;
    [Range(1, 3)][SerializeField] public int smallHealingECost;

    [Header("big heal hability")]
    [Range(2, 3)][SerializeField] public int healingMultiplier;
    [Range(1, 3)][SerializeField] public int bigHealingECost;

    [Header("Battle Cry")]
    [Range(2, 5)][SerializeField] public int battleCryXTdmg;
    [Range(0, 3)][SerializeField] public int battleCryECost;

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
        GenerateAttk();
    }


    public void AttackTwice()
    {
        attkAmmount = doubleAttk;
        Debug.Log("Attk should be " + doubleAttk);
        ConsumeEnergy(doubleAttk);
        GenerateAttk();
        GenerateAttk();

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
    public void LoveyDoveyLogic()
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

        // Use the same source as runtimeDeck - startingDeck.allCards
        if (deckManagement.cardDatabase != null && deckManagement.cardDatabase.allCards != null)
        {
            // Make sure we have enough cards for the actions
            if (deckManagement.cardDatabase.allCards.Count >= 5)
            {
                // make sure names and acctions match properly in here 
                cardAttaks.Add(deckManagement.cardDatabase.allCards[0], SwordStrike);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[1], AttackTwice);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[2], Shield);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[3], SmallHealing);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[4], LoveyDoveyLogic);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[5], BigHealing);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[6], BattleCry);
                Debug.Log("Card actions dictionary initialized with " + cardAttaks.Count + " entries");

                // set card's energy cost
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[0], swordStrikeECost);
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[1], doubleAttkECost);
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[2], shieldECost);
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[3], smallHealingECost); 
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[4], doubleAttkECost);
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[5], bigHealingECost);
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[6], battleCryECost);
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





#if UNITY_EDITOR

[CustomEditor(typeof(ActionsKnight)), CanEditMultipleObjects]
public class TheEditor : Editor
{
    SerializedProperty doubleAttk;
    SerializedProperty singleAttk;
    SerializedProperty singleShield;
    SerializedProperty healing;

    private void OnEnable()
    {
        doubleAttk = serializedObject.FindProperty("doubleAttk");
        singleAttk = serializedObject.FindProperty("swordStrike");
        singleShield = serializedObject.FindProperty("shield");
        healing = serializedObject.FindProperty("smallHealing");
    }

    public override void OnInspectorGUI()
    {
        // Always update the serialized object
        serializedObject.Update();

        // Draw the default inspector
        DrawDefaultInspector();

        // Check each property and display warnings
        // Add entry for each new card 
        CheckValue(doubleAttk, "Double Attack");
        CheckValue(singleAttk, "Sword Strike");
        CheckValue(singleShield, "Shield");
        CheckValue(healing, "Small Healing");

        // Apply changes
        serializedObject.ApplyModifiedProperties();
    }

    private void CheckValue(SerializedProperty property, string label)
    {
        if (property.intValue <= 0)
        {
            EditorGUILayout.HelpBox($"{label} should be greater than 0", MessageType.Warning);
        }
    }
}
#endif