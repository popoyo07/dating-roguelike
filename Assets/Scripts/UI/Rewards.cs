using System.Collections;
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
    public bool rewardsForCurrent; 

    [System.Serializable]
    public class Reward
    {
        public string rewardName;
        public Sprite rewardSprite;
        public RewardType rewardType;

        public GameObject enemyPrefab;
    }

    public enum RewardType { Coins, Card, Lovey, ClassCard }

    [Header("Rewards")]
    public List<Reward> roomRewards;

    [Header("Buttons")]
    public Button button1;
    public Button button2;
    public Button button3;

    private Reward reward1;
    private Reward reward2;
    private Reward reward3;

    private BattleSystem battleSystem;
    private EnemySpawner enemySpawner;

    void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        enemySpawner = GameObject.FindWithTag("EnemyS").GetComponent<EnemySpawner>();
        //openRewardsPop = true;
        firstPick = true;
        //ShowRewardOptions();
    }

    private void Update()
    {
        if (battleSystem.state == BattleState.WON && !pickedReward && !rewardSelectOnce)
        {
            ShowRewardOptions();
            rewardSelectOnce = true;
        }
    }

    public void ShowRewardOptions()
    {
        if (rewardsForCurrent)
        {
            return;
        }
        
        rewardsForCurrent = true;

        // Pick random rewards
        int index1 = Random.Range(0, roomRewards.Count);
        int index2 = Random.Range(0, roomRewards.Count);
        int index3 = Random.Range(0, roomRewards.Count);

        while (index2 == index1 || index2 == index3 || index3 == index1 && roomRewards.Count > 1)
        {
            index2 = Random.Range(0, roomRewards.Count);
            index3 = Random.Range(0, roomRewards.Count);
        }
            
        reward1 = roomRewards[index1];
        reward2 = roomRewards[index2];
        reward3 = roomRewards[index3];

        // Assign sprites
        button1.image.sprite = reward1.rewardSprite;
        button2.image.sprite = reward2.rewardSprite;
        button3.image.sprite = reward3.rewardSprite;

        // Assign behavior
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => ApplyReward(reward1));
        button2.onClick.AddListener(() => ApplyReward(reward2));
        button3.onClick.AddListener(() => ApplyReward(reward3));
    }

    void ApplyReward(Reward reward)
    {
        if (!rewardsForCurrent)
        {
            return;
        }

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
            case RewardType.ClassCard:
                Debug.LogWarning("Lovey Card ADDED");
                break;
        }

        if(reward.enemyPrefab != null && enemySpawner != null)
        {
            enemySpawner.SetNextEnemy(reward.enemyPrefab);
        }
    }
}