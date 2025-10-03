using UnityEngine;

public class DeckCards : DeckManagement
{
    GameObject showCardsInDeck;

    private void Awake()
    {
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        showCardsInDeck = GameObject.FindGameObjectWithTag("ShowCards");
    }
}
