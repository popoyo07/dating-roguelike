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
    public BattleState state;
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

    public bool secondEncounter;
    MenuButtons menuButtons;

    void Start()
    {
        endTurnB = GameObject.Find("EndTurn");
        state = BattleState.DEFAULT;              // change for actual game 
        Debug.Log("Current state is " + state);
        SetUpBattle();

        rewards = GameObject.FindWithTag("RewardsM").GetComponent<Rewards>();
        chooseRoom = GameObject.FindWithTag("RoomManager").GetComponent<ChooseRoom>();
        menuButtons = GameObject.FindWithTag("Canvas").GetComponent<MenuButtons>();
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
                if (enemyHP.isBoss && secondEncounter)
                { // win should be added here 
                    state = BattleState.WONGAME;
                    SceneManager.LoadScene("MainMenu");
                    menuButtons.ResetDialogueIndex();
                }
           

                rewards.openRewardsPop = true;
                rewards.ShowRewardOptions();

                if (rewards.pickedReward)
                {
                    rewards.pickedReward = false;
                    rewards.openRewardsPop = false;
                    chooseRoom.openRoomPop = true;
                    chooseRoom.ShowRoomOptions();
  
                }
  
                if (chooseRoom.chosenRoom)
                {
                    rewards.openRewardsPop = false;
                    state = BattleState.WON;
                    enemyHP = null;
                    moveA = true;
                    moveB = true;
                    chooseRoom.chosenRoom = false;
                    chooseRoom.openRoomPop = false;
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
                StartCoroutine(DelaySwitchState(.2f, BattleState.PLAYERTURN, "BattleSystem"));

                break;

            case BattleState.WON: 
                
                StartCoroutine(DelaySwitchState(1.5f, BattleState.DEFAULT, "BattleSystem"));
                break;
            case BattleState.PLAYERTURN: 
               
              
                break;
            case BattleState.STARTRUN:
                StartCoroutine(DelaySwitchState(.2f, BattleState.START, "BattleSystem"));
                    break;
                
            case BattleState.ENDENEMYTURN:
                StartCoroutine(DelaySwitchState(.1f, BattleState.PLAYERTURN, "BattleSystem"));


                break;
            case BattleState.LOST:
                menuButtons.loseMenu.SetActive(true);
                //SceneManager.LoadScene("MainMenu");
                break;
        }

    }

    void SetUpBattle()
    {
        if (enemyHP == null)
        {
            enemyHP = enemy.GetComponent<SimpleHealth>();
        }
        playerHP = player.GetComponent<SimpleHealth>(); // get simple helath 
        StartCoroutine(DelaySwitchState(0f, BattleState.PLAYERTURN, "BattleSystem"));

    }

    public IEnumerator EndPlayerTurn()
    {
        StartCoroutine(DelaySwitchState(0f, BattleState.ENDPLAYERTURN, "BattleSystem"));

        if (enemyHP.dead())
        {
            yield break;
        }

        StartCoroutine(DelaySwitchState(1f, BattleState.ENEMYTURN, "BattleSystem")); // delay a little so everything else can be run 
    }

    public IEnumerator EndEnemyTurn()
    {
        yield return new WaitForSeconds(1f); // wait some time to switch back to player turn 
        StartCoroutine(DelaySwitchState(0f, BattleState.ENDENEMYTURN, "BattleSystem"));
    }

    public IEnumerator DelaySwitchState(float delay, BattleState b, string whichScriptIsFrom)
    {
        yield return new WaitForSeconds(delay);
        state = b;
        Debug.LogWarning(" The current state is " + b + " " + whichScriptIsFrom);
        
    }

}
