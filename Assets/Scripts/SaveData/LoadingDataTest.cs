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
    private SimpleHealth player, enemy;

    void Awake()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<SimpleHealth>();
        player = GameObject.FindWithTag("Player").GetComponent<SimpleHealth>();
    }

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

    public void TestTakeDamage()
    {
        player.ReceiveDMG(1);
    }

    public void TestAttack()
    {
        enemy.ReceiveDMG(1);
    }
}
