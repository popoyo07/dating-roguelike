using Unity.VisualScripting;
using UnityEngine;

public class CounterImages : MonoBehaviour
{

    [SerializeField] private Sprite[] spriteList;
    [SerializeField] private DeckDraw deckDraw;
    private string playerClass;

    private void Start()
    {
        deckDraw = GameObject.Find("CardManager").GetComponent<DeckDraw>();
        playerClass = deckDraw.
    }



}
