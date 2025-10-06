using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public TMP_Text cardNameText;
    public Image cardImageSprite;

    public void Setup(string cardName, Sprite cardNameImage)
    {
        cardNameText.text = cardName;
        cardImageSprite.sprite = cardNameImage;
    }

}