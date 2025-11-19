using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;
using System.Collections;

public class CardUI : MonoBehaviour
{
    public BattleSystem battleSystem;

    [Header("UI References")]
    public TMP_Text cardNameText;
    public Image cardSpriteImage;
    public Button cardButton;

    [Header("Dialogue References")]
    public GameObject Boss;
    public Enemy bossEnemyScript;
    public GameObject canvas;
    public DialogueUI DialogueUI;
    public MenuButtons MenuButtons;
    public DialogueActivator Activator;
    public EnemySpawner enemySpawner;
    public DialogueProgression progression;

    public AudioSource loveyDovey;

    private string cardName;
    public bool correctLovyDovy;
    
   // public bool bossRomanced;

    private void Awake()
    {
        //battleSystem = GetComponentInChildren<BattleSystem>();
        //battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        // bossRomanced = false;
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

    private void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }


    private void Update()
    {
        // Try to find boss only once after it spawns
        if (Boss == null)
        {
            Boss = GameObject.FindWithTag("Boss");
            if (Boss != null)
            {
                bossEnemyScript = Boss.GetComponentInChildren<Enemy>();
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
        Debug.LogWarning("card set up for choice ");
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
            loveyDovey.Play();
            StartCoroutine(HoldForLoveyDoveySFX());
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
            case "LoveyDovy"://Beating Heart --> Vampire (Phase 3)
                if (boss == enemySpawner.sirenBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(1, 0);

                    //bossEnemyScript.Angry();
                    bossEnemyScript.AngryHealth();
                }
                else if (boss == enemySpawner.vampireBoss)
                {
                    if (phase == 3)
                    {
                        //  correctLovyDovy = true;
                        //  bossRomanced = true;
                        battleSystem.bossRomanced = true;

                      //  Debug.Log(bossRomanced);
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 3)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(2, 1);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                    }
                }
                else if (boss == enemySpawner.idkBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(2, 1);

                    bossEnemyScript.Angry();
                    bossEnemyScript.AngryHealth();
                }

                    break;

            case "LoveyDovy2": //Thorned Rose --> Vampire (Phase 1) && Bird (Phase 3)

                if (boss == enemySpawner.sirenBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(1, 2);

                    bossEnemyScript.Angry();
                    bossEnemyScript.AngryHealth();
                }
                else if (boss == enemySpawner.vampireBoss)
                {
                    if (phase == 1)
                    {
                        //correctLovyDovy = true;
                        battleSystem.bossRomanced = true;

                        DialogueUI.MarkPendingSkip();
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 1)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(2, 1);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                    }
                }
                else if (boss == enemySpawner.idkBoss)
                {
                    if (phase == 3)
                    {
                        battleSystem.bossRomanced = true;

                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 3)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                    }
                }

                break;
            case "LoveyDovy3": //Mirror --> Siren (Phase 2) & Bird (Phase 1)

                if (boss == enemySpawner.sirenBoss)
                {
                    if (phase == 2)
                    {
                        //correctLovyDovy = true;
                        battleSystem.bossRomanced = true;

                        DialogueUI.MarkPendingSkip();
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 2)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase not = 2: {phase}");
                        Activator.ContinueDialogue(number: 2, 1);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                    }
                }
                else if (boss == enemySpawner.vampireBoss)
                {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(number: 2, 1);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                }
                else if (boss == enemySpawner.idkBoss)
                {
                    if (phase == 1)
                    {
                       // correctLovyDovy = true;
                        battleSystem.bossRomanced = true;

                        DialogueUI.MarkPendingSkip();
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 1)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(number: 2, 1);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                    }
                }
                break;

            case "LoveyDovy4": //Magic conch --> Siren (Phase 1)

                if (boss == enemySpawner.sirenBoss)
                {
                    if (phase == 1)
                    {
                        battleSystem.bossRomanced = true;

                        DialogueUI.MarkPendingSkip();
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(number: 1, 2);
                    }
                    else if (phase != 1)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase not = 1: {phase}");
                        Activator.ContinueDialogue(2, 1);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                    }
                }
                else if (boss == enemySpawner.vampireBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(2, 1);

                    bossEnemyScript.Angry();
                    bossEnemyScript.AngryHealth();
                }
                else if (boss == enemySpawner.idkBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(2, 1);

                    bossEnemyScript.Angry();
                    bossEnemyScript.AngryHealth();
                }

                break;


            case "LoveyDovy5": //Lyre Instrument --> Siren (Phase 3) & Vampire (Phase 2)
                if (boss == enemySpawner.sirenBoss)
                {
                    if (phase == 3)
                    {
                        battleSystem.bossRomanced = true;
                       // bossRomanced = true;
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 0);
                        
                    }
                    else if (phase != 3)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase not = 3: {phase}");
                        Activator.ContinueDialogue(2, 1);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                    }
                }
                else if (boss == enemySpawner.vampireBoss)
                {
                    if (phase == 2)
                    {
                        battleSystem.bossRomanced = true;

                        DialogueUI.MarkPendingSkip();
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 2)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(number: 2, 1);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                    }
                }
                else if (boss == enemySpawner.idkBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                    Activator.ContinueDialogue(2, nextArray: 1);

                    bossEnemyScript.Angry();
                    bossEnemyScript.AngryHealth();
                }

                break;

            case "LoveyDovy6": //Love Bottle --> Bird (Phase 2)
                if (boss == enemySpawner.sirenBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase not = 3: {phase}");
                    Activator.ContinueDialogue(2, 1);

                    bossEnemyScript.Angry();
                    bossEnemyScript.AngryHealth();
                }
                else if (boss == enemySpawner.vampireBoss)
                {
                    Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase not = 3: {phase}");
                    Activator.ContinueDialogue(2, 1);

                    bossEnemyScript.Angry();
                    bossEnemyScript.AngryHealth();
                }
                else if (boss == enemySpawner.idkBoss)
                {
                    if (phase == 2)
                    {
                        battleSystem.bossRomanced = true;

                        DialogueUI.MarkPendingSkip();
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(1, 2);
                    }
                    else if (phase != 2)
                    {
                        Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");
                        Activator.ContinueDialogue(number: 2, 1);

                        bossEnemyScript.Angry();
                        bossEnemyScript.AngryHealth();
                    }
                }


                break;

            default: //Cards that don't belong as LoveyDovy Card
                Debug.Log($"Chosen Card: {chosenCard} | Boss: {boss} | Phase: {phase}");

              //  bossRomanced = false;
                Activator.ContinueDialogue(2,1);
                break;

        }
            
    }

    IEnumerator HoldForLoveyDoveySFX()
    {
        yield return new WaitForSeconds(1.5f);
        MenuButtons.CloseDeck();
    }
}