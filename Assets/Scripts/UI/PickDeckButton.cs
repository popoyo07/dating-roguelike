using UnityEngine;
using UnityEngine.UI;

public class PickDeckButton : MonoBehaviour, IDataPersistence 
{
    Button b;
    [SerializeField] CharacterClass c;
    DataPersistenceManager DATA;

    #region Save and Load

    public void LoadData(GameData data)
    {
       
    }

    public void SaveData(ref GameData data)
    {
        data.playerClass = this.c;
    }

    #endregion


    public void ChangeToKnight()
    {
        c = CharacterClass.KNIGHT;
    }

    public void ChangeToRogue()
    {
        c = CharacterClass.ROGUE;
    }

    public void ChangeToWizzard()
    {
        c = CharacterClass.WIZZARD;
    }
}
