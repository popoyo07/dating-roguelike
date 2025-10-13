using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Cards/Card Database")]
public class AllCardsOfCharacter : ScriptableObject
{
    [Tooltip("All possible card names for this character")]
    public List<string> allCards = new List<string>();

    [Tooltip("All matching card sprites (index must match allCards)")]
    public List<Sprite> allCardSprites = new List<Sprite>();

    [Tooltip("All card Descriptions")]
    public List<string> descriptionCard = new List<string>();
}