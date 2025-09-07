using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string respnseText;
    [SerializeField] private DialogueObject dialogueObject;

    public string ResonpseText => respnseText;
    public DialogueObject DialogueObject => dialogueObject;

}
