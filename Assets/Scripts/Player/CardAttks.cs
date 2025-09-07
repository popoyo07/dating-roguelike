using Unity.VisualScripting;
using UnityEngine;

public class CardAttks : Cards
{
    [Header("Double Attk DMG")]
    [Range(1, 5)]
    public int doubleAttk;
    
    [Header("Single Attk DMG")]
    [Range(1, 5)]
    public int singleAttk;
    
    [Header("Single Shield")]
    [Range(1,5)]
    public int singleShield;

    public void SingleShield()
    {
        GenerateShield(singleShield);
    }

    public void AttackOnce()
    {
        attkAmmount = 2;
        Debug.Log("Attk should be " + singleAttk);
        GenerateAttk();
    }


    public void AttackTwice()
    {
        attkAmmount = 2;
        Debug.Log("Attk should be " + doubleAttk);

        GenerateAttk();
        GenerateAttk();
    }
    
}
