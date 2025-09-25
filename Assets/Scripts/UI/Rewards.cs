using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    private SimpleHealth playerHP;
    [Header("Bools")]
    public bool openRewardsPop;
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

    public enum RewardType { Coins, Card, RecoverHP }

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
    private DeckDraw deck;

    void Start()
    {
        playerHP = GameObject.FindWithTag("Player").GetComponent<SimpleHealth>();
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        enemySpawner = GameObject.FindWithTag("EnemyS").GetComponent<EnemySpawner>();
        deck = GameObject.Find("Managers").GetComponentInChildren<DeckDraw>();
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

        switch (reward.rewardType)
        {
            case RewardType.Coins:
                Debug.LogWarning("Coins ADDED");
                break;
            case RewardType.Card:
                int r = Random.Range(0, deck.cardDatabase.allCards.Count);
                deck.runtimeDeck.Add(deck.cardDatabase.allCards[r]); 

                Debug.LogWarning("New Card ADDED called " + deck.cardDatabase.allCards[r]);
                Debug.Log(" deck now has " + deck.runtimeDeck.Count + " and discard has " + deck.discardedCards.Count);
                break;
            case RewardType.RecoverHP:
                playerHP.PercentageRecoverHP(30);
                Debug.LogWarning("Recover HP by 30% of max HP");
                break;
        }
    }
}