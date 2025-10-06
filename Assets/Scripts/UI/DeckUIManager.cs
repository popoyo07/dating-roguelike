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
            PopulateDeckUI(deckManager.runtimeDeck);
        }
    }

    public void PopulateDeckUI(List<string> deck)
    {
        ClearDeckUI();

        foreach (var card in deck)
        {
            AddCardUI(card);
        }
    }

    public void AddCardUI(string cardName)
    {
        GameObject cardObj = Instantiate(cardUIPrefab, contentParent);
        CardUI ui = cardObj.GetComponent<CardUI>();
        ui.Setup(cardName);
        spawnedCards.Add(cardObj);
    }

    /*public void RemoveCardUI(string cardName)
    {
        var found = spawnedCards.Find(go => go.GetComponent<CardUI>().cardNameText.text == cardName);
        if (found != null)
        {
            spawnedCards.Remove(found);
            Destroy(found);
        }
    }*/

    private void ClearDeckUI()
    {
        foreach (var obj in spawnedCards)
        {
            Destroy(obj);
        }
        spawnedCards.Clear();
    }
}
