using UnityEngine;
using TMPro;
public class BuffManager : MonoBehaviour, IDataPersistence
{
    public int coins;

    private int maxActiveBuffs = 0;
    private int currentActiveBuffs = 0;

    public int healthBuffCount;
    public int healthBuffCost = 2;

    public int energyBuffCount;
    public int energyBuffCost = 4;

    public int doubleCoinsBuffCount;
    public int doubleCoinsBuffCost = 5;

    public bool isCoinBuffActive;

    public TextMeshProUGUI activeHealthBuffCount;
    public TextMeshProUGUI activeEnergyBuffCount;
    public TextMeshProUGUI activeCoinBuffCount;

    public CoinUISHOP coinUI;

    #region Save and Load

    public void LoadData(GameData data)
    {
        Debug.Log("Load data is running");
        this.healthBuffCount = data.healthBuff;
        this.energyBuffCount = data.energyBuff;
        this.doubleCoinsBuffCount = data.coinBuff;
        this.coins = data.coins;
        this.isCoinBuffActive = data.isCoinBuffActive;
    }

    public void SaveData(ref GameData data)
    {
        data.coins = this.coins;
        data.healthBuff = this.healthBuffCount;
        data.energyBuff = this.energyBuffCount;
        data.coinBuff = this.doubleCoinsBuffCount;
        data.isCoinBuffActive = this.isCoinBuffActive;
    }

    #endregion

    private void Start()
    {
         resetBuffs();
    }

    private void Update()
    {

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
                activeHealthBuffCount.SetText("Active Buffs: " + healthBuffCount);
                coinUI.UpdateCoins();
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
                activeEnergyBuffCount.SetText("Active Buffs: " + energyBuffCount);
                coinUI.UpdateCoins();
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
                doubleCoinsBuffCount++;
                activeCoinBuffCount.SetText("Active Buffs: " + doubleCoinsBuffCount);
                coinUI.UpdateCoins();
                isCoinBuffActive = true;
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

        activeHealthBuffCount.SetText("Active Buffs: " + healthBuffCount);
        activeEnergyBuffCount.SetText("Active Buffs: " + energyBuffCount);
        activeCoinBuffCount.SetText("Active Buffs: " + doubleCoinsBuffCount);

        isCoinBuffActive = false;
    }

}

