using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestItem : MonoBehaviour
{
    [SerializeField] private CoinSystem coinSystem;
    [SerializeField] private int cost;
    int coins;
    bool itemPurchased;

    private void Awake()
    {
        coinSystem = GameObject.Find("CoinSystem").GetComponent<CoinSystem>(); 
    }

    private void Start()
    {
        coins = coinSystem.coins;
    }
     
    private void BuyItem()
    {
        coins = coinSystem.coins;

        if (coins > cost)
        {
            StartCoroutine(coinSystem.BuyItem(5));
            coins = coinSystem.coins;
            itemPurchased = true;
        }
        else
        {
            itemPurchased = false;
        }

        if (itemPurchased == true)
        {
            //Add code to add things to the player's inventory
        }
    }
}
