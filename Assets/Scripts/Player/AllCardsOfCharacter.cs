using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "CardDatabase", menuName = "Cards/Card Database")]
public class AllCardsOfCharacter : ScriptableObject
{
    [Tooltip("All possible cards in the game")]
    public List<string> allCards = new List<string>()
    {
        "SingleAttk",
        "DobleAttk",
        "SingleShield",
        "GainHP",
        "LoveyDovy"
    };
}
