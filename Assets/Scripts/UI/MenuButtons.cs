using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject cardsMan;
    public GameObject man;
    public GameObject loadingScreen;
    public GameObject settings;
    public GameObject rewardsPopup;
    public GameObject roomPopup;

    BattleSystem battleSystem;
    Rewards rewards;
    ChooseRoom chooseRoom;

    private void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        rewards = GameObject.FindWithTag("RewardsM").GetComponent<Rewards>();
        chooseRoom = GameObject.FindWithTag("RoomManager").GetComponent<ChooseRoom>();

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
            StartCoroutine(HideLoading());
        }
    }

    private void Update()
    {
        if (battleSystem.state == BattleState.LOST)
        {
            loseMenu.SetActive(true);
            cardsMan.SetActive(false);
            man.SetActive(false);
        }

        if (rewards.openRewardsPop && !rewardsPopup.activeSelf)
        {
            rewardsPopup.SetActive(true);
        }

        if (!rewards.openRewardsPop && rewardsPopup.activeSelf)
        {
            rewardsPopup.SetActive(false);
        }
        
        if (chooseRoom.openRoomPop && !roomPopup.activeSelf)
        {
            roomPopup.SetActive(true);
        }

        if (!chooseRoom.openRoomPop && roomPopup.activeSelf)
        {
            roomPopup.SetActive(false);
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Dungeon");
    }

    private IEnumerator HideLoading()
    {
        yield return new WaitForSeconds(1.5f);
        loadingScreen.SetActive(false);     // hide after 2 seconds
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
    }
}