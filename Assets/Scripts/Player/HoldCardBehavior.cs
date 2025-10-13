using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class HoldCardBehavior : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameObject cardExplanation;
    public TextMeshProUGUI txt;
    public float holdDuration = 1f;
    bool done = false;
    Vector3 startingLocation;
    float holdtimer;
    bool isHolding = false;

    public UnityEvent HoldingButton;
    private void OnEnable()
    {
        CloseExplanation();
        if (cardExplanation == null)
        {
            cardExplanation = GameObject.Find("Card Explnation");
            if (txt == null)
            { txt = cardExplanation.GetComponentInChildren<TextMeshProUGUI>(); }
                
        }
        startingLocation = gameObject.transform.position;
    }
    private void Update()
    {
        if (isHolding)
        {
            holdtimer += Time.deltaTime;
            if (holdtimer >= holdDuration)
            {
                if (HoldingButton != null)
                {
                    if (!done)
                    {
                        Debug.Log("On card hold from assing card is working ");

                        done = true;    
                        HoldingButton.Invoke();
                        Debug.Log("runing the code");
                    }
                   
                } 
                
               // ResetHolding();
            }
        }
    }

    public void ShowExplanation(string s)
    {
        cardExplanation.SetActive(true);
        txt.text = s;
    }

    public void CloseExplanation()
    {
        cardExplanation.SetActive(false);
        done = false;
        ResetHolding();

    }
    void ResetHolding()
    {
        isHolding = false;
        holdtimer = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true ;
        Debug.Log("Holding");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CloseExplanation();
        Debug.Log("release holding " + isHolding);
    }
}
