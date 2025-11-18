using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class PickDeckButton : MonoBehaviour 
{
    Button b;
    [SerializeField] MusicManager c;
    [SerializeField] BuffManager buffM;
    DataPersistenceManager DATA;
    [SerializeField] GameObject ClassSwitch_TextBox;
    [SerializeField] TextMeshProUGUI ClassSwitch_Text;

    public int ClassPrice;

    private void Awake()
    {
        buffM = GameObject.Find("BuffManager").GetComponent<BuffManager>();
        c = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    public void ChangeToKnight()
    {
        if (buffM.coins > ClassPrice)
        {
            buffM.coins = buffM.coins - ClassPrice;
            c.c = CharacterClass.KNIGHT;
            Debug.Log("choice " + c.c);
            //Place UI Element that tells the player that they have switched classes
            StartCoroutine(SwitchClassUI("Knight", true));
        } else
        {
            StartCoroutine(SwitchClassUI("Knight", false));
            Debug.Log("Not enough coins to change to Knight Class...");
            //Place UI Element that tells the player they do not have enough coins
        }
    }

    public void ChangeToRogue()
    {
        if (buffM.coins > ClassPrice)
        {
            buffM.coins = buffM.coins - ClassPrice;
            c.c = CharacterClass.CHEMIST;
            Debug.Log("choice " + c.c);
            //Place UI Element that tells the player that they have switched classes
            StartCoroutine(SwitchClassUI("Chemist", true));
        }
        else
        {
            StartCoroutine(SwitchClassUI("Chemist", false));
            Debug.Log("Not enough coins to change to Chemist Class...");
            //Place UI Element that tells the player they do not have enough coins
        }
    }

    public void ChangeToWizzard()
    {
        if (buffM.coins > ClassPrice)
        {
            buffM.coins = buffM.coins - ClassPrice;
            c.c = CharacterClass.WIZZARD;
            Debug.Log("choice " + c.c);
            //Place UI Element that tells the player that they have switched classes
            StartCoroutine(SwitchClassUI("Wizard", true));
        }
        else
        {
            StartCoroutine(SwitchClassUI("Wizard", false));
            Debug.Log("Not enough coins to change to Wizard Class...");
            //Place UI Element that tells the player they do not have enough coins
        }
    }

    public IEnumerator SwitchClassUI(string ChClass, bool rich)
    {
        ClassSwitch_TextBox.SetActive(true);
        if (rich == true)
        {
            ClassSwitch_Text.text = "Switched class to the " + ChClass + " Class";
        }
        else
        {
            ClassSwitch_Text.text = "Not enough coins to switch class!";
        }
        yield return new WaitForSeconds(5f);
        ClassSwitch_TextBox.SetActive(false);
        ClassSwitch_Text.text = "";
        yield break;
    }
}
