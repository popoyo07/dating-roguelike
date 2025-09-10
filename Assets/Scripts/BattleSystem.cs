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
    STARTRUN ,
    DIALOGUE} // start run is for starting a new playthrough 
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject player;
    public GameObject enemy;

    public SimpleHealth enemyHP;
    public SimpleHealth playerHP;

    private GameObject endTurnB;
    public int turnCounter = 0;

    // Corinne told me to add this 
    public bool moveA;
    public bool moveB;
    public bool moveC;

    void Start()
    {
        endTurnB = GameObject.Find("EndTurn");
        state = BattleState.DEFAULT;              // change for actual game 
        Debug.Log("Current state is " + state);
        StartCoroutine(assignHP());

    }
    IEnumerator assignHP()
    {
        yield return new WaitUntil(() => enemy != null && player != null);

        enemyHP = enemy.GetComponent<SimpleHealth>();
        playerHP = player.GetComponent<SimpleHealth>();
        SetUpBattle();

    }
    private void Update()
    {
        if (playerHP != null && enemyHP != null)
        {
            if (playerHP.dead == true)
            {
                state = BattleState.LOST;
                Debug.Log("Current state is " + state);
                playerHP = null;

            }
            if (enemyHP.dead == true) 
            {
                state = BattleState.WON;
                Debug.Log("Current state is " + state);
                enemyHP = null;
                moveC = true;
            }

            if (moveC == true)
            {
                moveA = true;
                moveB = true;
                moveC = false;
            }
        }
        else if (enemyHP == null) Debug.LogWarning("lost enemy hp");

            // need to claen up later on 
            // simple remove from screen when is not player's turn 
            switch (state)  // maybe can be donone on separate script and handle all the UI elements 
            {
                case BattleState.START:
                StartCoroutine(DelaySwitchState(1f, BattleState.PLAYERTURN));
                break;
            case BattleState.WON:
                    
                    StartCoroutine(DelaySwitchState(2.5f, BattleState.START));
                    break;
                case BattleState.PLAYERTURN:


                    break;
                case BattleState.STARTRUN:
                    StartCoroutine(DelaySwitchState(1, BattleState.START));
                    break;

                case BattleState.DEFAULT:


                    break;
                case BattleState.LOST:

                    break;
            }

    }
    void SetUpBattle()
    {
        
        enemyHP = enemy.GetComponent<SimpleHealth>();
        playerHP = player.GetComponent<SimpleHealth>(); // get simple gelath 
        state = BattleState.PLAYERTURN;
        Debug.Log("Current state is " + state);

    }

    public IEnumerator EndPlayerTurn()
    {
        state = BattleState.ENDPLAYERTURN;
        yield return new WaitForSeconds(1f);  // delay a little so everything else can be run 
        state = BattleState.ENEMYTURN;
        Debug.Log("Current state is " + state);
    }

    public IEnumerator EndEnemyTurn()
    {
        yield return new WaitForSeconds(1f); // wait some time to switch back to player turn 
        state = BattleState.PLAYERTURN ;
        Debug.Log("Current state is " + state);
    }

    IEnumerator DelaySwitchState(float delay, BattleState b)
    {
        yield return new WaitForSeconds(delay);
        state = b;
        
    }

}
