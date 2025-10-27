using System.Collections.Generic;
using UnityEngine;

public class DeckUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent; // Parent transform where all card UI elements will be instantiated
    public GameObject cardUIPrefab; // Prefab used to create individual card UI elements

    [Header("References")]
    public DeckManagement deckManager; // Reference to the DeckManagement script containing the deck and card database

    private List<GameObject> spawnedCards = new List<GameObject>(); // Keeps track of all currently spawned card UI objects

    private void Start()
    {
        // When the scene starts, populate the UI with the current runtime deck if deckManager is assigned
        if (deckManager != null)
        {
            PopulateDeckUI(deckManager.runtimeDeck);
        }
    }

    // Populates the deck UI with a list of card names
    public void PopulateDeckUI(List<string> deck)
    {
        ClearDeckUI(); // First clear any existing cards in the UI

        foreach (var cardName in deck)
        {
            AddCardUI(cardName); // Add each card in the deck to the UI
        }
    }

    // Instantiates a single card UI element and sets it up
    public void AddCardUI(string cardName)
    {
        GameObject cardObj = Instantiate(cardUIPrefab, contentParent); // Create the UI object as a child of contentParent
        CardUI ui = cardObj.GetComponent<CardUI>(); // Get the CardUI component to setup the card display

        // Find sprite that matches the card name
        Sprite sprite = null;
        if (deckManager.cardDatabase != null) // Ensure the card database exists
        {
            int index = deckManager.cardDatabase.allCards.IndexOf(cardName); // Find the index of the card in the database
            if (index >= 0 && index < deckManager.cardDatabase.allCardSprites.Count)
            {
                sprite = deckManager.cardDatabase.allCardSprites[index]; // Get the corresponding sprite
            }
        }

        ui.Setup(cardName, sprite); // Initialize the CardUI with the card name and sprite
        spawnedCards.Add(cardObj); // Keep track of the spawned card
    }

    // Clears all currently displayed cards from the UI
    private void ClearDeckUI()
    {
        foreach (var obj in spawnedCards)
        {
            Destroy(obj); 
        }
        spawnedCards.Clear();
    }
}