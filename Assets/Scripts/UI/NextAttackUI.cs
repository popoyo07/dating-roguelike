using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class NextAttackUI : MonoBehaviour
{
    public Enemy enemy;
    [SerializeField] private TextMeshProUGUI nextAttackText;
    public int attack;

    private void Awake()
    {
        nextAttackText = GameObject.Find("NextAttack").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateNextAttackUI()
    {
        switch (attack)
        {
            case 0:
                nextAttackText.text = "Regular Attack";
                break;
            case 1:
                nextAttackText.text = "Double Attack";
                break;
            case 2:
                nextAttackText.text = "Attack Stance";
                break;
            case 3:
                nextAttackText.text = "Guard";
                break;
            default:
                Debug.Log("Nothing happened");
                break;
        }
    }


}
