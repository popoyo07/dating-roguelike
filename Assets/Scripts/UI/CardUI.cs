using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text cardNameText;
    public Image cardSpriteImage;
    public Button cardButton;

    private string cardName;

    public void Setup(string cardName, Sprite cardSprite)
    {
        this.cardName = cardName;
        cardNameText.text = cardName;

        if (cardSpriteImage != null)
            cardSpriteImage.sprite = cardSprite;

        if (cardButton != null)
            cardButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        Debug.LogWarning($"Card clicked: {cardName}");
    }
}