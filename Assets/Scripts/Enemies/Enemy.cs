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
    bool isAngry;
    bool angryHealth;
    string enemyName;
    [SerializeField] bool selectionUsed;
    StatusEffects EnemyStatus;
    StatusEffects PlayerStatus;
    Animation anim;
  
    void Awake()
    {
        enemyName = this.transform.parent.name;
        playerObject = GameObject.FindWithTag("Player");
        system = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        player = playerObject.GetComponent<SimpleHealth>();
        nextAttackUI = GameObject.Find("NextAttack").GetComponent<NextAttackUI>();

        system.enemy = this.gameObject;
        system.enemyHP = this.gameObject.GetComponent<SimpleHealth>();
        EnemyStatus = this.gameObject.GetComponent<StatusEffects>();
        PlayerStatus = player.GetComponent<StatusEffects>();
        nextAttackUI.ShowNextAttack();

        anim = this.gameObject.GetComponent<Animation>();

     

    }
     
    // Update is called once per frame
    void Update()
    {
        if (system.state == BattleState.PLAYERTURN && selectionUsed) 
        {
            actionSelector = Random.Range(0, 6); // remember that range the last digit is ignored in slecetion 
            nextAttackUI.UpdateNextAttackUI(actionSelector);
            setAttkDmg();
            if (isAngry == true)
            {
                Angry();
            }
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
            if (PlayerStatus.currentStatus == StatusEffect.IVINCIBLE)
            {
                attkDmg = 0;
            }
            selectionUsed = true;
            Action();
        }
        else
        {
            StartCoroutine(system.EndEnemyTurn());
        }
    }

    void setAttkDmg()
    {
        switch (enemyName)
        {
            case "KinnaraEnemy3(Clone)":
            case "SirenEnemy2(Clone)":
                attkDmg = Random.Range(7, 13);
                break;
            case "KinnaraEnemy2(Clone)":
            case "SirenEnemy1(Clone)":
                attkDmg = Random.Range(5, 11);
                break;
            case "KinnaraEnemy1(Clone)":
            case "SirenEnemy3(Clone)":
                attkDmg = Random.Range(3, 9);
                break;
            case "KinnaraBoss(Clone)": 
            case "SirenBoss(Clone)":
            case "VampireBoss(Clone)":
                attkDmg = Random.Range(9, 15);
                break;
        }
    }

    void Action()
    {
        doingS = true;
        anim.TriggerAttack();

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

    #region basic enemy attacks 
    
    //Sword
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

    //Monster battle face
    void stanceUp()
    {
        attkDmg += Random.Range(1, 3);
        Debug.Log("Stanced Up");
    }

    //Sheild
    void Guard()
    {
        system.enemyHP.shield += 4;
    }

    //Broken sword
    void Weaken()
    {
        PlayerStatus.currentStatus = StatusEffect.WEAK;
    }

    //Broken sheild
    void Vulnerable()
    {
        PlayerStatus.currentStatus = StatusEffect.VULNERABLE;
    }

    #endregion

    #region Boss Functions

    public void Angry()
    {
        Debug.Log("Multiplying attack dmg by 1.45");
        // have to set dmg each round
        float bossDmg = attkDmg * 1.45f;
        attkDmg = (int)bossDmg;
        if (isAngry == false)
        {
            isAngry = true;
        }
    }

    public void AngryHealth()
    {
        Debug.Log("Multiplying health by 1.5");
        float bossHP = system.enemyHP.maxHealth * 1.5f;
        system.enemyHP.maxHealth = (int)bossHP;
        system.enemyHP.health = (int)bossHP;
    }

    #endregion

    void OnDestroy()
    {
        nextAttackUI.HideNextAttack();
    }
}
