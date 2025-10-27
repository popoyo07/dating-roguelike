using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    private SimpleHealth playerHP; // Reference to the player's health component

    [Header("Bools")]
    public bool openRewardsPop;      // Whether the rewards popup is currently open
    public bool pickedReward;        // Whether a reward has been picked
    public bool rewardsForCurrent;   // Whether rewards have been generated for the current room

    private DeckUIManager deckUI;    // Reference to the Deck UI manager

    [System.Serializable]
    public class Reward
    {
        public string rewardName;    // Name of the reward
        public Sprite rewardSprite;  // Sprite to show for the reward
        public RewardType rewardType; // Type of reward
        //public GameObject enemyPrefab; // Optional for future enemy rewards
    }

    public enum RewardType { Coins, Card, RecoverHP } // Possible reward types

    [Header("Rewards")]
    public List<Reward> roomRewards; // List of possible rewards for this room

    [Header("Buttons")]
    public Button button1; // UI button for first reward
    public Button button2; // UI button for second reward
    public Button button3; // UI button for third reward

    private Reward reward1; // First randomly selected reward
    private Reward reward2; // Second randomly selected reward
    private Reward reward3; // Third randomly selected reward

    //private BattleSystem battleSystem;
    private CoinSystem coinSystem; // Reference to coin management system
    private DeckDraw deck;         // Reference to the deck management system

    void Start()
    {
        // Get references to required components at runtime
        playerHP = GameObject.FindWithTag("Player").GetComponent<SimpleHealth>();
        coinSystem = GameObject.FindWithTag("CoinSystem").GetComponent<CoinSystem>();
        deck = GameObject.Find("Managers").GetComponentInChildren<DeckDraw>();
        deckUI = GameObject.FindWithTag("DUM").GetComponent<DeckUIManager>(); // or assign in inspector
    }

    // Displays three reward options to the player
    public void ShowRewardOptions()
    {
        if (rewardsForCurrent)
        {
            return; // Prevent generating rewards multiple times for the same room
        }

        rewardsForCurrent = true;

        // Pick three random rewards
        int index1 = Random.Range(0, roomRewards.Count);
        int index2 = Random.Range(0, roomRewards.Count);
        int index3 = Random.Range(0, roomRewards.Count);

        // Ensure that indices are unique if there is more than one reward available
        while (index2 == index1 || index2 == index3 || index3 == index1 && roomRewards.Count > 1)
        {
            index2 = Random.Range(0, roomRewards.Count);
            index3 = Random.Range(0, roomRewards.Count);
        }

        // Assign the chosen rewards
        reward1 = roomRewards[index1];
        reward2 = roomRewards[index2];
        reward3 = roomRewards[index3];

        // Assign the reward sprites to the buttons
        button1.image.sprite = reward1.rewardSprite;
        button2.image.sprite = reward2.rewardSprite;
        button3.image.sprite = reward3.rewardSprite;

        // Remove any previous click listeners to prevent stacking
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        // Add new click listeners to apply the selected reward
        button1.onClick.AddListener(() => ApplyReward(reward1));
        button2.onClick.AddListener(() => ApplyReward(reward2));
        button3.onClick.AddListener(() => ApplyReward(reward3));
    }

    // Applies the chosen reward to the player
    void ApplyReward(Reward reward)
    {
        if (!rewardsForCurrent)
        {
            return; // Prevent applying rewards if none are currently active
        }

        pickedReward = true;

        switch (reward.rewardType)
        {
            case RewardType.Coins:
                StartCoroutine(coinSystem.AddCoins(8)); // Add 8 coins to the player's inventory
                Debug.LogWarning("Coins ADDED");
                break;

            case RewardType.Card:
                // Pick a random card from the deck database
                int r = Random.Range(0, deck.cardDatabase.allCards.Count);
                string newCard = deck.cardDatabase.allCards[r];

                deck.runtimeDeck.Add(newCard); // Add the new card to the runtime deck

                if (deckUI != null)
                {
                    deckUI.AddCardUI(newCard); // Update the deck UI to display the new card
                }

                Debug.LogWarning("New Card ADDED called " + deck.cardDatabase.allCards[r]);
                Debug.Log("Deck now has " + deck.runtimeDeck.Count + " and discard has " + deck.discardedCards.Count);
                break;

            case RewardType.RecoverHP:
                playerHP.PercentageRecoverHP(30); // Recover 30% of player's max HP
                Debug.LogWarning("Recover HP by 30% of max HP");
                break;
        }
    }
}
