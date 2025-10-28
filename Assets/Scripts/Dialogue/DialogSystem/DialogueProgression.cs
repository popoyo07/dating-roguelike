using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueProgression")]
public class DialogueProgression : ScriptableObject
{
    public int currentDialogueIndex = 0;
    public int phase = 0;
}
