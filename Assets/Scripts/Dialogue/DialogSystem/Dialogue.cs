using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField][TextArea] private string dialogueText;
    [SerializeField] private string characterName;
    [SerializeField] private Sprite characterImage;
    [SerializeField] private bool dialogueEnd;

    public string DialogueText => dialogueText;
    public string CharacterName => characterName;

    public Sprite CharacterImage => characterImage;

    public bool DialogueEnd => dialogueEnd;
}
