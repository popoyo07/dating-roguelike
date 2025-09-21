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
    [Range(1, 5)]
    [SerializeField] private int doubleAttk;
    [SerializeField] private int doubleAttkECost;

    [Header("Single Attk DMG")]
    [Range(1, 5)]
    [SerializeField] private int swordStrike;
    [SerializeField] private int swordStrikeECost;

    [Header("Single Shield")]
    [Range(1, 5)]
    [SerializeField] public int shield;
    [SerializeField] public int shieldECost;

    [Header("heal hability")]
    [SerializeField] public int healing;
    [SerializeField] public int healingECost;

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
    public void GainHealth()
    {
        ConsumeEnergy(healingECost);
        player.GetComponent<SimpleHealth>().RecoverHP(healing);
    }

    public void LoveyDoveyLogic()
    {
        ConsumeEnergy(1);
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
                // make sure names and acctions match properly in here 
                cardAttaks.Add(deckManagement.cardDatabase.allCards[0], SwordStrike);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[1], AttackTwice);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[2], Shield);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[3], GainHealth);
                cardAttaks.Add(deckManagement.cardDatabase.allCards[4], LoveyDoveyLogic);
                Debug.Log("Card actions dictionary initialized with " + cardAttaks.Count + " entries");

                // set card's energy cost
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[0], swordStrikeECost);
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[1], doubleAttkECost);
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[2], shieldECost);
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[3], healingECost);
                cardEnergyCost.Add(deckManagement.cardDatabase.allCards[4], doubleAttkECost);
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
        healing = serializedObject.FindProperty("healing");
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
        CheckValue(healing, "Healing");

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