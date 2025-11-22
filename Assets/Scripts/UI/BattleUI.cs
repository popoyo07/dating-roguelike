using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Rewards;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;

    bool w;
    BattleSystem bSystem;
    public Button endTurnB;
    public DialogueUI dialogueUI; // assign in inspector or find in Awake
    public DialogueActivator enemyDialogue;
    public ResponseHandle responseHandle; 
    public GameObject cardsUI;
    Rewards reward;

    MoveRoomA roomA;
    MoveRoomB roomB;
    bool isWaiting;

    void Awake()
    {
        if (dialogueUI == null)
        {
            dialogueUI = GameObject.FindFirstObjectByType<DialogueUI>(); // if this causes an issue replace with { GameObject.FindObjectOfType<DialogueUI>()
        }

        roomA = GameObject.Find("RoomA").GetComponent<MoveRoomA>();
        roomB = GameObject.Find("RoomB").GetComponent<MoveRoomB>();
        Canvas = GameObject.Find("Canvas");
        bSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        endTurnB = GameObject.Find("EndTurn")?.GetComponent<Button>();
        responseHandle = Canvas.GetComponent<ResponseHandle>();
        reward = GameObject.FindWithTag("RewardsM").GetComponent<Rewards>();
        cardsUI = GameObject.Find("CARDS UI");

        // Initialize the button at start
        StartCoroutine(InitializeButton());
    }

    void Start()
    {
        //Check did the enemy have the activator or not
        if (enemyDialogue == null)
        {
            GameObject enemyGO = GameObject.FindWithTag("Enemy");
            if (enemyGO != null)
            {
                enemyDialogue = enemyGO.GetComponent<DialogueActivator>();
            }
        }
    }


    IEnumerator InitializeButton()
    {
        // Wait until BattleSystem is ready
        yield return new WaitUntil(() => bSystem != null);

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
        if (bSystem == null) return; // if bSystem is empty then it does not run update 

        switch (bSystem.state)
        {
 
            case BattleState.DIALOGUE:

                if (dialogueUI.isTalking == false)
                {
                    StartCoroutine(bSystem.ChangeBattleState(0f, BattleState.PLAYERTURN, "isTalking = false "));
                }
                else if (cardsUI.activeSelf)
                {
                    cardsUI.SetActive(false);
                }
                break;

            case BattleState.PLAYERTURN:
                
                if (dialogueUI.isTalking)
                {
                    StartCoroutine(bSystem.ChangeBattleState(0f, BattleState.DIALOGUE, "Battle UI Script "));
                }
                else if(cardsUI.activeSelf == false)
                {
                    dialogueUI.StartCoroutine(dialogueUI.DelayAble(0f));
                }

                break;


            case BattleState.WON:
                if (roomA != null && roomB != null)
                {
                    StartCoroutine(CheckTeleport());
                }
                break;

            case BattleState.DEFAULT:
                if (enemyDialogue == null) //if the enemy doesn't have dialogue activator, then switch to Playerturn state
                {
                    StartCoroutine(bSystem.ChangeBattleState(0f, BattleState.PLAYERTURN, "Enemy has no dialogue"));
                }
                else if (dialogueUI.isTalking) // if is talking switch to Dialogue state
                {
                    StartCoroutine(bSystem.ChangeBattleState(0f, BattleState.DIALOGUE, "isTalking = false "));
                }
              
                break;

            case BattleState.ENEMYTURN:
                cardsUI.SetActive(false);
                dialogueUI.StartCoroutine(dialogueUI.DelayDisable(0.01f));
                break;

            default:
                Debug.Log("BattleUI Default");
                break;
        }

    }
    IEnumerator CheckTeleport()
    {
        if (isWaiting)
        {
            yield break;
        }

        isWaiting = true;
        yield return new WaitUntil (() => roomA.teleported == true || roomB.teleported == false);
        yield return new WaitForSeconds(2.5f);
        isWaiting = false;  

        if (!roomA.teleported || !roomB.teleported)
        {
            Debug.Log("pp");
            dialogueUI.StartCoroutine(dialogueUI.DelayDisable(0f));

        }
    }

    IEnumerator DelayDisableUI()
    { 
        if (w)
        {
            yield break;
        }

        Debug.Log("Waiting");
        w = true;
        yield return new WaitForSeconds(.9f);
        Debug.Log("It disable the thing");
        w = false;
        cardsUI.SetActive(false);
    }
}