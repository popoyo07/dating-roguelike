using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class NextAttackUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nextAttackText;
    [SerializeField] private Image nextAttackImage;
    [SerializeField] private Sprite[] spriteList;

    private void Awake()
    {
        nextAttackText = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();            //Find("NextAttackText").GetComponent<TextMeshProUGUI>();
        nextAttackImage = this.gameObject.GetComponentInChildren<Image>();
    }

    public void UpdateNextAttackUI(int action)
    {
        Debug.Log("Attempting to Update Next Attack UI");
        switch (action)
        {
            case 0:
                nextAttackImage.sprite = spriteList[0];
                break;
            case 1:
                nextAttackImage.sprite = spriteList[0];
                break;
            case 2:
                nextAttackImage.sprite = spriteList[1];
                break;
            case 3:
                nextAttackImage.sprite = spriteList[1];
                break;
            default:
                Debug.Log("Nothing happened");
                break;
        }
    }

    public void ShowNextAttack()
    {
        nextAttackImage.color = new Color32(255, 255, 255, 255);
    }

    public void HideNextAttack()
    {
        nextAttackImage.color = new Color32(255, 255, 255, 0);
    }
}
