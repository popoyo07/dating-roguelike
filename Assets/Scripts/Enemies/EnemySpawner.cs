using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField] private string enemy;
    // private string randomEnemy;
    [SerializeField] private List<Vector3> spawnPoints;

    private int randomEnemy;
    private int randomSpawn;
    public int roomsSpawnBoss;
    [SerializeField] private int enemy;
    [SerializeField] private int poolSize;

    public GameObject boss1;
    private GameObject bossInstance;
    public GameObject[] pooooooooop2;
    public List<GameObject> enemyPrefabs;

    private Vector3 bossSpawn;
    private bool ifBossExists;

    BattleSystem battleSystem;
    bool enemySpawn;

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
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        roomsSpawnBoss = 0;
        bossSpawn = new Vector3(0f, 0.75f, -6.73f);
        // spawnPoints.Add(new Vector3(2f, 0.75f, -6.73f));
        spawnPoints.Add(new Vector3(0f, 0.75f, -6.73f));
        //  spawnPoints.Add(new Vector3(-2f, 0.75f, -6.73f));
        /* for (int i = 0; i < enemyPrefabs.Count; i++)
         {
             // Debug.Log("test2: " + i);
             EnemyObjectPool.Instance.CreatePool(enemy, enemyPrefabs[i], poolSize);
          }*/

        SpawnEnemy(1);
    }

    void Update()
    {
        if (battleSystem.state == BattleState.WON)
        {
            foreach (GameObject obj in pooooooooop2)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }

            }

            if (!enemySpawn)
            {
                StartCoroutine(DelayTrash());
                enemySpawn = true;
                roomsSpawnBoss++;
            }
        }

        if (battleSystem.state != BattleState.WON && enemySpawn == true)
        {
            enemySpawn = false;
        }

        if (roomsSpawnBoss == 6 && ifBossExists == false)
        {
            ifBossExists = true;
            StartCoroutine(DelayBoss());
        }
    }

    IEnumerator DelayTrash()
    {
        yield return new WaitForSeconds(2.5f);

        if (roomsSpawnBoss < 6 || roomsSpawnBoss >= 7)
        {
            SpawnEnemy(1);
            DestroyBoss();
            ifBossExists = false;
        }
    }

    IEnumerator DelayBoss()
    {
        yield return new WaitForSeconds(2.5f);
        bossInstance = Instantiate(boss1, bossSpawn, Quaternion.identity);
    }

    
    public void SpawnEnemy(int count)
    {
        // List<int> availableEnemies = new List<int>(enemyPrefabs.Count);
        // List<Transform> availableSpawn = new List<Transform>(spawnPoints);

        for (int i = 0; i < 1; i++)
        {
            //Debug.Log("test: " + i);
            //randomEnemy = availableEnemies[Random.Range(0, enemyPrefabs.Count)];
            randomEnemy = Random.Range(0, enemyPrefabs.Count);
            // randomSpawn = Random.Range(0, spawnPoints.Count);
            Vector3 chosenSpawn = spawnPoints[randomSpawn];
            Instantiate(enemyPrefabs[randomEnemy], chosenSpawn, Quaternion.identity);
            //spawnPoints.RemoveAt(randomSpawn);
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
        pooooooooop2 = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void DestroyBoss()
    {
        if (boss1 != null) //battleSystem.state == BattleState.WON
        {
            Destroy(bossInstance);
        }
    }
}
