using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    BattleSystem bSystem;
    GameObject[] cards;
    //GameObject endTurn;
    bool doing;
    Button endTurnB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        cards = GameObject.FindGameObjectsWithTag("Cards");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (bSystem.state) 
        {
            case BattleState.PLAYERTURN: // set cards actie whem fighting
                if (!doing) {
                    for (int i = 0; i < cards.Length; i++)
                    {
                        cards[i].SetActive(true);
                    }
                    StartCoroutine(GetAssignButton(0.5f));  /// delay ssecods
                    doing = true;
                }
                break;
            case BattleState.WON: // set cards inactive after combat ends 
                if (!doing)
                {
                    for (int i = 0; i < cards.Length; i++)
                    {
                        cards[i].SetActive(false);
                    }
                }
                break;
            default:
                doing = false;  
                break;
        }
    }

    IEnumerator GetAssignButton(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (endTurnB == null)
        {
            endTurnB = GameObject.Find("EndTurn").GetComponent<Button>();
        }
        if (endTurnB != null)
        { 
            // add endplayerTurn to button
            endTurnB.onClick.AddListener(() =>
            {
                StartCoroutine(bSystem.EndPlayerTurn());
            });
        }
    }


}
