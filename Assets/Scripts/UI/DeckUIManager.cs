using System.Collections.Generic;
using UnityEngine;

public class DeckUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent;
    public GameObject cardUIPrefab;

    [Header("References")]
    public DeckManagement deckManager;

    private List<GameObject> spawnedCards = new List<GameObject>();

    private void Start()
    {
        if (deckManager != null)
        {
            PopulateDeckUI(deckManager.runtimeDeck);
        }
    }

    public void PopulateDeckUI(List<string> deck)
    {
        ClearDeckUI();

        foreach (var cardName in deck)
        {
            AddCardUI(cardName);
        }
    }

    public void AddCardUI(string cardName)
    {
        GameObject cardObj = Instantiate(cardUIPrefab, contentParent);
        CardUI ui = cardObj.GetComponent<CardUI>();

        // Find sprite that matches card name
        Sprite sprite = null;
        if (deckManager.cardDatabase != null)
        {
            int index = deckManager.cardDatabase.allCards.IndexOf(cardName);
            if (index >= 0 && index < deckManager.cardDatabase.allCardSprites.Count)
            {
                sprite = deckManager.cardDatabase.allCardSprites[index];
            }
        }

        ui.Setup(cardName, sprite);
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