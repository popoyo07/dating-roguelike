using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardImageListManager : MonoBehaviour
{
    //public GameObject imageItemPrefab;
    private DeckDraw deckDraw;

    [SerializeField] private AllCardsOfCharacter cardDatabase;

    void Start()
    {
        deckDraw = GameObject.FindWithTag("CardMan").GetComponent<DeckDraw>();
        GenerateImageList();
    }

    void GenerateImageList()
    {
        if (cardDatabase == null || cardDatabase.allSprites == null)
        {
            Debug.LogWarning("Card database or sprites list is null.");
            return;
        }

        foreach (var sprite in cardDatabase.allSprites)
        {
            /*GameObject imageGO = Instantiate(imageItemPrefab, transform); // Instantiate as a child of this GameObject

            Image img = imageGO.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = sprite;
            }
            else
            {
                Debug.LogWarning("Image component not found on prefab!");
            }*/
        }
    }
}