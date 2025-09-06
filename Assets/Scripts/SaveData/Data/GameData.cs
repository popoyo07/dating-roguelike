using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]

public class GameData
{
    public int testCounter;

    // the values defined in this constructor will be the default values
    // the game starts with when there is no data to load

    // define values and their type above and set their default value in GameData()
    public GameData()
    {
        this.testCounter = 0;   

    }



}
