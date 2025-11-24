using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    // UI panels
    public GameObject winMenu;        // Win screen UI
    public GameObject winMenuSiren;
    public GameObject winMenuVampire;
    public GameObject winMenuKinnara;
    public GameObject loseMenu;       // Lose screen UI
    public GameObject cardsMan;       // UI container for cards
    public GameObject man;            // UI container for other elements
    public GameObject loadingScreen;  // Loading screen UI
    public GameObject settings;       // Settings UI panel
    public GameObject rewardsPopUp;   // Rewards popup UI
    public GameObject roomPopUp;      // Room selection popup UI
    public GameObject showDeckPopup;  // Deck display popup UI
    public DialogueUI dialogueUI;

    // Game system references
    BattleSystem battleSystem;       // Reference to battle system
    Rewards rewards;                 // Reference to rewards manager
    ChooseRoom chooseRoom;           // Reference to room manager

    private CoinSystem coinSystem;
    public AudioSource rewardsAudio;
    public AudioSource buttonClick;

    [Header("Dialogue Data")]
    [SerializeField] private DialogueProgression[] dialogueProgression;

    public bool showDeckToggleBool; // Tracks whether the deck popup should be displayed
    bool endingDialogueTriggered = false;

    public bool isRetryTrue = false;

    private void Start()
    {
        // Get references to game systems using tags
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        rewards = GameObject.FindWithTag("RewardsM").GetComponent<Rewards>();
        chooseRoom = GameObject.FindWithTag("RoomManager").GetComponent<ChooseRoom>();
        dialogueUI = GetComponent<DialogueUI>();
        coinSystem = GameObject.FindWithTag("CoinSystem").GetComponent<CoinSystem>();

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

        if (battleSystem.state == BattleState.REWARD)
        {
            //Debug.Log("Current phase:" + dialogueProgression[0].phase);


            // Only show normal rewards popup if NOT final reward phase
            if (dialogueProgression[0].phase != 3)
            {
                //Debug.Log("Current phase:" + dialogueProgression[0].phase);

                Debug.Log($"Reward:{rewards.pickedReward} | RewardsPopUp.activeSelf:{rewardsPopUp.activeSelf}");

                if (!rewardsPopUp.activeSelf && !roomPopUp.activeSelf)
                {
                    rewardsAudio.Play();
                    rewardsPopUp.SetActive(true);
                    StartCoroutine(ShowRewardsNextFrame());

                }

                if (rewards.pickedReward && rewardsPopUp.activeSelf)
                {
                    rewardsPopUp.SetActive(false);
                    rewards.rewardsForCurrent = false;
                    roomPopUp.SetActive(true);
                    chooseRoom.ShowRoomOptions();
                }

            }
            else
            {
                // Phase 3: final reward logic
                if (!battleSystem.finalReward)
                {
                    battleSystem.finalReward = true;
                    coinSystem.coins += 30;

                    DialogueActivator dialogueActivator = GameObject.FindWithTag("Boss").GetComponent<DialogueActivator>();

                    if (dialogueActivator != null)
                        dialogueProgression[0] = dialogueActivator.progression;


                    if (!endingDialogueTriggered)
                    {
                        if (dialogueProgression[0].phase == 3)
                        {
                            endingDialogueTriggered = true;
                            dialogueActivator.EndingDialogue();
                        }
                        else
                        {
                            Debug.Log("DialogueEnd not activate");
                        }
                    }
                }
            }

            // Handle room popup visibility
            if (chooseRoom.chosenRoom && roomPopUp.activeSelf)
            {
                roomPopUp.SetActive(false);

                // Reset flags so reward UI doesn't trigger again
                rewards.pickedReward = false;
                rewards.rewardsForCurrent = false;
                chooseRoom.chosenRoom = false;

                // IMPORTANT: leave REWARD state
                battleSystem.state = BattleState.WON; // or whatever next state you use
            }

        }
        else if (rewardsPopUp.activeSelf || roomPopUp.activeSelf)
        {
            roomPopUp.SetActive(false);

            rewardsPopUp.SetActive(false);
        }

        // Handle deck popup visibility
        if (showDeckToggleBool && !showDeckPopup.activeSelf) // only runs when toogle is on and is not active yet 
        {
            showDeckPopup.SetActive(true);
        }
        if (!showDeckToggleBool && showDeckPopup.activeSelf) // only run if toogle is off and it is still enabled 
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
        buttonClick.Play();
        StartCoroutine(HoldMainForSFX());
    }

    // Reload the dungeon scene
    public void Retry()
    {
        buttonClick.Play();
        StartCoroutine(HoldRetryForSFX());
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
        buttonClick.Play();
    }

    // Close the settings panel
    public void CloseSettings()
    {
        settings.SetActive(false);
        Debug.Log("Close Setting");
        buttonClick.Play();
    }

    // Toggle deck popup visibility
    public void ShowDeck()
    {
        showDeckToggleBool = !showDeckToggleBool;
        buttonClick.Play();
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
            //rewards.rewardsForCurrent = false;
            rewards.ShowRewardOptions();
        }
    }

    private IEnumerator HoldRetryForSFX()
    {
        // Wait one frame to ensure UI elements are fully active
        yield return new WaitForSeconds(.4f);

        loadingScreen.SetActive(true);
        SceneManager.LoadScene("Dungeon");
        loseMenu.SetActive(false);
        isRetryTrue = true;
    }

    private IEnumerator HoldMainForSFX()
    {
        // Wait one frame to ensure UI elements are fully active
        yield return new WaitForSeconds(.4f);

        SceneManager.LoadScene("MainMenu");
    }
}
