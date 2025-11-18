using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


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

    private GameObject endTurnB;
    public int turnCounter = 0;

    Rewards rewards;
    ChooseRoom chooseRoom;
    public bool moveA;
    public bool moveB;

    public EnemySpawner enemySpawner;

    public DialogueUI dialogueUI;
    public CardUI cardUI;

    public bool secondEncounter;
    MenuButtons menuButtons;
    bool runing;

    public bool finalReward;
    public bool bossRomanced;

    [SerializeField] AssignCard card;
    void Start()
    {
        endTurnB = GameObject.Find("EndTurn");
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
                else if (enemyHP.isBoss && secondEncounter && enemySpawner.isSiren == true && bossRomanced == true)
                {
                    Debug.Log("Siren");
                    StartCoroutine(ChangeBattleState(0f, BattleState.REWARD, "WON?"));
                    //menuButtons.winMenuSiren.SetActive(true);
                    //menuButtons.ResetDialogueIndex();
                }
                else if (enemyHP.isBoss && secondEncounter && enemySpawner.isVampire == true && bossRomanced == true)
                {
                    Debug.Log("Vampire is now mine");
                    StartCoroutine(ChangeBattleState(0f, BattleState.REWARD, "WON?"));
                    //state = BattleState.WONGAME;
                    //menuButtons.winMenuVampire.SetActive(true);
                    //menuButtons.ResetDialogueIndex();
                }
                else if (enemyHP.isBoss && secondEncounter && enemySpawner.isKinnara == true && bossRomanced == true)
                {
                    Debug.Log("isKinnara");
                    StartCoroutine(ChangeBattleState(0f, BattleState.REWARD, "WON?"));
                    //menuButtons.winMenuKinnara.SetActive(true);
                    //menuButtons.ResetDialogueIndex();
                }
                else if (!enemyHP.isBoss && state != BattleState.REWARD && !finalReward)
                {
                    StartCoroutine(ChangeBattleState(1.5f, BattleState.REWARD, "BattleSystem"));

                }

                if (rewards.pickedReward)
                {
                    // rewards.pickedReward = false;
                    Debug.Log("picked a rewuard");
                    // rewards.openRewardsPop = false;
                    //  chooseRoom.openRoomPop = true;
                }
  
                if (chooseRoom.chosenRoom)
                {
                   // rewards.openRewardsPop = false;
                    //state = BattleState.WON;
                    StartCoroutine(ChangeBattleState(0f, BattleState.WON, "BattleSystem"));
                    enemyHP = null;
                    moveA = true;
                    moveB = true;
                    //chooseRoom.chosenRoom = false;
                  //  chooseRoom.openRoomPop = false;
                    Debug.Log("Current state is " + state);
                    secondEncounter = true;
                }
            }
        }

        // need to claen up later on 
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
                //SceneManager.LoadScene("MainMenu");
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
        if (clickedEndTurn || !card.cardSet)
        {
            yield break;
        }
        clickedEndTurn = true;
        StartCoroutine(ChangeBattleState(0f, BattleState.ENDPLAYERTURN, "BattleSystem"));

        if (enemyHP.dead())
        {
            yield break;
        }

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
        if (running) 
        {
            yield break;
        }
        running = true;
        yield return new WaitForSeconds(delay);
        state = b;
        running = false;
        Debug.LogWarning(" The current state is " + b + " " + whichScriptIsFrom);
        
    }

}