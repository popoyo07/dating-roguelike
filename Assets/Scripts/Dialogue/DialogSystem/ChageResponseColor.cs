using UnityEngine;
using UnityEngine.UI;

public class ChageResponseColor : MonoBehaviour
{
    public Image responseImage;
    public EnemySpawner enemySpawner;

    private void Awake()
    {
        responseImage = GetComponent<Image>();
        enemySpawner = GameObject.FindWithTag("EnemyS").GetComponent<EnemySpawner>();        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeBackground();
    }

    private void ChangeBackground()
    {
        var boss = enemySpawner.boss;

        if (boss != null) 
        {
            if (boss == enemySpawner.sirenBoss)
            {
                Color newColor;
                if (ColorUtility.TryParseHtmlString("#7CCCFF", out newColor))
                {
                    responseImage.color = newColor;
                }
            }
            else if (boss == enemySpawner.idkBoss)
            {
                Color newColor;
                if (ColorUtility.TryParseHtmlString("#FFF747", out newColor))
                {
                    responseImage.color = newColor;
                }
            }
            else
            {
                Color newColor;
                if (ColorUtility.TryParseHtmlString("#FF6C6C", out newColor))
                {
                    responseImage.color = newColor;
                }

            }
        }


    }
}
