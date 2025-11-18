using System.Collections.Generic;
using UnityEngine;
using System;

public class Cards : MonoBehaviour
{
    public BattleSystem battleSystem;
    public int attkAmmount;
    public GameObject enemy;
    public GameObject player;// assigned from somewhere else 
    public DeckManagement deckManagement;
    public EnergySystem energy; // reference enrgy system 
    public int xtStrenght; // used for whenever you want to temporarily increas or deacrease player dmg
    public float multStrenght;
    public SimpleHealth playerHp;
    
    
    public bool attkDone;
    public bool turnBuff;
    public RoundTracker roundTracker;
    public StatusEffects pStatus;
    public StatusEffect LastStatus;

    public AudioClip weakenSound;
    private AudioSource audioSource;

    // stores the 
    public Dictionary<string, int> cardEnergyCost = new Dictionary<string, int>();

    public Dictionary<string, Action> cardAttaks = new Dictionary<string, Action>();


    [Header("Small heal hability")]
    [Range(5, 10)][SerializeField] public int smallHealing;
    [Range(1, 3)][SerializeField] public int smallHealingECost;

    [Header("Big heal hability")]
    [Range(2, 3)][SerializeField] public int healingMultiplier;
    [Range(1, 3)][SerializeField] public int bigHealingECost;

    [Header("Taunt")]
    [Range(1, 10)][SerializeField] public int taunt;
    [Range(1, 3)][SerializeField] public int tauntECost;

    [Header("Pocket Pebble")]
    [Range(1, 10)][SerializeField] public int pocketPebble;
    [Range(1, 3)][SerializeField] public int pocketPebbleECost;

    [Header("Double Attk DMG")]
    [Range(1, 10)][SerializeField] public int doubleAttk;
    [Range(1, 3)][SerializeField] public int doubleAttkECost;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void GenerateAttk(StatusEffect attackerState)
    {

        Debug.Log ("Attk is " +  attkAmmount);

        // create logic to attack enemy 
        if(enemy != null)
        {
            int dmg ;
            if (multStrenght > 0)
            {
                dmg = (int)((float)multStrenght * ((float)attkAmmount + xtStrenght)); 
            } else
            {
                dmg = attkAmmount + xtStrenght;
            }
            if (dmg < 0) dmg = 0;

            enemy.GetComponent<SimpleHealth>().ReceiveDMG(dmg, attackerState);
            enemy.GetComponent<Animation>().AnimationTrigger(attackerState);
            Debug.Log("Player does " + dmg + "DMG to the Enemy");

        }else
        {
            Debug.Log("Enemy null");
        }
        multStrenght = 0;
    }

    public void GenerateShield(int shield)
    {
        // genreagte shield logic 
        if (playerHp == null)
        {
            playerHp = player.GetComponent<SimpleHealth>();
        }
        playerHp.shield += shield;
        Debug.Log("Shield genereated " + playerHp.shield + " of shield");
    }

    public void DiscardCards(string cardUsed) // button actions 
    {
        deckManagement.discardedCards.Add(cardUsed);
    }
    public void SmallHealing() // common card all will have 
    {
        ConsumeEnergy(smallHealingECost);
        playerHp.RecoverHP(smallHealing);
    }

    public void ConsumeEnergy(int cost) // unsert enery cost in cost 
    {
        energy.energyCounter -= cost;
        deckManagement.cardPlayedCount++;
    }

    public void BigHealing()
    {
        int biggerHeal = smallHealing * healingMultiplier;
        ConsumeEnergy(bigHealingECost);
        player.GetComponent<SimpleHealth>().RecoverHP(biggerHeal);
    }
    public void LoveyDoveyLogic()  // (1) Enemy Falls Asleep for 1 Turn 
    {
        ConsumeEnergy(1);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.WEAK;
        audioSource.PlayOneShot(weakenSound);
        Debug.Log("Lovely");
    }
    public void LoveyDoveyLogic2() // (1) Stun enemy for 1 turn
    {
        ConsumeEnergy(1);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.STUN;

        Debug.Log("Lovely2");
    }
    
    public void LoveyDoveyLogic3() // (1) Makes enemy vulnerable for 1 turn
    {
        ConsumeEnergy(1);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.VULNERABLE;
        Debug.Log("Lovely3");
    }
        public void LoveyDoveyLogic4() // needs to do something ? 
    {
        ConsumeEnergy(1);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.WEAK;
        audioSource.PlayOneShot(weakenSound);
        Debug.Log("Lovely4");
    }
        public void LoveyDoveyLogic5() // needs to do something ? 
    {
        ConsumeEnergy(1);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.STUN;
        Debug.Log("Lovely5");
    }
        public void LoveyDoveyLogic6() // needs to do something ? 
    {
        ConsumeEnergy(1);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.VULNERABLE;

        Debug.Log("Lovely6");
    }



    public void PocketPebble()
    {
        int r = UnityEngine.Random.Range(1, 6);
        // 20% chance of happening 
        if (r == 1)
        {
            enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.STUN;

        }
        GenerateAttk(pStatus.currentStatus);

    }


    public void Taunt()
    {
        ConsumeEnergy(tauntECost);
        enemy.GetComponent<StatusEffects>().currentStatus = StatusEffect.WEAK;
        audioSource.PlayOneShot(weakenSound);
        attkAmmount = taunt;
        GenerateAttk(pStatus.currentStatus);

    }

    public void AttackTwice()
    {
        attkAmmount = doubleAttk;
        Debug.Log("Attk should be " + doubleAttk);
        ConsumeEnergy(doubleAttk);
        GenerateAttk(pStatus.currentStatus);
        GenerateAttk(pStatus.currentStatus);

    }

}
