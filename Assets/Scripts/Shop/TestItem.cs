using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestItem : MonoBehaviour
{
    [SerializeField] private CoinSystem coinSystem;
    [SerializeField] private int cost;
    int coins;
    bool itemPurchased;

    private void Start()
    {
        coins = coinSystem.coins;
    }
     
    private void BuyItem()
    {
        coins = coinSystem.coins;

        if (coins > cost)
        {
            coinSystem.BuyItem(cost);
            coins = coinSystem.coins;
            itemPurchased = true;
        }
        else
        {
            itemPurchased = false;
        }

        if (itemPurchased == true)
        {
            //Add code to add card to the player's deck
        }
    }
}
