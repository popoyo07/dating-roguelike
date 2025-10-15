using UnityEngine;

public class BuffManager : MonoBehaviour, IDataPersistence
{
    private SimpleHealth simpleHealth;
    private CoinSystem coinSystem;
    private EnergySystem energySystem;

    int coins;

    private int maxActiveBuffs = 0;
    private int currentActiveBuffs = 0;

    public int healthBuff = 10;
    public int healthBuffCost = 2;

    public int energyBuff = 1;
    public int energyBuffCost = 4;

    public int doubleCoinsBuff;
    public int doubleCoinsBuffCost = 5;

    #region Save and Load

    public void LoadData(GameData data)
    {
        this.coins = data.coins;
    }

    public void SaveData(ref GameData data)
    {
        data.coins = this.coins;
    }

    #endregion

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
        if(maxActiveBuffs < 3)
        {
            if (coins >= healthBuffCost)
            {
                coins -= healthBuffCost;
               // simpleHealth.IncreaseHP(healthBuff);
                Debug.Log("Health added");
                currentActiveBuffs++;
            }
            else
            {
                Debug.Log("Not enough coins");
                Debug.Log("coins: " + coins);
            }

        }

        else
        {
            Debug.Log("Can not have more then 3 buffs active");
        }

        maxActiveBuffs = currentActiveBuffs;
    }

    public void buyEnergyBuff()
    {
        if (maxActiveBuffs < 3)
        {
            if (coins >= energyBuffCost && maxActiveBuffs < 3)
            {
                coins -= energyBuffCost;
               // energySystem.IncreaseEnergy(energyBuff);
                Debug.Log("Energy added");
                currentActiveBuffs++;
            }
            else
            {
                Debug.Log("Not enough coins");
                Debug.Log("coins: " + coins);
            }
        }

        else
        {
            Debug.Log("Can not have more then 3 buffs active");
        }

        maxActiveBuffs = currentActiveBuffs;
    }

    public void buyExtraCoinsBuff()
    {
        if (maxActiveBuffs < 3)
        {
            if (coins >= doubleCoinsBuffCost && maxActiveBuffs < 3)
            {
                coins -= doubleCoinsBuffCost;
                Debug.Log("Extra coins added");
               // coinSystem.AddCoins(4);
                currentActiveBuffs++;
            }
            else
            {
                Debug.Log("Not enough coins");
                Debug.Log("coins: " + coins);
            }

        }

        else
        {
            Debug.Log("Can not have more then 3 buffs active");
        }

        maxActiveBuffs = currentActiveBuffs;
    }

    public void resetBuffs()
    {
        Debug.Log("Buffs Reset");
        currentActiveBuffs = 0;
        energySystem.energyCounter = 3;
        simpleHealth.health = 100;
        coinSystem.AddCoins(2);
    }

}

