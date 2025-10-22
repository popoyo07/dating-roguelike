using UnityEngine;
using UnityEngine.UI;

public class PickDeckButton : MonoBehaviour 
{
    Button b;
    [SerializeField] MusicManager c;
    DataPersistenceManager DATA;

    private void Awake()
    {
        c = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    public void ChangeToKnight()
    {
        c.c = CharacterClass.KNIGHT;
        Debug.Log("choice " + c.c);
    }

    public void ChangeToRogue()
    {
        c.c = CharacterClass.CHEMIST;
        Debug.Log("choice " + c.c);
    }

    public void ChangeToWizzard()
    {
        c.c = CharacterClass.WIZZARD;
        Debug.Log("choice " + c.c);
    }
}
