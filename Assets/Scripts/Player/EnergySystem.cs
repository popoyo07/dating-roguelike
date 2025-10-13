using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public int energyCounter;
    public int maxEnergy;
    BattleSystem bSystem;
   
    void Start()
    {
        energyCounter = 3;
        bSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (bSystem.state)
        {
            case BattleState.ENDPLAYERTURN:
                ResetCounter();

                break;
            case BattleState.WON:
                ResetCounter();

                break;
        }

    }
    void ResetCounter() // reset how much energy we have 
    {
        if (energyCounter != maxEnergy) 
        {
            energyCounter = 3;
        }
    }

    public void IncreaseEnergy(int amount)
    {
        energyCounter += amount;
    }
}
