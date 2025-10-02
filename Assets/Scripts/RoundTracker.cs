using UnityEngine;

// later will need another check that makes a counter for enemy turn and not only player turm

public class RoundTracker : MonoBehaviour
{
    public int playerRounds;
    public int enemyRounds;
    public BattleSystem BSystem;
    bool playerTurn;
    bool enemyTurn;
    private void Start()
    {
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }

    private void Update()
    {
        if (!playerTurn && BSystem.state == BattleState.PLAYERTURN) 
        {
            playerTurn = true;
            playerRounds++;
        }
        else if (!enemyTurn && BSystem.state == BattleState.ENEMYTURN)
        { 
            enemyTurn = true;
            enemyRounds++;
        }

        if (playerTurn && BSystem.state == BattleState.WON) 
        {
            playerTurn = false;
            playerRounds = 0; 
        }
    }
}
