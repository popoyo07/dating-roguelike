using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// later will need another check that makes a counter for enemy turn and not only player turm
public enum StatusEffect
{
    NORMAL, STUN, KEEPSHIELD, SHIELDIGNORED, VULNERABLE, IVINCIBLE, WEAK
}
public class StatusEffects : MonoBehaviour
{
    public StatusEffect currentStatus;
    RoundTracker roundTracker;
    bool waitingFor;
    bool isPlayer;
    private void Start()
    {
        roundTracker = GameObject.Find("CardManager").GetComponent<RoundTracker>();
        currentStatus = StatusEffect.NORMAL;
        if (gameObject.CompareTag("Player"))// the check 
        {
            isPlayer = true;
        }

    }
    public IEnumerator ResetStatusAfterRound(int currentRound)
    {
                    waitingFor = true;

        if (!isPlayer)
        {
            // roun tracker affecting until player turn starts 
            int round = roundTracker.playerRounds;

            yield return new  WaitUntil(() => roundTracker.playerRounds >  round);
       
            currentStatus = StatusEffect.NORMAL;
        }
        else
        {
            // round tracker for affecting player during until their turn ends 
            int round = roundTracker.enemyRounds;
            yield return new WaitUntil(() => roundTracker.enemyRounds > round);
            currentStatus = StatusEffect.NORMAL;

        }
        Debug.LogWarning("New status effect is " +  currentStatus);
    }

    private void Update()
    {
        switch (currentStatus)
        {
            // should run reset on any of this options 
            case StatusEffect.WEAK:
            case StatusEffect.IVINCIBLE:           
            case StatusEffect.STUN:           
            case StatusEffect.VULNERABLE:
                if (!waitingFor) 
                {
                    StartCoroutine(ResetStatusAfterRound(roundTracker.playerRounds));
                }
                break;
            default:
                    waitingFor = false;
                    break;

        }
    }


}
