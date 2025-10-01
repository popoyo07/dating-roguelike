using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardImageListManager : MonoBehaviour
{
    public GameObject imageItemPrefab;
    DeckDraw deckDraw;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deckDraw = GameObject.FindWithTag("CardMan").GetComponent<DeckDraw>();
        GenerateImageList();
    }

    private void Update()
    {
        GenerateImageList();
    }

    // Update is called once per frame
    void GenerateImageList()
    {
        string firstItem = deckDraw.runtimeDeck[0];

        foreach (Sprite sprite in firstItem)
        {
            GameObject newItem = Instantiate(imageItemPrefab, transform);
            Image imageComponent = newItem.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = sprite;
            }
        }
    }
}
