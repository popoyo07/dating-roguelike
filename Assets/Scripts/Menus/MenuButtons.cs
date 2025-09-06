using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject shopMenu;

    public void EnterDungeon()
    {
        //SceneManager.LoadScene("Rooms");
        mainMenu.SetActive(false);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void Main()
    {
        //SceneManager.LoadScene("CorinneTest");
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
}
