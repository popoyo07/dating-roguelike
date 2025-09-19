using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    [Header("Bools")]
    public bool openRewardsPop;
    public bool firstPick;
    private bool rewardSelectOnce;
    public bool pickedReward;

    [System.Serializable]
    public class Reward
    {
        public string rewardName;
        public Sprite rewardSprite;
        public RewardType rewardType;
    }

    public enum RewardType { Coins, Card, Lovey }

    [Header("Rewards")]
    public List<Reward> roomRewards;

    [Header("Buttons")]
    public Button button1;
    public Button button2;

    private Reward reward1;
    private Reward reward2;

    private BattleSystem battleSystem;

    void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        openRewardsPop = true;
        firstPick = true;
        ShowRewardOptions();
    }

    private void Update()
    {
        if (battleSystem.state == BattleState.WON && !pickedReward && !rewardSelectOnce)
        {
            ShowRewardOptions();
            rewardSelectOnce = true;
        }
    }

    void ShowRewardOptions()
    {
        // Pick random rewards
        int index1 = Random.Range(0, roomRewards.Count);
        int index2 = Random.Range(0, roomRewards.Count);

        while (index2 == index1 && roomRewards.Count > 1)
        {
            index2 = Random.Range(0, roomRewards.Count);
        }
            
        reward1 = roomRewards[index1];
        reward2 = roomRewards[index2];

        // Assign sprites
        button1.image.sprite = reward1.rewardSprite;
        button2.image.sprite = reward2.rewardSprite;

        // Assign behavior
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => ApplyReward(reward1));
        button2.onClick.AddListener(() => ApplyReward(reward2));
    }

    void ApplyReward(Reward reward)
    {
        pickedReward = true;
        openRewardsPop = false;
        firstPick = false;
        rewardSelectOnce = false;

        switch (reward.rewardType)
        {
            case RewardType.Coins:
                Debug.LogWarning("Coins ADDED");
                break;
            case RewardType.Card:
                Debug.LogWarning("New Card ADDED");
                break;
            case RewardType.Lovey:
                Debug.LogWarning("Lovey Card ADDED");
                break;
        }
    }
}