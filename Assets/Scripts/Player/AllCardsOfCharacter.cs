using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Cards/Card Database")]
public class AllCardsOfCharacter : ScriptableObject
{
     [Tooltip("All possible cards in the game")]
     public List<string> allCards = new List<string>()
     {
     };

    public List<Sprite> allSprites = new List<Sprite>() { };
}  
