using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    // UI panels
    public GameObject winMenu;        // Win screen UI
    public GameObject loseMenu;       // Lose screen UI
    public GameObject cardsMan;       // UI container for cards
    public GameObject man;            // UI container for other elements
    public GameObject loadingScreen;  // Loading screen UI
    public GameObject settings;       // Settings UI panel
    public GameObject rewardsPopup;   // Rewards popup UI
    public GameObject roomPopup;      // Room selection popup UI
    public GameObject showDeckPopup;  // Deck display popup UI

    // Game system references
    BattleSystem battleSystem;       // Reference to battle system
    Rewards rewards;                 // Reference to rewards manager
    ChooseRoom chooseRoom;           // Reference to room manager

    [Header("Dialogue Data")]
    [SerializeField] private DialogueProgression[] dialogueProgression;

    public bool showDeckToggleBool; // Tracks whether the deck popup should be displayed

    public bool isRetryTrue = false;

    private void Start()
    {
        // Get references to game systems using tags
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        rewards = GameObject.FindWithTag("RewardsM").GetComponent<Rewards>();
        chooseRoom = GameObject.FindWithTag("RoomManager").GetComponent<ChooseRoom>();

        // Show loading screen briefly at start
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
            StartCoroutine(HideLoading());
        }
    }

    private void Update()
    {
        // Show lose menu if player lost the battle
        if (battleSystem.state == BattleState.LOST)
        {
            loseMenu.SetActive(true);
            cardsMan.SetActive(false); // Hide card UI
            man.SetActive(false);      // Hide other UI
        }

        // Handle rewards popup visibility
        if (rewards.openRewardsPop && !rewardsPopup.activeSelf && !roomPopup.activeSelf)
        {
            rewardsPopup.SetActive(rewards.openRewardsPop);
            StartCoroutine(ShowRewardsNextFrame());

        }

        if (!rewards.openRewardsPop && rewardsPopup.activeSelf)
        {
            rewardsPopup.SetActive(false);
            rewards.rewardsForCurrent = false;
            roomPopup.SetActive(true); // Show room popup after rewards are closed
        }

        // Handle room popup visibility
        if (!chooseRoom.openRoomPop && roomPopup.activeSelf)
        {
            roomPopup.SetActive(false);
        }

        // Handle deck popup visibility
        if (showDeckToggleBool)
        {
            showDeckPopup.SetActive(true);
        }
        if (!showDeckToggleBool)
        {
            showDeckPopup.SetActive(false);
        }
    }

    // Exit the application
    public void ExitApplication()
    {
        Application.Quit();
    }

    // Return to main menu scene
    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Reload the dungeon scene
    public void Retry()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("Dungeon");
        loseMenu.SetActive(false);
        isRetryTrue = true;
        StartCoroutine(HideLoading());
    }

    // Coroutine to hide loading screen after a delay
    private IEnumerator HideLoading()
    {
        yield return new WaitForSeconds(1.5f); // Wait for 1.5 seconds
        loadingScreen.SetActive(false);        // Hide loading screen
        isRetryTrue = false;
    }

    // Open the settings panel
    public void OpenSettings()
    {
        settings.SetActive(true);
    }

    // Close the settings panel
    public void CloseSettings()
    {
        settings.SetActive(false);
        Debug.Log("Close Setting");
    }

    // Toggle deck popup visibility
    public void ShowDeck()
    {
        showDeckToggleBool = !showDeckToggleBool;
    }

    // Explicitly close the deck popup
    public void CloseDeck()
    {
        showDeckToggleBool = false;
    }

    // Reset dialogue progression index
    public void ResetDialogueIndex()
    {
        dialogueProgression[0].currentDialogueIndex = 0;
        dialogueProgression[0].phase = 0;
    }

    private IEnumerator ShowRewardsNextFrame()
    {
        // Wait one frame to ensure UI elements are fully active
        yield return null;

        if (rewards != null)
        {
            rewards.rewardsForCurrent = false;
            rewards.ShowRewardOptions();
        }
    }
}
