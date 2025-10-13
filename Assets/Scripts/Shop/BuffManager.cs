using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private SimpleHealth simpleHealth;
    private CoinSystem coinSystem;
    private EnergySystem energySystem;

    int coins;

    private int maxActiveBuffs = 0;

    public int healthBuff = 10;
    public int healthBuffCost = 2;

    public int energyBuff = 1;
    public int energyBuffCost = 4;

    public int doubleCoinsBuff;
    public int doubleCoinsBuffCost = 5;

    private void Start()
    {
        coinSystem = GameObject.Find("CoinSystem").GetComponent<CoinSystem>();
        simpleHealth = GameObject.Find("SimpleHealth").GetComponent<SimpleHealth>();
        energySystem = GameObject.Find("EnergySystem").GetComponent<EnergySystem>();

        coins = coinSystem.coins;
    }

    private void Update()
    {
        if (simpleHealth.health == 0)
        {
            resetBuffs();
        }

    }
    public void buyHealthBuff()
    {
        if (coins >= healthBuffCost && maxActiveBuffs < 3)
        {
            coins -= healthBuffCost;
            simpleHealth.IncreaseHP(healthBuff);
            Debug.Log("Health added");
            maxActiveBuffs += 1;
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }

    public void buyEnergyBuff()
    {
        if (coins >= energyBuffCost && maxActiveBuffs < 3)
        {
            coins -= energyBuffCost;
            energySystem.IncreaseEnergy(energyBuff);
            Debug.Log("Energy added");
            maxActiveBuffs += 1;
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }

    public void buyExtraCoinsBuff()
    {
        if (coins >= doubleCoinsBuffCost && maxActiveBuffs < 3)
        {
            coins -= doubleCoinsBuffCost;
            Debug.Log("Extra coins added");
            coinSystem.AddCoins(4);
            maxActiveBuffs += 1;
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }

    public void resetBuffs()
    {
        maxActiveBuffs = 0;
        energySystem.energyCounter = 3;
        simpleHealth.health = 100;
        coinSystem.AddCoins(2);
    }

}

