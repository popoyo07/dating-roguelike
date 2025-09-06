using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
   // [SerializeField] private string enemy;
    [SerializeField] private int enemy;
    [SerializeField] private int poolSize;
    public List<GameObject> enemyPrefabs;
   // private string randomEnemy;
    private int randomEnemy;
   // private Dictionary<string, GameObject> enemyPrefabs2 = new Dictionary<string, GameObject>();

   /* void Awake()
    {
        enemyPrefabs2.Add("Enemy1", enemyPrefabs[0]);
        enemyPrefabs2.Add("Enemy2", enemyPrefabs[1]);
        enemyPrefabs2.Add("Enemy3", enemyPrefabs[2]);
        enemyPrefabs2.Add("Enemy4", enemyPrefabs[3]);  
    }*/
    void Start()
    {
        for (int i = 0; i < 3; i++)
         {
           // Debug.Log("test2: " + i);
            EnemyObjectPool.Instance.CreatePool(enemy, enemyPrefabs[i], poolSize);
         }

        SpawnEnemy(3);
    }
    public void SpawnEnemy(int count)
    {
       // List<string> availableEnemies = new List<string>(enemyPrefabs2.Keys);

        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("test: " + i);
            // randomEnemy = availableEnemies[Random.Range(0, availableEnemies.Count)];
            randomEnemy = Random.Range(0, enemyPrefabs.Count);
            //Instantiate(enemy[randomEnemy]);
            GameObject spawnEnemy = EnemyObjectPool.Instance.GetPooledObject(randomEnemy);
        }
    }
}
