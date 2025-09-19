using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    public bool pickedReward;

    public List<Sprite> roomRewards;

    public Button button1;
    public Button button2;

    private Sprite reward1;
    private Sprite reward2;

    BattleSystem battleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        pickedReward = false;
        ShowRewardOptions();
    }

    private void Update()
    {
        if (battleSystem.state == BattleState.WON && pickedReward == false)
        {
            ShowRewardOptions();
        }

        //PickReward();
    }

    void ShowRewardOptions()
    {
        // Pick two random different rewards
        int index1 = Random.Range(0, roomRewards.Count);
        int index2 = Random.Range(0, roomRewards.Count);

        while (index2 == index1 && roomRewards.Count > 1)
        {
            index2 = Random.Range(0, roomRewards.Count);
        }

        reward1 = roomRewards[index1];
        reward2 = roomRewards[index2];

        // Assign to button images
        button1.image.sprite = reward1;
        button2.image.sprite = reward2;
    }

    public void PickReward()
    {
        pickedReward = true;
        StartCoroutine(ClosePopupAfterDelay());
    }

    IEnumerator ClosePopupAfterDelay()
    {
        yield return new WaitForSeconds(0.01f);
        pickedReward = false;
        //FindObjectOfType<MenuButtons>().CloseRewardsPopup();
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
}
