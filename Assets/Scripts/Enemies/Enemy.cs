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
    }
     
    // Update is called once per frame
    void FixedUpdate()
    {
        if (system.state == BattleState.ENEMYTURN && !doingS) 
        {
            PickAttk();
            doingS = true;
        } else
        {
            doingS = false;
        }

    }

    void PickAttk()
    {
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
