using UnityEngine;

public class BuffManager : MonoBehaviour, IDataPersistence
{
    int coins;

    private int maxActiveBuffs = 0;
    private int currentActiveBuffs = 0;

    public int healthBuffCount;
    public int healthBuffCost = 2;

    public int energyBuffCount;
    public int energyBuffCost = 4;

    public int doubleCoinsBuffCount;
    public int doubleCoinsBuffCost = 5;

    #region Save and Load

    public void LoadData(GameData data)
    {
        Debug.Log("Load data is running");
        this.healthBuffCount = data.healthBuff;
        this.energyBuffCount = data.energyBuff;
        this.doubleCoinsBuffCount = data.coinBuff;
        this.coins = data.coins;
    }

    public void SaveData(ref GameData data)
    {
        data.coins = this.coins;
        data.healthBuff = this.healthBuffCount;
        data.energyBuff = this.energyBuffCount;
        data.coinBuff = this.doubleCoinsBuffCount;
    }

    #endregion

    private void Start()
    {

    }

    private void Update()
    {
       /* if (player dies)
        {
            resetBuffs();
        }*/

    }
    public void buyHealthBuff()
    {
        if(maxActiveBuffs < 3)
        {
            if (coins >= healthBuffCost)
            {
                coins -= healthBuffCost;
                Debug.Log("Health added");
                currentActiveBuffs++;
                healthBuffCount++;
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
                Debug.Log("Energy added");
                currentActiveBuffs++;
                energyBuffCount++;
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
                currentActiveBuffs++;
                doubleCoinsBuffCost = coins;
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
        healthBuffCount = 0;
        energyBuffCount = 0;
        doubleCoinsBuffCount = 0;
        maxActiveBuffs = 0;
        currentActiveBuffs = 0;
    }

}

