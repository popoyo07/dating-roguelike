using UnityEngine;

public class Cards : MonoBehaviour
{
    public BattleSystem battleSystem;
    public int attkAmmount;
    public GameObject enemy;
    public GameObject player;
    public DeckManagement deckManagement;
    public EnergySystem energy; // reference enrgy system 


    public int shieldAmmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        deckManagement = gameObject.GetComponent<DeckManagement>();
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        player = GameObject.FindWithTag("Player");
    }
    public void GenerateAttk()
    {

        Debug.Log ("Attk is " +  attkAmmount);

        // create logic to attack enemy 
        if(enemy != null)
        {
            enemy.GetComponent<SimpleHealth>().ReceiveDMG(attkAmmount);
            Debug.Log("Player does " + attkAmmount + "DMG to the Enemy");

        }else
        {
            Debug.Log("Enemy null");
        }
    }

    public void GenerateShield(int shield)
    {
        // genreagte shield logic 
        shieldAmmount = shield;
        Debug.Log("Shield genereated " + shieldAmmount + " of shield");
    }

    public void DiscardCards(string cardUsed) // button actions 
    {
        deckManagement.discardedCards.Add(cardUsed);
    }


}
