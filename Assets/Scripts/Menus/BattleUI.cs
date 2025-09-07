using UnityEngine;

public class BattleUI : MonoBehaviour
{
    BattleSystem bSystem;
    GameObject[] cards;
    GameObject endTurn;
    bool doing;

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
}
