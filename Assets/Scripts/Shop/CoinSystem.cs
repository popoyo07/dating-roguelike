using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public int coins;
    bool itemPurchased;

    // Coins to be used for unlocking new cards and classes in the shop

    // Item buttons check cost, if coins > cost then subtract coins

    //



    void AddCoin()
    {
        coins += 1;
    }

    void SubCoin()
    {
        coins -= 1;
    }

    public void BuyItem(int cost)
    {
        coins -= cost;
    }

}
