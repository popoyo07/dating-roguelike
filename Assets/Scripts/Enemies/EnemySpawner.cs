using System.Collections.Generic;
//using System.Numerics;
using System.Threading;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   // [SerializeField] private string enemy;
    [SerializeField] private int enemy;
    [SerializeField] private int poolSize;
    public List<GameObject> enemyPrefabs;
    // private string randomEnemy;
    [SerializeField] private List<Vector3> spawnPoints;
    private int randomEnemy;
    private int randomSpawn;
    
   // private Dictionary<string, GameObject> enemyPrefabs2 = new Dictionary<string, GameObject>();

  /*  void Awake()
    {
        enemyPrefabs.Add(enemyPrefabs[0]);
        enemyPrefabs.Add(enemyPrefabs[1]);
        enemyPrefabs.Add(enemyPrefabs[2]);
        enemyPrefabs.Add(enemyPrefabs[3]);  
    }*/
    void Start()
    {
        spawnPoints.Add(new Vector3(2f, 0.75f, -6.73f));
        spawnPoints.Add(new Vector3(0f, 0.75f, -6.73f));
        spawnPoints.Add(new Vector3(-2f, 0.75f, -6.73f));
        /* for (int i = 0; i < enemyPrefabs.Count; i++)
         {
             // Debug.Log("test2: " + i);
             EnemyObjectPool.Instance.CreatePool(enemy, enemyPrefabs[i], poolSize);
          }*/

        SpawnEnemy(3);
    }
    public void SpawnEnemy(int count)
    {
        // List<int> availableEnemies = new List<int>(enemyPrefabs.Count);
        // List<Transform> availableSpawn = new List<Transform>(spawnPoints);

        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("test: " + i);
            //randomEnemy = availableEnemies[Random.Range(0, enemyPrefabs.Count)];
            randomEnemy = Random.Range(0, enemyPrefabs.Count);
            // randomSpawn = Random.Range(0, spawnPoints.Count);
            Vector3 chosenSpawn = spawnPoints[randomSpawn];
            Instantiate(enemyPrefabs[randomEnemy], chosenSpawn, Quaternion.identity);
            spawnPoints.RemoveAt(randomSpawn);
            // randomSpawn = Random.Range(0, availableSpawn.Count);
            // Transform selectedSpawn = availableSpawn[randomSpawn];
            // GameObject spawnEnemy = EnemyObjectPool.Instance.GetPooledObject(randomEnemy);
            // Instantiate(enemyPrefabs[randomEnemy], selectedSpawn.position, selectedSpawn.rotation);
            // availableSpawn.RemoveAt(randomSpawn);

            /*  if (spawnEnemy != null)
                 {
                     randomSpawn = Random.Range(0, availableSpawn.Count);
                     Transform selectedSpawn = availableSpawn[randomSpawn];
                     spawnEnemy.transform.position = selectedSpawn.position;
                     availableSpawn.RemoveAt(randomSpawn);  
                 } */
        }
    }
}
