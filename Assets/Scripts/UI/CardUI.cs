using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text cardNameText;
    public Image cardSpriteImage;
    public Button cardButton;

    [Header("Dialogue References")]
    public GameObject Boss;
    public GameObject canvas;
    public DialogueUI DialogueUI;
    public MenuButtons MenuButtons;
    public DialogueActivator Activator;


    private string cardName;

    private void Awake()
    {
        //Boss = GameObject.FindWithTag("Boss");
        canvas = GameObject.Find("Canvas");
        DialogueUI = canvas.GetComponent<DialogueUI>();
        MenuButtons = canvas.GetComponent<MenuButtons>();
        //Activator = Boss.GetComponent<DialogueActivator>();
    }


    private void Update()
    {
        // Try to find boss only once after it spawns
        if (Boss == null)
        {
            Boss = GameObject.FindWithTag("Boss");
            if (Boss != null)
            {
                Activator = Boss.GetComponent<DialogueActivator>();
                if (Activator != null)
                {
                    Debug.Log("Boss found and DialogueActivator cached!");
                }
            }
            else
            {
                // If still not found, try again next frame
                return;
            }
        }
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
            case "LoveCard":
                Debug.Log("You chose: " + chosenCard + "for LoveyDovey");
                break;
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
                Activator.ContinueDialogue(2);
                break;
            default:
                Debug.Log("You chose: " + chosenCard + "for LoveyDovey");
                Activator.ContinueDialogue(1);
                break;

        }
            
    }
}