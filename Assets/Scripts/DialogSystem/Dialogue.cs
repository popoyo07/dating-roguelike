using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField][TextArea] private string dialogueText;
    [SerializeField] private string characterName;

    public string DialogueText => dialogueText;
    public string CharacterName => characterName;

}
