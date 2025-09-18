using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    public bool openRewards;
    public bool closeRewards;

    //public int addCoins;

    public List<Sprite> roomRewardList;
    public Button targetButton;

    BattleSystem battleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        NextRoomReward();
    }

    public void NextRoomReward()
    {
        if (roomRewardList.Count == 0)
        {
            Debug.LogWarning("No images in the list to select from!");
            return;
        }

        // Generate a random index within the bounds of the list
        int randomIndex = Random.Range(0, roomRewardList.Count);

        // Get the Image component of the button
        Image buttonImage = targetButton.GetComponent<Image>();

        if (buttonImage != null)
        {
            // Set the sprite of the button's Image component to the randomly chosen image
            buttonImage.sprite = roomRewardList[randomIndex];
        }
        else
        {
            Debug.LogError("Button does not have an Image component!");
        }
    }

    public void CoinReward()
    {
        //add coins
        Debug.LogWarning("Coins ADDED");
    }

    public void CardReward()
    {
        //add new card or cards to deck
        Debug.LogWarning("New Card ADDED");
    }

    public void LoveyReward()
    {
        //guaranteed LovyDovy Card
        Debug.LogWarning("Lovey Card ADDED");
    }

    public void OnRewardSelected()
    {
        openRewards = false;
        closeRewards = true;
    }
}
