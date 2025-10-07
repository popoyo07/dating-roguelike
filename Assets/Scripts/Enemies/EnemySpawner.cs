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

    void Start()
    {
        chosenList = Random.Range(0, 3);

        switch (chosenList)
        {
            case 0:
                activeList = sirenList;
                Debug.Log("Siren boss");
                boss = sirenBoss;
                break;
            case 1:
                activeList = sirenList;
                Debug.Log("Siren boss");
                boss = sirenBoss;
                break;
            case 2:
                activeList = sirenList;
                Debug.Log("Siren boss");
                boss = sirenBoss;
                break;

                /*case 1:
                    activeList = vampireList;
                    Debug.Log("Vampire boss");
                    boss = vampireBoss;
                    break;
                /case 2:
                    activeList = idkList;
                    Debug.Log("Idk boss");
                    boss = idkBoss;
                    break;*/
        }

        Debug.Log("Chosen list: " + chosenList);

        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        bossSpawn = new Vector3(0f, 1.5f, -6.73f);
        roomsSpawnBoss = 0;

        spawnPoint = new Vector3(0f, 1.3f, -6.73f);

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

    /*public List<GameObject> GetActiveList()
    {
        return activeList;
    }*/

    IEnumerator DelayTrash()
    {
        yield return new WaitForSeconds(2.5f);

        if (roomsSpawnBoss < 7 || roomsSpawnBoss >= 8 || roomsSpawnBoss < 14 || roomsSpawnBoss >= 15|| roomsSpawnBoss < 21 || roomsSpawnBoss >= 22)
        {
            SpawnEnemy();
            ifBossExists = false;
        }
        if (roomsSpawnBoss == 7 || roomsSpawnBoss == 14 || roomsSpawnBoss == 22 && !ifBossExists)
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

    /*public void SpawnPickedEnemy(int enemyIndex)
    {
        if (enemyIndex >= 0 && enemyIndex < activeList.Count)
        {
            GameObject pickedEnemy = activeList[enemyIndex];
            enemyInstance = Instantiate(pickedEnemy, spawnPoint, Quaternion.identity);
            spawnedList.Add(enemyInstance);
            Debug.Log($"Spawned specific enemy: {pickedEnemy.name}");
        }
        else
        {
            Debug.LogWarning("Invalid enemy index for SpawnPickedEnemy.");
        }
    }*/

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
}