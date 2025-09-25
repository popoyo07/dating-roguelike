using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Point")]
    private Vector3 spawnPoint;

    [Header("Boss Settings")]
    public int roomsSpawnBoss;
    public GameObject boss1;
    private GameObject bossInstance;
    private Vector3 bossSpawn;
    private bool ifBossExists;

    [Header("Enemy Prefabs")]
    public List<GameObject> enemyPrefabs;
    private bool enemySpawn;

    public List<GameObject> bossPrefabs;

    private BattleSystem battleSystem;
    private GameObject[] activeEnemies;

    void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        bossSpawn = new Vector3(0f, 0.75f, -6.73f);

        roomsSpawnBoss = 0;
        spawnPoint = new Vector3(0f, 1.3f, -6.73f);

        // Spawn the first forced enemy if already set
        SpawnEnemy();
    }

    void Update()
    {
        if (battleSystem.state == BattleState.WON)
        {
            foreach (GameObject obj in activeEnemies)
            {
                if (obj != null) Destroy(obj);
            }

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

        if (roomsSpawnBoss == 6 || roomsSpawnBoss == 12 || roomsSpawnBoss == 18 && !ifBossExists)
        {
            ifBossExists = true;
            StartCoroutine(DelayBoss());
        }
    }

    IEnumerator DelayTrash()
    {
        yield return new WaitForSeconds(2.5f);

        if (roomsSpawnBoss < 6 || roomsSpawnBoss >= 7 || roomsSpawnBoss < 12 || roomsSpawnBoss >= 13 || roomsSpawnBoss < 18 || roomsSpawnBoss >= 19)
        {
            SpawnEnemy();
            ifBossExists = false;
        }
    }

    IEnumerator DelayBoss()
    {
        yield return new WaitForSeconds(2.5f);
        bossInstance = Instantiate(boss1, bossSpawn, Quaternion.identity);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefabs[0], spawnPoint, Quaternion.identity);

        activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void DestroyBoss()
    {
        if (bossInstance != null)
        {
            Destroy(bossInstance);
        }
    }
}