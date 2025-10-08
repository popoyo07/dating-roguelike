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
    public SimpleHealth playerHp;
    
    
    public bool attkDone;
    public bool turnBuff;
    public RoundTracker roundTracker;
    public StatusEffects pStatus;
    public StatusEffect LastStatus;
    
    // stores the 
    public Dictionary<string, int> cardEnergyCost = new Dictionary<string, int>();

    public Dictionary<string, Action> cardAttaks = new Dictionary<string, Action>();


    [Header("Small heal hability")]
    [Range(5, 10)][SerializeField] public int smallHealing;
    [Range(1, 3)][SerializeField] public int smallHealingECost;

    [Header("Big heal hability")]
    [Range(2, 3)][SerializeField] public int healingMultiplier;
    [Range(1, 3)][SerializeField] public int bigHealingECost;
    public void GenerateAttk(StatusEffect attackerState)
    {

        Debug.Log ("Attk is " +  attkAmmount);

        // create logic to attack enemy 
        if(enemy != null)
        {
            int dmg = attkAmmount + xtStrenght;
            if (dmg < 0) dmg = 0;

            enemy.GetComponent<SimpleHealth>().ReceiveDMG(dmg, attackerState);
            Debug.Log("Player does " + dmg + "DMG to the Enemy");

        }else
        {
            Debug.Log("Enemy null");
        }
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
        player.GetComponent<SimpleHealth>().RecoverHP(smallHealing);
    }

    public void ConsumeEnergy(int cost) // unsert enery cost in cost 
    {
        energy.energyCounter -= cost;
    }

    public void BigHealing()
    {
        int biggerHeal = smallHealing * healingMultiplier;
        ConsumeEnergy(bigHealingECost);
        player.GetComponent<SimpleHealth>().RecoverHP(biggerHeal);
    }
    public void LoveyDoveyLogic()  // do nothing type of cards 
    {
        ConsumeEnergy(1);
        Debug.Log("Lovely");
    }
    public void LoveyDoveyLogic2() // needs to do something ? 
    {
        ConsumeEnergy(1);
        Debug.Log("Lovely2");
    }
    
    public void LoveyDoveyLogic3() // needs to do something ? 
    {
        ConsumeEnergy(1);
        Debug.Log("Lovely2");
    }
        public void LoveyDoveyLogic4() // needs to do something ? 
    {
        ConsumeEnergy(1);
        Debug.Log("Lovely2");
    }

}
