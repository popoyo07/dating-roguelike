using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Point")]
    private Vector3 spawnPoint;

    [Header("Boss Settings")]
    public int roomsSpawnBoss;
    private GameObject boss;
    public GameObject sirenBoss;
    public GameObject vampireBoss;
    public GameObject idkBoss;
    private GameObject bossInstance;
    private Vector3 bossSpawn;
    private bool ifBossExists;

    [Header("Enemy Prefabs")]
    public List<GameObject> sirenList;
    public List<GameObject> vampireList;
    public List<GameObject> idkList;

    private List<GameObject> activeList;
    private List<GameObject> spawnedList = new List<GameObject>();
    private int chosenList;
    private bool enemySpawn;
    private GameObject enemyInstance;

    private BattleSystem battleSystem;

    private GameObject queuedEnemyPrefab; // next enemy chosen by player
    private bool spawnSpecificNext;

    void Start()
    {
        chosenList = Random.Range(2, 2);

        switch (chosenList)
        {
            case 0:
                activeList = sirenList;
                Debug.Log("Vampire boss");
                boss = sirenBoss;
                break;
      
             case 1:
                activeList = vampireList;
                Debug.Log("Vampire boss");
                boss = vampireBoss;
                break;
            case 2:
                activeList = idkList;
                Debug.Log("Idk boss");
                boss = idkBoss;
                break;
        }

        Debug.Log("Chosen list: " + chosenList);

        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        bossSpawn = new Vector3(0f, 1.5f, 1.94f);
        roomsSpawnBoss = 0;

        spawnPoint = new Vector3(0f, 1.3f, 1.94f);

        SpawnEnemy();
    }

    void Update()
    {
        if (battleSystem.state == BattleState.WON)
        {
            DestroyEnemy();

            if (!enemySpawn)
            {
                StartCoroutine(DelayTrash());
                enemySpawn = true;
                roomsSpawnBoss++;
            }

            DestroyBoss();
        }

        if (battleSystem.state != BattleState.WON && enemySpawn)
        {
            enemySpawn = false;
        }
    }

    public List<GameObject> GetActiveList()
    {
        return activeList;
    }

    IEnumerator DelayTrash()
    {
        yield return new WaitForSeconds(2.5f);

        if (roomsSpawnBoss < 6 || roomsSpawnBoss >= 7 || roomsSpawnBoss < 12 || roomsSpawnBoss >= 13 || roomsSpawnBoss < 18 || roomsSpawnBoss >= 19)
        {
            // Spawn queued enemy if player chose one, otherwise random
            if (spawnSpecificNext && queuedEnemyPrefab != null)
            {
                SpawnSpecificEnemy(queuedEnemyPrefab);
                spawnSpecificNext = false;
                queuedEnemyPrefab = null;
            }
            else
            {
                SpawnEnemy();
            }

            ifBossExists = false;
        }

        if ((roomsSpawnBoss == 6 || roomsSpawnBoss == 12 || roomsSpawnBoss == 18) && !ifBossExists)
        {
            DestroyEnemy();
            ifBossExists = true;
            bossInstance = Instantiate(boss, bossSpawn, Quaternion.identity);
        }
    }

    public void SpawnEnemy()
    {
        if (activeList.Count > 0)
          {
            int randomEnemy = Random.Range(0, activeList.Count);
            enemyInstance = Instantiate(activeList[randomEnemy], spawnPoint, Quaternion.identity);
            spawnedList.Add(enemyInstance);
          }
    }

    public void SpawnSpecificEnemy(GameObject prefab)
    {
        if (prefab != null)
        {
            enemyInstance = Instantiate(prefab, spawnPoint, Quaternion.identity);
            spawnedList.Add(enemyInstance);
            Debug.Log($"Spawned specific enemy prefab: {prefab.name}");
        }
        else
        {
            Debug.LogWarning("Tried to spawn a null enemy prefab.");
        }
    }

    public void QueueSpecificEnemy(GameObject prefab)
    {
        if (prefab != null)
        {
            queuedEnemyPrefab = prefab;
            spawnSpecificNext = true;
            Debug.Log($"Queued specific enemy for next spawn: {prefab.name}");
        }
        else
        {
            Debug.LogWarning("Tried to queue a null enemy prefab.");
        }
    }

    public void DestroyEnemy()
    {
        if (spawnedList.Contains(enemyInstance))
        {
            spawnedList.Remove(enemyInstance);
            Destroy(enemyInstance);
        }
    }
    public void DestroyBoss()
    {
        if (bossInstance != null)
        {
            Destroy(bossInstance);
        }
    }

    public void skipBossFight()
    {
        DestroyBoss();

        battleSystem.state = BattleState.WON;

    }
}