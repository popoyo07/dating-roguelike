using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Animation : MonoBehaviour
{
    public Animator animator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponentInChildren<Animator>();


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
