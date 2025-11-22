using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST,
    DEFAULT,
    ENDPLAYERTURN,
    ENDENEMYTURN,
    STARTRUN,
    DIALOGUE,
    REWARD,
    WONGAME
}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject player;
    public GameObject enemy;

    public SimpleHealth enemyHP;
    public SimpleHealth playerHP;

    public int turnCounter = 0;
    public bool moveA;
    public bool moveB;

    public EnemySpawner enemySpawner;
    Rewards rewards;
    ChooseRoom chooseRoom;
    public DialogueUI dialogueUI;
    public CardUI cardUI;
    MenuButtons menuButtons;

    public bool finalReward;
    public bool bossRomanced;

    private bool rewardShown = false;
    private bool rewardDisplayed = false;
    private bool roomDisplayed = false;

    public bool clickedEndTurn;

    void Start()
    {
        state = BattleState.DEFAULT;
        Debug.Log("Current state is " + state);
        SetUpBattle();

        rewards = GameObject.FindWithTag("RewardsM").GetComponent<Rewards>();
        chooseRoom = GameObject.FindWithTag("RoomManager").GetComponent<ChooseRoom>();
        menuButtons = GameObject.FindWithTag("Canvas").GetComponent<MenuButtons>();

        finalReward = false;
    }

    private void FixedUpdate()
    {
        if (playerHP != null && enemyHP != null)
        {
            // Player death
            if (playerHP.dead())
            {
                state = BattleState.LOST;
                Debug.Log("Current state is " + state);
                playerHP = null;
            }

            // Enemy death
            if (enemyHP.dead())
            {
                HandleEnemyDeath();
            }

            // Handle REWARD state
            if (state == BattleState.REWARD)
            {
                dialogueUI.StartCoroutine(dialogueUI.DelayDisable(.01f));

                if (!rewardDisplayed)
                {
                    rewardDisplayed = true;
                    roomDisplayed = false;
                    StartCoroutine(ShowRewardNextFrame());
                }

                if (rewards.pickedReward && !roomDisplayed)
                {
                    rewards.gameObject.SetActive(false);
                    chooseRoom.ShowRoomOptions();
                    roomDisplayed = true;
                }
            }

            // Reset flags when leaving REWARD
            if (state != BattleState.REWARD)
            {
                rewardDisplayed = false;
            }

            // Reset flags when returning to DEFAULT
            if (state == BattleState.DEFAULT)
            {
                rewardDisplayed = false;
                roomDisplayed = false;
                rewards.pickedReward = false;
                chooseRoom.chosenRoom = false;
                rewardShown = false;
            }
        }

        HandleStateSwitches();
    }

    private void HandleEnemyDeath()
    {
        Debug.Log("bossRomanced: " + bossRomanced);

        if (enemyHP.isBoss && !bossRomanced)
        {
            Debug.Log("Regular boss defeated");
            state = BattleState.WONGAME;
            menuButtons.winMenu.SetActive(true);
            menuButtons.ResetDialogueIndex();
        }
        else if (enemyHP.isBoss && bossRomanced)
        {
            Debug.Log("Boss romanced reward");
            TryShowReward();
        }
        else if (!enemyHP.isBoss && !finalReward)
        {
            TryShowReward();
        }

        if (chooseRoom.chosenRoom)
        {
            StartCoroutine(ChangeBattleState(0f, BattleState.WON, "BattleSystem"));
            enemyHP = null;
            moveA = true;
            moveB = true;
        }
    }

    private void TryShowReward()
    {
        if (!rewardShown && state != BattleState.REWARD)
        {
            rewardShown = true;
            StartCoroutine(ChangeBattleState(1.5f, BattleState.REWARD, "BattleSystem"));
        }
    }

    private IEnumerator ShowRewardNextFrame()
    {
        yield return null; // wait 1 frame
        rewards.ShowRewardOptions();
        rewards.gameObject.SetActive(true);
    }

    private void HandleStateSwitches()
    {
        switch (state)
        {
            case BattleState.START:
                StartCoroutine(ChangeBattleState(.2f, BattleState.PLAYERTURN, "BattleSystem"));
                break;

            case BattleState.WON:
                rewardShown = false;
                rewards.pickedReward = false;
                chooseRoom.chosenRoom = false;
                StartCoroutine(ChangeBattleState(2.5f, BattleState.DEFAULT, "BattleSystem"));
                break;

            case BattleState.PLAYERTURN:
                clickedEndTurn = false;
                break;

            case BattleState.STARTRUN:
                StartCoroutine(ChangeBattleState(.2f, BattleState.START, "BattleSystem"));
                break;

            case BattleState.ENDENEMYTURN:
                clickedEndTurn = false;
                StartCoroutine(ChangeBattleState(.1f, BattleState.PLAYERTURN, "BattleSystem"));
                break;

            case BattleState.LOST:
                menuButtons.loseMenu.SetActive(true);
                enemySpawner.DestroyBoss(resumeDefault: false);
                break;

            case BattleState.WONGAME:
                HandleWongameMenus();
                break;

            case BattleState.ENDPLAYERTURN:
                StartCoroutine(ChangeBattleState(1f, BattleState.ENEMYTURN, "BattleSystem"));
                break;
        }
    }

    private void HandleWongameMenus()
    {
        if (enemySpawner.boss == enemySpawner.sirenBoss && bossRomanced)
            menuButtons.winMenuSiren.SetActive(true);
        else if (enemySpawner.boss == enemySpawner.vampireBoss && bossRomanced)
            menuButtons.winMenuVampire.SetActive(true);
        else if (enemySpawner.isKinnara && bossRomanced)
            menuButtons.winMenuKinnara.SetActive(true);
        else
            menuButtons.winMenu.SetActive(true);

        enemySpawner.DestroyBoss(resumeDefault: false);
        menuButtons.ResetDialogueIndex();
    }

    void SetUpBattle()
    {
        if (enemyHP == null) enemyHP = enemy.GetComponent<SimpleHealth>();
        playerHP = player.GetComponent<SimpleHealth>();
        StartCoroutine(ChangeBattleState(0f, BattleState.PLAYERTURN, "BattleSystem"));
    }

    public IEnumerator EndPlayerTurn()
    {
        if (clickedEndTurn) yield break;

        clickedEndTurn = true;

        StartCoroutine(ChangeBattleState(.4f, BattleState.ENDPLAYERTURN, "BattleSystem"));

        if (enemyHP.dead()) yield break;
        yield return new WaitForSeconds(.5f);
    }

    public IEnumerator EndEnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(ChangeBattleState(0f, BattleState.ENDENEMYTURN, "BattleSystem"));
    }

    public IEnumerator ChangeBattleState(float delay, BattleState b, string whichScriptIsFrom)
    {
        yield return new WaitForSeconds(delay);
        state = b;
        Debug.LogWarning("The current state is " + b + " (" + whichScriptIsFrom + ")");
    }
}
