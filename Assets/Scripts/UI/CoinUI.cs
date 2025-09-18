using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinUI;
    BattleSystem battleSystem;
    bool banana;
    void Awake()
    {
        coinUI = this.gameObject.GetComponent<TextMeshProUGUI>();
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
    }

    private void Update()
    {
        if (battleSystem.state == BattleState.WON && banana == false)
        {
            Debug.Log("Bananas Added");
            StartCoroutine(AddCoin());
            banana = true;
        } 
        else if (battleSystem.state != BattleState.WON)
        {
            banana = false;
        }
    }

    private IEnumerator AddCoin()
    {
        coinUI.SetText("+5 Coin");
        yield return new WaitForSeconds(5f);
        coinUI.SetText("");
    }


}
