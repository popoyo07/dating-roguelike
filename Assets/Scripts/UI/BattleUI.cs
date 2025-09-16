using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    BattleSystem bSystem;
    GameObject[] cards;
    public Button endTurnB;
    bool doing;
    public DialogueUI dialogueUI; // assign in inspector or find in Awake
    //public GameObject CardUI;


    void Awake()
    {
        if (dialogueUI == null)
        {
            dialogueUI = GameObject.FindObjectOfType<DialogueUI>();
        }

        bSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        cards = GameObject.FindGameObjectsWithTag("Cards");
        endTurnB = GameObject.Find("EndTurn")?.GetComponent<Button>();

        // Initialize the button at start
        StartCoroutine(InitializeButton());
    }

    IEnumerator InitializeButton()
    {
        // Wait until BattleSystem is ready
        yield return new WaitUntil(() => bSystem != null);

        // Find the button
        //endTurnB = GameObject.Find("EndTurn")?.GetComponent<Button>();

        if (endTurnB != null)
        {
            // Add listener to the button
            endTurnB.onClick.RemoveAllListeners(); // Clear any existing listeners
            endTurnB.onClick.AddListener(OnEndTurnClicked);
            Debug.Log("End turn button initialized successfully");
        }
        else
        {
            Debug.LogError("EndTurn button not found!");
        }
    }

    void OnEndTurnClicked()
    {
        Debug.Log("End turn button clicked!");
        StartCoroutine(bSystem.EndPlayerTurn());
    }

    void FixedUpdate()
    {
        if (bSystem == null) return;

        switch (bSystem.state)
        {
            case BattleState.DIALOGUE:

                if (dialogueUI.isTalking == false)
                {
                    StartCoroutine(bSystem.DelaySwitchState(0.2f, BattleState.PLAYERTURN, "isTalking = false "));
                }
                else
                {
                    dialogueUI.StartCoroutine(dialogueUI.DelayDisable(0.1f));
                }
                break;

            case BattleState.PLAYERTURN:
                if (dialogueUI.isTalking)
                {
                    StartCoroutine(bSystem.DelaySwitchState(0f, BattleState.DIALOGUE, "Battle UI Script "));
                }
                else
                {
                    dialogueUI.StartCoroutine(dialogueUI.DelayAble(0.1f));
                }
                break;

            /*  if (!doing)
                            {
                                // Activate cards
                                for (int i = 0; i < cards.Length; i++)
                                {
                                    cards[i].SetActive(true);
                                }

                                // Ensure button is active and interactable
                                if (endTurnB != null)
                                {
                                    endTurnB.gameObject.SetActive(true);
                                    endTurnB.interactable = true;
                                }

                                doing = true;
                            }*/
            case BattleState.WON:
                dialogueUI.StartCoroutine(dialogueUI.DelayDisable(0.2f));

                break;
            case BattleState.DEFAULT:

                if (dialogueUI.isTalking) // if is stalking swithc to Dialogue state
                {
                    StartCoroutine(bSystem.DelaySwitchState(0f, BattleState.DIALOGUE, "isTalking = false "));

                }
                break;
            case BattleState.ENEMYTURN:
                if (doing)
                {
                    // Deactivate cards
                    for (int i = 0; i < cards.Length; i++)
                    {
                        cards[i].SetActive(false);
                    }

                    // Deactivate or disable button
                    if (endTurnB != null)
                    {
                        endTurnB.interactable = false;
                        endTurnB.gameObject.SetActive(false);
                    }

                    doing = false;
                }
                break;
               
            default:
                doing = false;
                break;
        }
    }
}