using System.Xml.Schema;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SimpleHealth : MonoBehaviour, IDataPersistence
{
    StatusEffects status;
    [Range(1,130)]
    public int maxHealth;
    public int health;
    public int healthBuff;
    public int healthAmount;
    bool isPlayer;
    public int shield;
    public bool isBoss;

    public AudioClip hitSound;
    public AudioClip shieldSound;
    public AudioClip healSound;
    private AudioSource audioSource;
    
    #region Save and Load

    public void LoadData(GameData data)
    {
        Debug.Log("Load data SimpleHealth is running");
        this.healthBuff = data.healthBuff;
    }

    public void SaveData(ref GameData data)
    {
        data.healthBuff = 0;
    }

    #endregion
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
        audioSource = GetComponent<AudioSource>();

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
            isPlayer = false;
        } else if (gameObject.CompareTag("Player"))
        {
            if (battleSystem != null)
            {
                battleSystem.player = gameObject; // assign this game object to 
            }
            isPlayer = true;
        }
        status = gameObject.GetComponent<StatusEffects>();
        if (status == null) 
        {
            status = gameObject.GetComponentInChildren<StatusEffects>();
        }
    }

    private void Start()
    {
        healthAmount = healthBuff * 10;
        maxHealth += healthAmount;
        health = maxHealth;
        healthBar.UpdateMaxHealth();
        healthBar.UpdateHealth();
    }
    public void ReceiveDMG(int dmg, StatusEffect attackerStatus) 
    {
      
        if (dmg != 0)
        {
            int dmgDone;

           
            Debug.Log("Dmg before shield is " + dmg);
            switch (attackerStatus)
            {
                case StatusEffect.SHIELDIGNORED:
                    dmgDone = dmg;
                    break; 
                case StatusEffect.VULNERABLE: // increases the dmg received by 50%
                    dmgDone = (int)((float)dmg * 1.5);
                    Debug.Log("increase calculation " + ((float)dmg * 1.5));
                    Debug.Log("shown dmg before shield calc " + dmgDone);
                    break;
                case StatusEffect.WEAK:
                    dmgDone = (int)((float)dmg * .75f); // lower dmg by 25%
                    break;
                default:
                    dmgDone = dmg - shield;
                    shield -= dmg;
                    audioSource.PlayOneShot(shieldSound);
                    if (shield < 0) { shield = 0; }
                    Debug.Log("Damage done to is " + dmgDone);
                    break;
            }

            
            if (dmgDone > 0)
            {
                health = health - dmgDone;
                audioSource.PlayOneShot(hitSound);
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

        // Reset shield after oponent attacks 
        if(battleSystem.state == BattleState.ENDENEMYTURN && isPlayer)
        {
            shield = 0;
        } 
        else if (battleSystem.state == BattleState.ENDPLAYERTURN && !isPlayer)
        {
            shield = 0;
        }
    }

    public void RecoverHP(int hp) // recover flat hp
    {
        health += hp;
        healthBar.UpdateHealth();
        audioSource.PlayOneShot(healSound);
    }

    public void PercentageRecoverHP(float percent)// recover hp by percentage 
    {
        float addHP = (float)maxHealth * (percent * 0.001f);
        RecoverHP((int)addHP);
        audioSource.PlayOneShot(healSound);
    }
}
