using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    public TMP_Text cardNameText;

    public void Setup(string cardName)
    {
        cardNameText.text = cardName;
    }
}