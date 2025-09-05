using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /* [System.Serializable]
     public class EnemyType
     {
         public string type;
         public GameObject enemy;
     }*/

    // public GameObject enemy;
   
   public List<GameObject> enemy;

    [SerializeField] private int poolSize;
    private Dictionary<string, GameObject> enemyPrefabs = new Dictionary<string, GameObject>();
    void Start()
    {
       /* foreach (EnemyType enemyType in enemyTypes)
        {
            EnemyObjectPool.Instance.CreatePool(enemyType.type, enemyType.enemy, enemyType.poolSize);
            enemyPrefabs.Add(enemyType.type, enemyType.enemy);
        }*/
        SpawnEnemy(3);
       
    }
    public void SpawnEnemy(int count)
    {
        List<string> availableEnemies = new List<string>(enemyPrefabs.Keys);
        for (int i = 0; i < 3; i++)
        {
            int randomEnemyIndex = Random.Range(0, availableEnemies.Count);
           // string randomEnemyTag = availableEnemies[randomEnemyIndex];

            //Spawn Point code

           // GameObject enemy2 = EnemyObjectPool.Instance.GetPooledObject(enemyPrefabs);
        }

    }
}
