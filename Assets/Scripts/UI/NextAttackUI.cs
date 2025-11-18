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
        if(nextAttackImage != null && thoughtBubble != null)
        {
            Debug.Log("Attempting to Update Next Attack UI");
            switch (action)
            {
                //Regular ATK
                case 0:
                    nextAttackImage.sprite = spriteList[0];
                    break;
                
                //Double ATK
                case 1:
                    nextAttackImage.sprite = spriteList[1];
                    break;
                
                //Stance Up
                case 2:
                    nextAttackImage.sprite = spriteList[2];
                    break;
                
                //Gaurd
                case 3:
                    nextAttackImage.sprite = spriteList[3];
                    break;
                
                //Weaken
                case 4:
                    nextAttackImage.sprite = spriteList[4];
                    break;
                
                //Vulnerable
                case 5:
                    nextAttackImage.sprite = spriteList[5];
                    break;
                
                //Default
                default:
                    Debug.Log("Nothing happened");
                    break;
            }
        }
        
    }

    public void ShowNextAttack()
    {
        if (nextAttackImage != null && thoughtBubble != null) 
        {
            nextAttackImage.color = new Color32(255, 255, 255, 255);
            thoughtBubble.color = new Color32(255, 255, 255, 255);
            isVisible = true;
        }
            
    }

    public void HideNextAttack()
    {
        if (nextAttackImage != null && thoughtBubble != null) 
        {
            nextAttackImage.color = new Color32(255, 255, 255, 0);
            thoughtBubble.color = new Color32(255, 255, 255, 0);
            isVisible = false;
        }
    }
}
