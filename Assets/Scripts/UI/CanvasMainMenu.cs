using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasMainMenu : MonoBehaviour
{
    // UI Panels
    public GameObject mainMenu;       // Main menu UI
    public GameObject settingsMenu;   // Settings menu UI
    public GameObject shopMenu;       // Shop menu UI
    public GameObject loadingScreen;  // Loading screen UI
    public GameObject creditsScreen;  // Credits screen UI

    /*
    // Optional: Show loading screen at start
    private void Start()
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);   // Show loading screen immediately
            StartCoroutine(HideLoading());   // Start countdown to hide it
        }
    }
    */

    // Load the Dungeon scene
    public void EnterDungeon()
    {
        SceneManager.LoadScene("Dungeon");
    }

    // Quit the application
    public void ExitApplication()
    {
        Application.Quit();
    }

    // Show the main menu and hide other menus
    public void Main()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        shopMenu.SetActive(false);
    }

    // Open the shop menu and hide the main menu
    public void Shop()
    {
        shopMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    // Open the settings menu and hide the main menu
    public void Settings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    // Open the credits screen and hide settings menu
    public void Credits()
    {
        settingsMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }

    // Close credits screen and reopen settings menu
    public void CloseCredits()
    {
        settingsMenu.SetActive(true);
        creditsScreen.SetActive(false);
    }

    /*
    // Optional coroutine to hide loading screen after delay
    private IEnumerator HideLoading()
    {
        yield return new WaitForSeconds(2f);
        loadingScreen.SetActive(false);     // Hide after 2 seconds
    }
    */
}
