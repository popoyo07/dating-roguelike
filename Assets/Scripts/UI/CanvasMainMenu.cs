using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasMainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject shopMenu;
    public GameObject loadingScreen;
    public GameObject creditsScreen;

    /*private void Start()
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);   // show it right away
            StartCoroutine(HideLoading());   // start hiding countdown
        }
    }*/

    public void EnterDungeon()
    {
        SceneManager.LoadScene("Dungeon");
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

    public void Credits()
    {
        settingsMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void CloseCredits()
    {
        settingsMenu.SetActive(true);
        creditsScreen.SetActive(false);
    }

    /*private IEnumerator HideLoading()
    {
        yield return new WaitForSeconds(2f);
        loadingScreen.SetActive(false);     // hide after 2 seconds
    }*/
}