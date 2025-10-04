using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private Dialogue[] dialogue;
    [SerializeField] private Response[] responses;
    [SerializeField] private LovyCounting LovyCounting;


    public Dialogue[] Dialogue => dialogue;

    public LovyCounting LovyCount => LovyCounting;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;

}

