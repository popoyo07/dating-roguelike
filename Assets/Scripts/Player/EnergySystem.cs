using UnityEngine;

public class EnergySystem : MonoBehaviour, IDataPersistence
{
    public int energyCounter;
    public int maxEnergy;
    public int energyBuff;
    BattleSystem bSystem;

    #region Save and Load

    public void LoadData(GameData data)
    {
        Debug.Log("Load data is energySystem");
        this.energyBuff = data.energyBuff;
    }

    public void SaveData(ref GameData data)
    {
        data.energyBuff = this.energyBuff;
    }

    #endregion

    void Start()
    {
        maxEnergy += energyBuff;
   
        bSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (bSystem.state)
        {
            case BattleState.ENDPLAYERTURN:
                ResetCounter();

                break;
            case BattleState.WON:
                ResetCounter();

                break;
        }

    }
    void ResetCounter() // reset how much energy we have 
    {
        if (energyCounter != maxEnergy) 
        {
            energyCounter = maxEnergy;
        }
    }
}
