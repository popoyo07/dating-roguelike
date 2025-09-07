using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject player;
    public GameObject enemy;

    public SimpleHealth enemyHP;
    public SimpleHealth playerHP    ;
    void Start()
    {
        state = BattleState.START;
        Debug.Log("Current state is " + state);
        SetUpBattle();

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

    private void FixedUpdate()
    {
        if (playerHP != null && enemyHP != null)
        {
            if (playerHP.dead())
            {
                state = BattleState.LOST;
                Debug.Log("Current state is " + state);

            }
            if (enemyHP.dead())
            {
                state = BattleState.WON;
                Debug.Log("Current state is " + state);
            }
        }
    }
}
