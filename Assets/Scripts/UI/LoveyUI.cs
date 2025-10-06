using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LoveyUI : MonoBehaviour
{
    [SerializeField] private RectTransform loveyPanel;
    public DeckManagement deckManager;

    public TextMeshProUGUI[] texts;

    void Awake()
    {
        texts = loveyPanel.GetComponentsInChildren<TextMeshProUGUI>();

        //Debug.Log(texts.Length);
    }
}
