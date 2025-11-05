using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject playerObject;
    public BattleSystem system;
    public NextAttackUI nextAttackUI;
    public int actionSelector;
    SimpleHealth player;
    public int attkDmg;
    public int shiledAdded;
    bool doingS;
    public bool stuned; // change to true to stunn enemy    
    [SerializeField] bool selectionUsed;
    StatusEffects EnemyStatus;
    StatusEffects PlayerStatus;
    Animation animation;
  
    void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
        system = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        player = playerObject.GetComponent<SimpleHealth>();
        nextAttackUI = GameObject.Find("NextAttack").GetComponent<NextAttackUI>();

        system.enemy = this.gameObject;
        system.enemyHP = this.gameObject.GetComponent<SimpleHealth>();
        EnemyStatus = this.gameObject.GetComponent<StatusEffects>();
        PlayerStatus = player.GetComponent<StatusEffects>();
        nextAttackUI.ShowNextAttack();

        animation = this.gameObject.GetComponent<Animation>();

     

    }
     
    // Update is called once per frame
    void Update()
    {
        if (system.state == BattleState.PLAYERTURN && selectionUsed) 
        {
            actionSelector = Random.Range(0, 6); // remember that range the last digit is ignored in slecetion 
            if (nextAttackUI.isVisible == false)
            {
                nextAttackUI.ShowNextAttack();
            }
            nextAttackUI.UpdateNextAttackUI(actionSelector);
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
        animation.TriggerAttack();

        switch (actionSelector) // randomly select an enemy attack
        {
            case 0:
                regular();
                Debug.Log("Single Attack");
                break;
            case 1:
                StartCoroutine(doubleAttk());
                Debug.Log("Double Attack");
                break;
            case 2:
                stanceUp();
                Debug.Log("Stance Up");
                break;
            case 3:
                Guard();
                Debug.Log("Guard");
                break;
            case 4:
                Weaken();
                Debug.Log("Weaken");
                break;
            case 5: 
                Vulnerable();
                Debug.Log("Vulnerable");
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

        player.ReceiveDMG(attkDmg, EnemyStatus.currentStatus);
           Debug.Log("RegularAttk");
    }

    IEnumerator doubleAttk()
    {

        player.ReceiveDMG(attkDmg, EnemyStatus.currentStatus);
        yield return new WaitForSeconds(.7f);

        player.ReceiveDMG(attkDmg, EnemyStatus.currentStatus);

        Debug.Log("DoubleAttk");
    }

    void stanceUp()
    {
        attkDmg += Random.Range(1, 3);
        Debug.Log("Stanced Up");
    }

    void Guard()
    {
        system.enemyHP.shield += 4;
    }

    void Weaken()
    {
        PlayerStatus.currentStatus = StatusEffect.WEAK;
    }

    void Vulnerable()
    {
        PlayerStatus.currentStatus = StatusEffect.VULNERABLE;
    }

    void Angery()
    {
        float bossDmg = attkDmg * 1.25f;
        attkDmg = (int)bossDmg;
        float bossHP = system.enemyHP.maxHealth * 1.5f;
        system.enemyHP.maxHealth = (int)bossHP;
    }

    void OnDestroy()
    {
        nextAttackUI.HideNextAttack();
    }
}
