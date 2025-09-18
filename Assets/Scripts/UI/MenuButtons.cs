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

    BattleSystem battleSystem;


    private void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);   // show it right away
            StartCoroutine(HideLoading());   // start hiding countdown
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
    }

    public void ExitApplication()
    {
        Application.Quit();
    }


    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
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