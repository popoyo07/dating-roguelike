using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleHealth : MonoBehaviour
{
    [Range(1,100)]
    public int maxHealth;
    public int health;

    public int shield;
    public bool dead;
    public HealthBar healthBar;
    private CardActionsCharacter1 attackManager;
    private BattleSystem battleSystem;
    private void Awake()
    {
        health = maxHealth;
        attackManager = GameObject.Find("CardManager").GetComponent<CardActionsCharacter1>();
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        healthBar = this.gameObject.GetComponent<HealthBar>();
        if (gameObject.CompareTag("Enemy"))
        { 
            if (battleSystem != null)
            {
                battleSystem.enemy = gameObject;
            }
            if (attackManager != null)
            {
                attackManager.enemy = gameObject;
            }
        } else if (gameObject.CompareTag("Player"))
        {
            if (battleSystem != null)
            {
                battleSystem.player = gameObject; // assign this game object to 
            }
        }
    }
    private void Update()
    {
        if (health <= 0)
        {
            dead = true;
            Debug.Log("dead eaqueals " + dead);
        }
        if (dead && battleSystem.state == BattleState.WON) 
        {
            StartCoroutine(destroyCharacer());
        }
    }
    IEnumerator destroyCharacer()
    {
        yield return new WaitForSeconds(1);
            Destroy(transform.parent.gameObject);
    }
    public void ReceiveDMG(int dmg) 
    {
        if (shield > 0)
        {
            dmg = dmg -shield;
        }
        if (shield <= 0 && dmg != 0)
        {
            health = health - dmg;
        }
        Debug.Log("Remeining health is " +  health);
        healthBar.UpdateHealth();
    }

    private void FixedUpdate()
    {
        // if healed, character's health cannot be more than their assigned max health 
        if (health > maxHealth)
        {
            health = maxHealth;
        } 
        
    }

    public void RecoverHP(int hp)
    {
        health += hp;
        healthBar.UpdateHealth();
    }

}
