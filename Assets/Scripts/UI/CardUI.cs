using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text cardNameText;
    public Image cardSpriteImage;
    public Button cardButton;



    [Header("Dialogue References")]
    public GameObject canvas;
    public DialogueUI DialogueUI;
    public MenuButtons MenuButtons;


    private string cardName;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        DialogueUI = canvas.GetComponent<DialogueUI>();
        MenuButtons = canvas.GetComponent<MenuButtons>();
    }

    public void Setup(string cardName, Sprite cardSprite)
    {
        this.cardName = cardName;
        cardNameText.text = cardName;

        if (cardSpriteImage != null)
            cardSpriteImage.sprite = cardSprite;

        if (cardButton != null)
        {
            cardButton.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        Debug.LogWarning($"Card clicked: {cardName}");
        if (DialogueUI.showAllDeck)
        {
            MenuButtons.CloseDeck();
            DialogueUI.showAllDeck = false;
            CardChosen(cardName);
        }
    }

    void CardChosen(string chosenCard)
    {
        switch (chosenCard)
        {
            case "LoveCard2":
                Debug.Log("You chose: " + chosenCard + "for LoveyDovey");
                break;
            case "LoveCard3":
                Debug.Log("You chose: " + chosenCard + "for LoveyDovey");
                break;
            case "LoveCard4":
                Debug.Log("You chose: " + chosenCard + "for LoveyDovey");
                break;
            case "Shield":
                Debug.Log("You chose: Shield for LoveyDovey");

                break;
            default:
                Debug.Log("You chose: " + chosenCard + "for LoveyDovey");
                break;

        }
            
    }
}