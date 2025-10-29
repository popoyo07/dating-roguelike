using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CounterImages : MonoBehaviour
{

    [SerializeField] private Sprite[] spriteList;
    [SerializeField] private DeckDraw deckDraw;
    public CharacterClass playerClass;

    [SerializeField] private Image deckImage;
    [SerializeField] private Image discardImage;

    private void Start()
    {
        deckImage = GameObject.Find("DeckImage").GetComponent<Image>();
        discardImage = GameObject.Find("DiscardImage").GetComponent<Image>();
        deckDraw = GameObject.Find("CardManager").GetComponent<DeckDraw>();

        playerClass = deckDraw.characterClass;

        switch (playerClass)
        {
            case CharacterClass.KNIGHT:
                deckImage.sprite = spriteList[0];
                discardImage.sprite = spriteList[0];
                break;
            case CharacterClass.CHEMIST:
                deckImage.sprite = spriteList[1];
                discardImage.sprite = spriteList[1];
                break;
            case CharacterClass.WIZZARD:
                deckImage.sprite = spriteList[2];
                discardImage.sprite = spriteList[2];
                break;
        }

    }

}
