using UnityEngine;

public class Cards : MonoBehaviour
{
    
    public int attkAmmount;
    public GameObject enemy;
   
    public int shieldAmmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public void GenerateAttk()
    {
        //Debug.Log("Ammount in input " + attackDamage);
       // attkAmmount = attackDamage;
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
}
