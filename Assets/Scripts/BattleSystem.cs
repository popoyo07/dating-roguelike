using UnityEngine;
using System.Collections;

public enum BattleState {
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
} // start run is for starting a new playthrough 

public class BattleSystem : MonoBehaviour
{
    public BattleState state; // the game battle state 
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

    bool runing;

    public bool finalReward;
    public bool bossRomanced;

   
    void Start()
    {
        state = BattleState.DEFAULT;              // change for actual game 
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
            if (playerHP.dead())
            {
                state = BattleState.LOST;
                Debug.Log("Current state is " + state);
                playerHP = null;

            }
            if (enemyHP.dead())
            {
                Debug.Log("bossRomanced: " + bossRomanced);

                if (enemyHP.isBoss && bossRomanced == false)
                {
                    Debug.Log("Regular");
                    state = BattleState.WONGAME;
                    menuButtons.winMenu.SetActive(true);
                    menuButtons.ResetDialogueIndex();
                }
                else if (enemyHP.isBoss && enemySpawner.isSiren == true && bossRomanced == true)
                {
                    Debug.Log("Siren");
                    StartCoroutine(ChangeBattleState(0f, BattleState.REWARD, "WON?"));
                }
                else if (enemyHP.isBoss && enemySpawner.isVampire == true && bossRomanced == true)
                {
                    Debug.Log("Vampire is now mine");
                    StartCoroutine(ChangeBattleState(0f, BattleState.REWARD, "WON?"));
                }
                else if (enemyHP.isBoss && enemySpawner.isKinnara == true && bossRomanced == true)
                {
                    Debug.Log("isKinnara");
                    StartCoroutine(ChangeBattleState(0f, BattleState.REWARD, "WON?"));
                }
                else if (!enemyHP.isBoss && state != BattleState.REWARD && !finalReward)
                {
                    StartCoroutine(ChangeBattleState(1.5f, BattleState.REWARD, "BattleSystem"));

                }

                if (rewards.pickedReward)
                {
                    Debug.Log("picked a rewuard");
                }
  
                if (chooseRoom.chosenRoom)
                {
                    StartCoroutine(ChangeBattleState(0f, BattleState.WON, "BattleSystem"));
                    enemyHP = null;
                    moveA = true;
                    moveB = true;
                    Debug.Log("Current state is " + state);
                }
            }
        }

        // simple remove from screen when is not player's turn 
        switch (state)  // maybe can be donone on separate script and handle all the UI elements 
        {
            case BattleState.START:
                StartCoroutine(ChangeBattleState(.2f, BattleState.PLAYERTURN, "BattleSystem"));
                break;

            case BattleState.WON: 
                if (!runing)
                {
                    rewards.pickedReward = false;
                    chooseRoom.chosenRoom = false ;
                    runing = true;
                    StartCoroutine(ChangeBattleState(2.5f, BattleState.DEFAULT, "BattleSystem"));

                }
                break;

            case BattleState.PLAYERTURN: 
               if (runing)
                {
                    runing = false;
                }
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
                enemySpawner.DestroyBoss(resumeDefault: false); // do not play default music
                break;

            case BattleState.WONGAME:
                if (enemySpawner.boss == enemySpawner.sirenBoss && bossRomanced)
                {
                    menuButtons.winMenuSiren.SetActive(true);
                    enemySpawner.DestroyBoss(resumeDefault: false); // do not play default music
                }
                else if (enemySpawner.boss == enemySpawner.vampireBoss && bossRomanced)
                {
                    menuButtons.winMenuVampire.SetActive(true);
                    enemySpawner.DestroyBoss(resumeDefault: false); // do not play default music
                }
                else if (enemySpawner.isKinnara && bossRomanced)
                {
                    menuButtons.winMenuKinnara.SetActive(true);
                    enemySpawner.DestroyBoss(resumeDefault: false); // do not play default music
                }
                else
                {
                    menuButtons.winMenu.SetActive(true);
                    enemySpawner.DestroyBoss(resumeDefault: false); // do not play default music
                }
                menuButtons.ResetDialogueIndex();
                break;
                case BattleState.ENDPLAYERTURN:
                StartCoroutine(ChangeBattleState(1f, BattleState.ENEMYTURN, "BattleSystem"));

                break;
        }

    }
    bool clickedEndTurn;
    void SetUpBattle()
    {
        if (enemyHP == null)
        {
            enemyHP = enemy.GetComponent<SimpleHealth>();
        }
        playerHP = player.GetComponent<SimpleHealth>(); // get simple helath 
        StartCoroutine(ChangeBattleState(0f, BattleState.PLAYERTURN, "BattleSystem"));
    }

    public IEnumerator EndPlayerTurn()
    {
        if (clickedEndTurn)
        {
           
            yield break;
        }
        clickedEndTurn = true;

        StartCoroutine(ChangeBattleState(.4f, BattleState.ENDPLAYERTURN, "BattleSystem"));

        if (enemyHP.dead())
        {
            yield break;
        }
        yield return new WaitForSeconds(.5f);

        //  StartCoroutine(ChangeBattleState(1f, BattleState.ENEMYTURN, "BattleSystem")); // delay a little so everything else can be run 
    }

    public IEnumerator EndEnemyTurn()
    {
        yield return new WaitForSeconds(1f); // wait some time to switch back to player turn 
        StartCoroutine(ChangeBattleState(0f, BattleState.ENDENEMYTURN, "BattleSystem"));
    }

    bool running = false;

    public IEnumerator ChangeBattleState(float delay, BattleState b, string whichScriptIsFrom)
    {
        yield return new WaitForSeconds(delay);
        state = b;
        Debug.LogWarning(" The current state is " + b + " " + whichScriptIsFrom);
    }
}