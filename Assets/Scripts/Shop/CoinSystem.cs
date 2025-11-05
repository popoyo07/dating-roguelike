using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CoinSystem : MonoBehaviour, IDataPersistence
{
    public int coins;
    public int coinBuff;

    public bool isCoinBuffActive;

    [SerializeField] BattleSystem battleSystem;
    [SerializeField] private TextMeshProUGUI coinUI;
    [SerializeField] private TextMeshProUGUI coinTotal;
    [SerializeField] private bool banana;

    // Coins to be used for unlocking new cards and classes in the shop

    // Item buttons check cost, if coins > cost then subtract coins

    #region Save and Load

    public void LoadData(GameData data)
    {
        Debug.Log("Load data CoinSystem is running");
        this.coins = data.coins;
        this.coinBuff = data.coinBuff;
        this.isCoinBuffActive = data.isCoinBuffActive;
    }

    public void SaveData(ref GameData data)
    {
        data.coins = this.coins;
        data.coinBuff = 0;
        data.isCoinBuffActive = false;
    }

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this);
        coinTotal = GameObject.Find("TotalCoins").GetComponent<TextMeshProUGUI>();
        coinUI = GameObject.Find("coinUI").GetComponent<TextMeshProUGUI>();
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }

    private void Start()
    {
        StartCoroutine(LoadCoins());
    }

    private void Update()
    {
        if (battleSystem.state == BattleState.WON && banana == false)
        {
            //Debug.LogWarning("Attempting to add coins...");
            StartCoroutine(AddCoins(2));
            banana = true;
        }
        else if (battleSystem.state != BattleState.WON)
        {
            //Debug.LogWarning("Banana bool reverted");
            banana = false;
        }
    }

    public IEnumerator LoadCoins()
    {
        yield return new WaitForSeconds(.1f);
        coinTotal.SetText("Coins: " + coins);
    }

    public IEnumerator AddCoins(int addcoins)
    {
        coins += addcoins + coinBuff;

        if (isCoinBuffActive == true)
        {
            coinUI.SetText("+" + addcoins + " Coins<br>" + "+" + coinBuff + " Coinbuff");
            coinTotal.SetText("Coins: " + coins);
        }
        else
        {
            coinUI.SetText("+" + addcoins + " Coins");
            coinTotal.SetText("Coins: " + coins);
        }

        yield return new WaitForSeconds(2f);
        coinUI.SetText("");
    }

    public IEnumerator BuyItem(int cost)
    {
        if (coins > cost)
        {
            coins -= cost;
            coinUI.SetText("-" + coins + " Coin");
            coinTotal.SetText("Coins: " + coins);
            yield return new WaitForSeconds(2f);
        }
    }
}
