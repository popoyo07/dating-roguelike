using UnityEngine;
using System.Collections;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject player;
    public GameObject enemy;

    public SimpleHealth enemyHP;
    public SimpleHealth playerHP;

    private GameObject endTurnB;
    public int turnCounter = 0; 

    void Start()
    {
        endTurnB = GameObject.Find("EndTurn");
        state = BattleState.START;
        Debug.Log("Current state is " + state);
        SetUpBattle();

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
                state = BattleState.WON;
                Debug.Log("Current state is " + state);
                enemyHP = null;
            }
        }


        // simple remove from screen when is not player's turn 
        switch (state)  // maybe can be donone on separate script and handle all the UI elements 
        {
            case BattleState.PLAYERTURN: 
                endTurnB.SetActive(true);
                turnCounter++;
                break;
            default:
                endTurnB.SetActive(false);
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

    public void EndPlayerTurn()
    {
        state = BattleState.ENEMYTURN;
        Debug.Log("Current state is " + state);
    }

    public IEnumerator EndEnemyTurn()
    {
        yield return new WaitForSeconds(1f); // wait some time to switch back to player turn 
        state = BattleState.PLAYERTURN ;
        Debug.Log("Current state is " + state);
    }


}
