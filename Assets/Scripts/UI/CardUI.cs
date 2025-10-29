using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;

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
    public EnemySpawner enemySpawner;
    public DialogueProgression progression;

    private string cardName;

    private void Awake()
    {
        GameObject spawnerObj = GameObject.FindWithTag("EnemyS");
        if (spawnerObj != null)
        {
            enemySpawner = spawnerObj.GetComponent<EnemySpawner>();
        }
        else
        {
            Debug.LogWarning("No object with tag 'EnemyS' found in the scene!");
        }
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
                    //Debug.Log("Boss found and DialogueActivator cached!");
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
        var boss = enemySpawner.boss;
        var phase = progression.phase;

        switch (chosenCard)
        {
            case "LoveyDovy"://Beating Heart --> Vampire Boss (Phase 3)
                if (boss == enemySpawner.sirenBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(number: 1, nextArray: 2);
                }
                else if (boss == enemySpawner.vampireBoss)
                {
                    if (phase == 3)
                    {
                        DialogueUI.MarkPendingSkip();
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 3)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(2, nextArray: 1);
                    }
                }
                else if (boss == enemySpawner.idkBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(2, nextArray: 1);
                }

                break;

            case "LoveyDovy2": //Thorned Rose --> Vampire Boss (Phase 1)

                if (boss == enemySpawner.sirenBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(number: 1, nextArray: 2);
                }
                else if (boss == enemySpawner.vampireBoss)
                {
                    if (phase == 1)
                    {
                        DialogueUI.MarkPendingSkip();
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 1)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(2, nextArray: 1);
                    }
                }
                else if (boss == enemySpawner.idkBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(number: 1, nextArray: 2);
                }

                break;
            case "LoveyDovy3": //Mirror

                Activator.ContinueDialogue(2, 1);

                break;

            case "LoveyDovy4": //Magic conch --> Siren Boss (Phase 1)

                if (boss == enemySpawner.sirenBoss)
                {
                    if (phase == 1)
                    {
                        DialogueUI.MarkPendingSkip();
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 1)
                    {
                        Debug.LogWarning("Phase not = to 1");
                    }
                }
                else if (boss == enemySpawner.vampireBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(2, nextArray: 1);
                }
                else if (boss == enemySpawner.idkBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(2, nextArray: 1);
                }

                break;


            case "LoveyDovy5": //Lyre Instrument
                Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                Activator.ContinueDialogue(2, 1);

                break;

            case "LoveyDovy6": //Love Bottle
                Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                Activator.ContinueDialogue(2, 1);

                break;

            default: //Cards that don't belong as LoveyDovy Card
                Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                Activator.ContinueDialogue(2,1);
                break;

        }
            
    }
}