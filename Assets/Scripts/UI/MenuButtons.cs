using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class MenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject shopMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject cardsMan;
    public GameObject man;
    public GameObject loadScreen;

    BattleSystem battleSystem;


    private void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }

    private void Update()
    {
        if (battleSystem.state == BattleState.LOST)
        {   
            loadScreen.SetActive(true);
            loseMenu.SetActive(true);
            cardsMan.SetActive(false);
            man.SetActive(false);
        }
    }

    public void EnterDungeon()
    {
        SceneManager.LoadScene("Dungeon");
        loadScreen.SetActive(true);
        StartCoroutine(Delay());
        mainMenu.SetActive(false);
        cardsMan.SetActive(true);
        man.SetActive(true);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void Main()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        shopMenu.SetActive(false);
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
        cardsMan.SetActive(false);
        man.SetActive(false);
    }

    public void BackMain()
    {
        SceneManager.LoadScene("CorinneTest");
        mainMenu.SetActive(true);
        loseMenu.SetActive(false);
        winMenu.SetActive(false);   
    }

    public void Shop()
    {
        shopMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        loadScreen.SetActive(false);
    }
}
