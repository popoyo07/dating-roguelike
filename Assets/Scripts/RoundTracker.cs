using UnityEngine;

public class RoundTracker : MonoBehaviour
{
    public int Rounds;
    BattleSystem BSystem;
    bool playerTurn;
    private void Start()
    {
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }

    private void Update()
    {
        if (!playerTurn && BSystem.state == BattleState.PLAYERTURN) 
        {
            playerTurn = true;
            Rounds++;
        }

        if (playerTurn && BSystem.state == BattleState.WON) 
        {
            playerTurn = false;
            Rounds = 0; 
        }
    }
}
