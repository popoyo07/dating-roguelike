using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AssignCardAnimation : MonoBehaviour
{
    private Image cardImage;
    Animator animator;
    RectTransform rectTransform;
    public Vector3 location;
    public Vector3 setRotation;
   
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
        cardImage = GetComponent<Image>();
        StartCoroutine(StartingAnimation(0));

    }
    void LateUpdate()
    {
        SetUpLocation();
    }
    void SetUpLocation() // set up base location 
    {
        if (animator != null && animator.GetBool("spawned") == false && animator.GetBool("used") == false)
        {
            rectTransform.anchoredPosition = location;
            rectTransform.localEulerAngles = setRotation;
            //Debug.Log("For the object " + gameObject.name + " the locaton is " + rectTransform.anchoredPosition + "and the rotation is " + rectTransform.localEulerAngles);
        }

    }
    public IEnumerator StartingAnimation(int i) // changes values depending on animation choice 
    {
        if (animator != null)
        {
            if (i == 0)
            {
                animator.SetBool("spawned", true);
                yield return new WaitForSeconds(1f);

                animator.SetBool("spawned", false);

            }
            else
            {
                animator.SetBool("used", true);
                yield return new WaitForSeconds(1f);
                animator.SetBool("used", false);
                cardImage.enabled = false;

            }
        }

    }
}
