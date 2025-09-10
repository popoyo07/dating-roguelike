using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BattleSystem system; 
    public int randomSelector;
    SimpleHealth player;
    public int attkDmg;
    public int shiledAdded;
    bool doingS;

    void Awake()
    {
         system = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
         player = GameObject.FindWithTag("Player").GetComponent<SimpleHealth>();

        system.enemy = this.gameObject;
        system.enemyHP = this.gameObject.GetComponent<SimpleHealth>();
    }
     
    // Update is called once per frame
    void Update()
    {
        if (system.state == BattleState.ENEMYTURN && !doingS) 
        {
            doingS = true;

            PickAttk();
        } else if (system.state != BattleState.ENEMYTURN)
        {
            doingS = false;
        }

    }

    void PickAttk()
    {
        doingS = true;

        randomSelector = Random.Range(0, 1);
        switch (randomSelector) // randomly select an enemy attack
        {
            case 0:
                regular();
                break;
            case 1: doubleAttk(); 
                break;
            default:
                Debug.Log("Nothing happened");
                break;
        }

    }
    // basic enemy attacks 
    void regular()
    {

        player.ReceiveDMG(attkDmg);
           Debug.Log("RegularAttk");
        StartCoroutine(system.EndEnemyTurn());
    }

    void doubleAttk()
    {

        player.ReceiveDMG(attkDmg);
        player.ReceiveDMG(attkDmg);

        Debug.Log("DoubleAttk");
        StartCoroutine(system.EndEnemyTurn());


    }
}
