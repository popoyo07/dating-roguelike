using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class HoldTest : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public float holdDuration = 1f;
    
    Vector3 startingLocation;
    float holdtimer;
    bool isHolding = false;

    public UnityEvent HoldingButton;
    private void OnEnable()
    {
        
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
                    HoldingButton.Invoke();
                    Debug.Log("runing the code");
                }
               // ResetHolding();
            }
        }
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
        ResetHolding();
        Debug.Log("release holding");
    }
}
