using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleHealth : MonoBehaviour
{
    [Range(1,100)]
    public int maxHealth;
    public int health;

    public int shield;
    public bool dead() // changes between true and false dpending on object's HP
    {
        if (health <= 0)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
    public HealthBar healthBar;
    private ActionsKnight attackManager;
    private BattleSystem battleSystem;
    private void Awake()
    {
        health = maxHealth;
        attackManager = GameObject.Find("CardManager").GetComponent<ActionsKnight>();
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
    public void ReceiveDMG(int dmg) 
    {
      
        if (dmg != 0)
        {
            Debug.Log("player has " + shield);
            Debug.Log("Dmg before shield is " + dmg);
            int dmgDone = dmg - shield;
            shield -= dmg;
            Debug.Log("Damage done to is " + dmgDone);
            if (dmgDone > 0)
            {
                health = health - dmgDone;

            }
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
        dead();
    }

    public void RecoverHP(int hp)
    {
        health += hp;
        healthBar.UpdateHealth();
    }

}
