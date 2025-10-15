using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]

public class GameData
{
    public CharacterClass playerClass;
    public int coins;
    public int healthBuff;
    public int coinBuff;
    public int energyBuff;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    // the values defined in this constructor will be the default values
    // the game starts with when there is no data to load

    // define values and their type above and set their default value in GameData()
    public GameData()
    {
        this.playerClass = CharacterClass.KNIGHT;
        this.coins = 0;
        this.coinBuff = 0;
        this.energyBuff = 0;
        this.healthBuff = 0;
        this.masterVolume = 0;
        this.musicVolume = 0;
        this.sfxVolume = 0;
    }



}
