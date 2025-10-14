using System.Collections;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponentInChildren<Animator>();


    }

    public void TriggerBounce()
    {
        animator.SetTrigger("Bounce");
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
