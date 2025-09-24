using UnityEngine;

public class Cards : MonoBehaviour
{
    public BattleSystem battleSystem;
    public int attkAmmount;
    public GameObject enemy;
    public GameObject player;// assigned from somewhere else 
    public DeckManagement deckManagement;
    public EnergySystem energy; // reference enrgy system 
    public int xtStrenght; // used for whenever you want to temporarily increas or deacrease player dmg
    private SimpleHealth playerHp;
 
    public void GenerateAttk()
    {

        Debug.Log ("Attk is " +  attkAmmount);

        // create logic to attack enemy 
        if(enemy != null)
        {
            int dmg = attkAmmount + xtStrenght;
            if (dmg < 0) dmg = 0;

            enemy.GetComponent<SimpleHealth>().ReceiveDMG(dmg);
            Debug.Log("Player does " + dmg + "DMG to the Enemy");

        }else
        {
            Debug.Log("Enemy null");
        }
    }

    public void GenerateShield(int shield)
    {
        // genreagte shield logic 
        playerHp = player.GetComponent<SimpleHealth>();
        playerHp.shield += shield;
        Debug.Log("Shield genereated " + playerHp.shield + " of shield");
    }

    public void DiscardCards(string cardUsed) // button actions 
    {
        deckManagement.discardedCards.Add(cardUsed);
    }


}
