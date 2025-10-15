using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class NextAttackUI : MonoBehaviour
{
    public Enemy enemy;
    [SerializeField] private TextMeshProUGUI nextAttackText;
    bool isVisible;

    private void Awake()
    {
        nextAttackText = GameObject.Find("NextAttack").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateNextAttackUI(int action)
    {
        Debug.Log("Attempting to Update Next Attack UI");
        switch (action)
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

    public void ShowNextAttack()
    {
        nextAttackText.color = new Color32(255, 255, 255, 255);
    }

    public void HideNextAttack()
    {
        nextAttackText.color = new Color32(255, 255, 255, 0);
    }
}
