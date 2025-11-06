using NUnit.Framework;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Animation : MonoBehaviour
{
    public Animator animator;
    GameObject cardmanager;
 
    ActionsKnight knight;
    ActionsChemist chemist;
    ActionsWizzard wiz;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponentInChildren<Animator>();
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
                break;
            case StatusEffect.VULNERABLE: // increases the dmg received by 50%
                //TriggerBounce();
                break;
            case StatusEffect.WEAK:
                TriggerWeak();
                break;
            default:
                TriggerBounce();
                break;
        }

    }

    public void TriggerBounce()
    {
        animator.SetTrigger("Bounce");
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void TriggerShaking()
    {
        animator.SetTrigger("Shake");
    }

    public void TriggerWeak()
    {
        animator.SetTrigger("Weak");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
