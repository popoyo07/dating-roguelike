using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUISHOP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private int coins;
    [SerializeField] private BuffManager buffManager;

    private void Start()
    {
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        buffManager.coinUI = this;
        coinText = this.gameObject.GetComponent<TextMeshProUGUI>();
        UpdateCoins();
    }

    public void UpdateCoins()
    {
        this.coins = buffManager.coins;
        coinText.text = "Coins: " + coins;
    }


}
