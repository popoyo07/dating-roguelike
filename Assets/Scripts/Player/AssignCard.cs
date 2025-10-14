using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class AssignCard : MonoBehaviour
{
    RectTransform rectTransform;
    public Vector3 location;
    public Vector3 setRotation;
    Animator animator;
    public string cardNameFromList;
    public Button cardButton;
    public HoldCardBehavior hold;
    public bool cardUsed;

    private BattleSystem BSystem;
    private ActionsKnight knightCardAttks; // will eventually change to swithc statment that decides from which function to pu
    private ActionsRogue actionsRogue;

    private DeckDraw cardDraw;
    private bool displayTxt;
    private bool cardSet;
    private Image cardImage;

    bool resetForNewTurn;
    EnergySystem energy;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();    
        cardImage = GetComponent<Image>();
        BSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        cardDraw = GameObject.Find("CardManager").GetComponent<DeckDraw>();
        cardButton = GetComponent<Button>();
        cardUsed = false;
        resetForNewTurn = false;
        energy = GameObject.Find("Managers").GetComponentInChildren<EnergySystem>();
        hold = GetComponent<HoldCardBehavior>();
        StartCoroutine(StartingAnimation(0));
        StartCoroutine(InitializeCard());

    }
    IEnumerator StartingAnimation(int i)
    {
        if (animator != null)
        {
            if (i == 0)
            {
                animator.SetBool("spawned", true);
                yield return new WaitForSeconds(1f);
                animator.SetBool("spawned", false);
                rectTransform.position = location;
                rectTransform.localEulerAngles = setRotation;
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
    void Start()
    {
     
    }

    private IEnumerator InitializeCard()
    {
        // Wait for card actions to be initialized
        yield return new WaitUntil(() =>
            cardDraw.GetComponent<ActionsKnight>() != null &&
            cardDraw.GetComponent<ActionsKnight>().cardAttaks.Count > 0 &&
            energy.GetComponent<EnergySystem>() != null &&
            hold.GetComponent<HoldCardBehavior>() != null);

        switch (cardDraw.characterClass)
        {
            case CharacterClass.KNIGHT:
                knightCardAttks = cardDraw.GetComponent<ActionsKnight>();
                break;
            case CharacterClass.ROGUE:
                actionsRogue = cardDraw.GetComponent<ActionsRogue>();
                break;
            case CharacterClass.WIZZARD:
                break;
        }
        cardSet = true;
        SetupCardButton();
    }

    void Update()
    {
        if (BSystem == null) return;

      

        // Reset card for new turn
        if (BSystem.state == BattleState.PLAYERTURN && cardUsed)
        {
            resetForNewTurn = false;
        }

        // Auto-discard if card wasn't used by end of turn
        if (BSystem.state == BattleState.ENDPLAYERTURN && !cardUsed && gameObject.activeInHierarchy && !resetForNewTurn)
        {
            ResetForNewTurn();

        }

        if (BSystem.state == BattleState.WON && !resetForNewTurn && !cardUsed)
        {
            ResetForNewTurn();
        }
    }
    void ResetForNewTurn()
    {
        resetForNewTurn = true;
        DiscardAndReset();
        ResetCardForNewCombat();
    }
    public void SetupCardButton()
    {
        if (cardButton == null || knightCardAttks == null) return;
        cardImage.enabled = true;
        cardImage.sprite = cardDraw.allPossibleSprites[cardNameFromList]; // assign sprite according to the name and the database
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClicked);
        cardButton.interactable = true;
        
        hold.HoldingButton.RemoveAllListeners();
        hold.HoldingButton.AddListener(OnCardHold);
    }

    void OnCardHold()
    {
        hold.ShowExplanation(cardDraw.allCardDescriptions[cardNameFromList]);
    }
    private void OnCardClicked()
    {
            

        if (knightCardAttks.cardEnergyCost[cardNameFromList] <= energy.energyCounter)
        {
            Debug.Log("Enrgy cost is " + knightCardAttks.cardEnergyCost[cardNameFromList] + " and the current energy is " + energy.energyCounter);
            if (knightCardAttks.cardAttaks.ContainsKey(cardNameFromList))
            {
                knightCardAttks.cardAttaks[cardNameFromList].Invoke();

                cardUsed = true; // <-- mark the card as used immediately
                DiscardAndReset();
                cardButton.interactable = false;

            }
            else
            {
                Debug.LogWarning("No action found for key: " + cardNameFromList);
            }
        } else
        {
            Debug.LogWarning("Not enough Energy");
        }
        
    }


    void DiscardAndReset()
    {
        if (knightCardAttks != null && knightCardAttks.deckManagement != null)
        {
            knightCardAttks.deckManagement.DiscardCard(cardNameFromList);
        }
      
        cardUsed = true;
        cardSet = false;
        displayTxt = false;
        StartCoroutine(StartingAnimation(1));

    }

    void ResetCardForNewCombat()
    {
        // Reset card state for new turn
        cardUsed = false;
        cardSet = false;
        displayTxt = false;

        // Clear old card name and prepare for new assignment
        cardNameFromList = null;
       

        // Reactivate button
        if (cardButton != null)
        {
            cardButton.interactable = true;
        }

       
    }

    // This method should be called by DeckDraw when assigning a new card
    public void AssignNewCard(string newCardName)
    {
        cardNameFromList = newCardName;
        displayTxt = false; // Allow text to be updated in next Update
        cardUsed = false; 
        resetForNewTurn = false;
        SetupCardButton();
    }
}