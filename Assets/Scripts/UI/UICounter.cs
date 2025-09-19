using System;
using TMPro;
using UnityEngine;

public enum CounterSelection
{
    Energy,
    CardsInDeck,
    CardsDiscarded
}

// UI logic so it updates each caounter when necesary 
public class UICounter : MonoBehaviour
{
    EnergySystem energy;
    DeckDraw deckDraw;
    public int shownEnergy;
    public int shownDeck;
    public int shownDiscards;
    TextMeshProUGUI TMP;
    public CounterSelection counterSelection;
    bool isZero = false;
    void Awake()
    {
        shownEnergy = 0;
        shownDeck = 0;
        shownDiscards = 0;
        TMP = GetComponent<TextMeshProUGUI>();
        switch (counterSelection) 
        {
            case CounterSelection.Energy:
                energy = GameObject.Find("Managers").GetComponentInChildren<EnergySystem>();
                break;
             default:
                deckDraw = GameObject.Find("Managers").GetComponentInChildren<DeckDraw>();
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // making that each of them only update whenever the value was changed
        switch (counterSelection) 
        {
            case CounterSelection.Energy:
                if (energy != null && energy.energyCounter != shownEnergy)
                {
                    UpdateAndShowValue(shownEnergy, energy.energyCounter, "Energy: ");
                }
                break;
            case CounterSelection.CardsInDeck:
                if (deckDraw != null && deckDraw.runtimeDeck.Count != shownDeck) 
                {
                    UpdateAndShowValue(shownDeck, deckDraw.runtimeDeck.Count, "Deck: ");

                }
                break;
            case CounterSelection.CardsDiscarded:
                if (deckDraw != null && deckDraw.discardedCards.Count != shownDiscards) 
                {
                    UpdateAndShowValue(shownDiscards, deckDraw.discardedCards.Count, "Discards: ");
                    isZero = false;

                }else if (deckDraw.discardedCards.Count == 0 && !isZero)
                {
                    shownDiscards = 0;
                    TMP.text = "Discards: " + shownDiscards;
                    isZero = true;  
                }
                break;
        }

     
    }

    // for final game should be updated to bot include text
    void UpdateAndShowValue(int value, int dataSource, string text) 
    {
       value  = dataSource;
        TMP.text = text + value;
    }
}
