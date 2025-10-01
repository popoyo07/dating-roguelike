using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BattleSystem system; 
    public int actionSelector;
    SimpleHealth player;
    public int attkDmg;
    public int shiledAdded;
    bool doingS;
    public bool stuned; // change to true to stunn enemy    
    bool selectionUsed;
    StatusEffects EnemyStatus;
    StatusEffects PlayerStatus;

    void Awake()
    {
         system = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
         player = GameObject.FindWithTag("Player").GetComponent<SimpleHealth>();

        system.enemy = this.gameObject;
        system.enemyHP = this.gameObject.GetComponent<SimpleHealth>();
        EnemyStatus = this.gameObject.GetComponent<StatusEffects>();
        PlayerStatus = player.GetComponent<StatusEffects>();
    }
     
    // Update is called once per frame
    void Update()
    {
        if (system.state == BattleState.PLAYERTURN && selectionUsed) 
        {
            actionSelector = Random.Range(0, 2); // remember that range the last digit is ignored in slecetion 
            attkDmg = Random.Range(5, 11);
            selectionUsed = false;
        }

        if (system.state == BattleState.ENEMYTURN && !doingS) 
        {

            doingS = true;
            StartCoroutine(DelayEndOfTurn(2f));
           
        } else if (system.state != BattleState.ENEMYTURN)
        {
            doingS = false;
        }

    }
    IEnumerator DelayEndOfTurn(float deleay) // add delay so we can run animations and things like that 
    {
        yield return new WaitForSeconds(deleay);
        if (!stuned && !selectionUsed)
        {
            selectionUsed = true;
            Action();
        }
        else
        {
            StartCoroutine(system.EndEnemyTurn());
        }
    }
    void Action()
    {
        doingS = true;

        switch (actionSelector) // randomly select an enemy attack
        {
            case 0:
                regular();
                break;
            case 1:
                StartCoroutine(doubleAttk());
                break;
            default:
                Debug.Log("Nothing happened");
                break;
        }
        StartCoroutine(system.EndEnemyTurn());

    }
    // basic enemy attacks 
    void regular()
    {

        player.ReceiveDMG(attkDmg);
           Debug.Log("RegularAttk");
    }

    IEnumerator doubleAttk()
    {

        player.ReceiveDMG(attkDmg);
        yield return new WaitForSeconds(.7f);

        player.ReceiveDMG(attkDmg);

        Debug.Log("DoubleAttk");



    }
}
