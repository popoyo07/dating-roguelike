using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CardUI : MonoBehaviour
{
    public TMP_Text cardNameText;
    public Image cardSpriteImage;

    public void Setup(string cardName, Sprite cardImage)
    {
        cardNameText.text = cardName;
        cardSpriteImage.sprite = cardImage;
    }
}