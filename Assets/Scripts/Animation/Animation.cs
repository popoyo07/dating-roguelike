using System.Collections;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public GameObject enemy;
    public GameObject vfx;

    public Animator EnemyAnim;
    public Animator ExtraAnim;

    SpriteRenderer enemySprite;

    GameObject cardmanager;
 
    ActionsKnight knight;
    ActionsChemist chemist;
    ActionsWizzard wiz;

    private void Awake()
    {
        EnemyAnim = enemy.GetComponent<Animator>();
        ExtraAnim = vfx.GetComponent<Animator>();
        enemySprite = vfx.GetComponent<SpriteRenderer>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemySprite.enabled = false;

        cardmanager = GameObject.Find("CardManager");
        Debug.Log("it is assigning enemy for the card managers from the anim script");
        knight = cardmanager.GetComponent<ActionsKnight>();
        knight.enemy = this.gameObject;
        chemist = cardmanager.GetComponent<ActionsChemist>();
        chemist.enemy = this.gameObject;
        wiz = cardmanager.GetComponent<ActionsWizzard>();
        wiz.enemy = this.gameObject;
       
    }

    public void AnimationTrigger(StatusEffect attackerStatus)
    {

        switch (attackerStatus)
        {
            case StatusEffect.SHIELDIGNORED:
                TriggerShaking();
                BeingAttacked();
                break;
            case StatusEffect.VULNERABLE:
                TriggerShaking();
                BeingAttacked();
                break;
            case StatusEffect.WEAK:
                TriggerWeak();
                //BeingAttacked();
                break;
            default:
                TriggerBounce();
                BeingAttacked();
                break;
        }

    }

    public void BeingAttacked()
    {
        enemySprite.enabled = true;

        ExtraAnim.SetTrigger("BeingAttacked");

    }

    public void TriggerBounce()
    {
        EnemyAnim.SetTrigger("Bounce");
    }

    public void TriggerAttack()
    {
        EnemyAnim.SetTrigger("Attack");
    }

    public void TriggerShaking()
    {
        EnemyAnim.SetTrigger("Shake");
    }

    public void TriggerWeak()
    {
        EnemyAnim.SetTrigger("Weak");
    }
    void Update()
    {
        
    }
}
