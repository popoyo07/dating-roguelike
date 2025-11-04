using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class NextAttackUI : MonoBehaviour
{
    public bool isVisible;
    [SerializeField] private Image thoughtBubble;
    [SerializeField] private Image nextAttackImage;
    [SerializeField] private Sprite[] spriteList;

    private void Awake()
    {
        nextAttackImage = GameObject.Find("NextAttackImage").GetComponent<Image>();
        thoughtBubble = GameObject.Find("ThoughtBubble").GetComponent<Image>();
    }

    public void UpdateNextAttackUI(int action)
    {
        if(nextAttackImage != null)
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
        
    }

    public void ShowNextAttack()
    {
        nextAttackImage.color = new Color32(255, 255, 255, 255);
        thoughtBubble.color = new Color32(255, 255, 255, 255);
        isVisible = true;
    }

    public void HideNextAttack()
    {
        nextAttackImage.color = new Color32(255, 255, 255, 0);
        thoughtBubble.color = new Color32(255, 255, 255, 0);
        isVisible = false;
    }
}
