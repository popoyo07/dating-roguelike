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
    private ActionsChemist actionsChemist;
    private ActionsWizzard actionsWizzard;

    private DeckDraw cardDraw;
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
    void SetUpLocation()
    {
        if (animator != null && animator.GetBool("spawned") == false && animator.GetBool("used") == false) 
        {
            rectTransform.anchoredPosition = location;
            rectTransform.localEulerAngles = setRotation;
            //Debug.Log("For the object " + gameObject.name + " the locaton is " + rectTransform.anchoredPosition + "and the rotation is " + rectTransform.localEulerAngles);
        }
       
    }
    void LateUpdate()
    {
        SetUpLocation();
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
            cardDraw.GetComponent<ActionsKnight>() != null ||
            cardDraw.GetComponent<ActionsKnight>().cardAttaks.Count > 0 ||
            energy.GetComponent<EnergySystem>() != null &&
            hold.GetComponent<HoldCardBehavior>() != null);

        switch (cardDraw.characterClass)
        {
            case CharacterClass.KNIGHT:
                knightCardAttks = cardDraw.GetComponent<ActionsKnight>();
                break;
            case CharacterClass.CHEMIST:
                actionsChemist = cardDraw.GetComponent<ActionsChemist>();
                break;
            case CharacterClass.WIZZARD:
                actionsWizzard = cardDraw.GetComponent<ActionsWizzard>();
                break;
        }
        cardSet = true;
        yield return new WaitForSeconds(.5f);
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
        Debug.Log("card tried to run set up ");
      
        cardImage.enabled = true;
        cardImage.sprite = cardDraw.allPossibleSprites[cardNameFromList]; // assign sprite according to the name and the database
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardClicked);
        cardButton.interactable = true;
        
        hold.HoldingButton.RemoveAllListeners();
        hold.HoldingButton.AddListener(OnCardHold);
        Debug.Log("card set up ");
    }

    void OnCardHold()
    {
        hold.ShowExplanation(cardDraw.allCardDescriptions[cardNameFromList]);
    }
    private void OnCardClicked()
    {
            switch (cardDraw.characterClass)
        {
            case CharacterClass.KNIGHT:
                if( knightCardAttks == null)
                {
                    knightCardAttks = cardDraw.GetComponent<ActionsKnight>();
                }
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
                }
                else
                {
                    Debug.LogWarning("Not enough Energy");
                }
                break;
            case CharacterClass.CHEMIST:
               if(actionsChemist == null)
                {
                    actionsChemist = cardDraw.GetComponent<ActionsChemist>();
                }
                if (actionsChemist.cardEnergyCost[cardNameFromList] <= energy.energyCounter)
                {
                    Debug.Log("Enrgy cost is " + actionsChemist.cardEnergyCost[cardNameFromList] + " and the current energy is " + energy.energyCounter);
                    if (actionsChemist.cardAttaks.ContainsKey(cardNameFromList))
                    {
                        actionsChemist.cardAttaks[cardNameFromList].Invoke();

                        cardUsed = true; // <-- mark the card as used immediately
                        DiscardAndReset();
                        cardButton.interactable = false;

                    }
                    else
                    {
                        Debug.LogWarning("No action found for key: " + cardNameFromList);
                    }
                }
                else
                {
                    Debug.LogWarning("Not enough Energy");
                }
                break;
                case CharacterClass.WIZZARD:
                    if (actionsWizzard == null)
                    {
                        actionsWizzard = cardDraw.GetComponent<ActionsWizzard>();
                    }

                    if (actionsWizzard.cardEnergyCost[cardNameFromList] <= energy.energyCounter)
                    {
                        Debug.Log("Enrgy cost is " + actionsWizzard.cardEnergyCost[cardNameFromList] + " and the current energy is " + energy.energyCounter);
                        if (    actionsWizzard.cardAttaks.ContainsKey(cardNameFromList))
                        {
                            actionsWizzard.cardAttaks[cardNameFromList].Invoke();

                            cardUsed = true; // <-- mark the card as used immediately
                            DiscardAndReset();
                            cardButton.interactable = false;

                        }
                        else
                        {
                            Debug.LogWarning("No action found for key: " + cardNameFromList);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Not enough Energy");
                    }
                    break;
        }

        Debug.Log($"After Using Card RuntimeDeck {cardDraw.runtimeDeck.Count}, Discarded: {cardDraw.discardedCards.Count}");


    }


    void DiscardAndReset()
    {
        cardDraw.DiscardCard(cardNameFromList); // discards the card and adds to the list

         cardUsed = true;
        cardSet = false;
       
        StartCoroutine(StartingAnimation(1));

    }

    void ResetCardForNewCombat()
    {
        // Reset card state for new turn
        cardUsed = false;
        cardSet = false;
        

        // Clear old card name and prepare for new assignment
        cardNameFromList = null;
       

        // Reactivate button
        if (cardButton != null)
        {
            cardButton.interactable = true;
        }

       
    }

    // This method should be called by DeckDraw when assigning a new card
    public IEnumerator AssignNewCard(string newCardName)
    {
        yield return new WaitForSeconds(.15f);
        cardNameFromList = newCardName;
        cardUsed = false; 
        resetForNewTurn = false;
        SetupCardButton();
    }
}