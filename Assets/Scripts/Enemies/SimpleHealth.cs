using Unity.VisualScripting;
using UnityEngine;

public class SimpleHealth : MonoBehaviour
{
   [Range(1,100)]
   public int health;

    public int shield;

    private GameObject AttackManager;
    private void Start()
    {
        AttackManager = GameObject.Find("CardManager"); 
        AttackManager.GetComponent<CardAttks>().enemy = gameObject;
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
    }

}
