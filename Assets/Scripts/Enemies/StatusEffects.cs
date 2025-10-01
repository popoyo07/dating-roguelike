using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum StatusEffect
{
    NORMAL, STUN, KEEPSHIELD, SHIELDIGNORED, VULNERABLE, IVENCIBLE
}
public class StatusEffects : MonoBehaviour
{
    public StatusEffect currentStatus;
    int round;
    RoundTracker roundTracker;
    bool waitingFor;
    private void Start()
    {
        roundTracker = GameObject.Find("CardManager").GetComponent<RoundTracker>();
    }
    public IEnumerator ResetInvencible(int currentRound)
    {
        
        round = currentRound;

        yield return new  WaitUntil(() => round != roundTracker.rounds);
        if (currentStatus == StatusEffect.IVENCIBLE)
        {
            currentStatus = StatusEffect.NORMAL;
        }
        waitingFor = false;

    }

    private void Update()
    {
        switch (currentStatus)
        {
            case StatusEffect.IVENCIBLE:
                if (!waitingFor)
                {
                    waitingFor = true;
                    StartCoroutine(ResetInvencible(roundTracker.rounds));
                }
                break;
        }
    }


}
