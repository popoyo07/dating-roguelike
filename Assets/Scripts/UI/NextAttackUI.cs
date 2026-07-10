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
            //Depending on which attack the current enemy has chosen for their turn,
            //Set the next attack sprite to the corresponding sprite from the list
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
        //Change the alpha of the UI element to 255 (Visible)
        if (nextAttackImage != null && thoughtBubble != null) 
        {


            nextAttackImage.color = new Color32(255, 255, 255, 255);
            thoughtBubble.color = new Color32(255, 255, 255, 255);
            isVisible = true;
        }
            
    }

    public void HideNextAttack()
    {
        //Change the alpha of the UI element to 0 (Not visible)
        if (nextAttackImage != null && thoughtBubble != null) 
        {
            nextAttackImage.color = new Color32(255, 255, 255, 0);
            thoughtBubble.color = new Color32(255, 255, 255, 0);
            isVisible = false;
        }
    }
}
