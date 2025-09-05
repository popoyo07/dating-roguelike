using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int randomEnemy;
    public List<GameObject> enemy;

   // [SerializeField] private int poolSize;
   // private Dictionary<string, GameObject> enemyPrefabs = new Dictionary<string, GameObject>();
    void Start()
    {
        SpawnEnemy(3);
    }
    public void SpawnEnemy(int count)
    {
        for (int i = 0; i < 3; i++)
        {
            randomEnemy = Random.Range(0, enemy.Count);
            Instantiate(enemy[randomEnemy]);
        }
    }
}
