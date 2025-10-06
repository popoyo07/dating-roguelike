using System.Collections.Generic;
using UnityEngine;

public class DeckUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent; // assign Content here
    public GameObject cardUIPrefab; // assign CardUI prefab here

    [Header("References")]
    public DeckManagement deckManager; // assign DeckManagement instance

    private List<GameObject> spawnedCards = new List<GameObject>();

    private void Start()
    {
        if (deckManager != null)
        {
            PopulateDeckUI(deckManager.runtimeDeck, deckManager.runtimeDeckImages);
        }
    }

    public void PopulateDeckUI(List<string> deck, List<Sprite> deckImages)
    {
        ClearDeckUI();

        // Use index to keep name & image aligned
        for (int i = 0; i < deck.Count; i++)
        {
            string cardName = deck[i];
            Sprite cardImage = (i < deckImages.Count) ? deckImages[i] : null;
            AddCardUI(cardName, cardImage);
        }
    }

    public void AddCardUI(string cardName, Sprite cardImage)
    {
        GameObject cardObj = Instantiate(cardUIPrefab, contentParent);
        CardUI ui = cardObj.GetComponent<CardUI>();
        ui.Setup(cardName, cardImage);
        spawnedCards.Add(cardObj);
    }

    private void ClearDeckUI()
    {
        foreach (var obj in spawnedCards)
        {
            Destroy(obj);
        }
        spawnedCards.Clear();
    }
}