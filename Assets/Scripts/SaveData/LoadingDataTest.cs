using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class LoadingDataTest : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI testText;
    private int testCounter;
    public void testCount(InputAction.CallbackContext context)
    {
        testCounter++;
        testText.SetText("testCounter =" + testCounter);
    }

    //Take the game data and load the needed variables into the script
    public void LoadData(GameData data)
    {
        this.testCounter = data.testCounter;

    }

    //take the current value of the variables and save their values to the game data
    public void SaveData(ref GameData data)
    {
        data.testCounter = this.testCounter;
    }

}
