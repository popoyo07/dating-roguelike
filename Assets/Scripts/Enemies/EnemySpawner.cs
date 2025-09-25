using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

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
    //  private int randomEnemy;
    public List<GameObject> sirenList;
    public List<GameObject> vampireList;
    public List<GameObject> idkList;
    private List<GameObject> activeList;
    private List<GameObject> spawnedList = new List<GameObject>();
    private int randomEnemy;
    private int chosenList;
    private int chosenList2;
    private bool enemySpawn;
    private GameObject enemyInstance;

    private BattleSystem battleSystem;
    private GameObject[] activeEnemies;

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

        bossSpawn = new Vector3(0f, 0.75f, -6.73f);

        spawnPoint = new Vector3(0f, 1.3f, -6.73f);

        // Spawn the first forced enemy if already set
        SpawnEnemy();
    }

    void Update()
    {
        if (battleSystem.state == BattleState.WON)
        {
              /*foreach (GameObject obj in activeEnemies)
              {
                  if (obj != null) Destroy(obj);
              }*/

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

      /*  if (roomsSpawnBoss == 6 || roomsSpawnBoss == 12 || roomsSpawnBoss == 18 && !ifBossExists)
        {
            ifBossExists = true;
            StartCoroutine(DelayBoss());
        }*/
    }

    IEnumerator DelayTrash()
    {
        yield return new WaitForSeconds(2.5f);

        if (roomsSpawnBoss < 6 || roomsSpawnBoss >= 7 || roomsSpawnBoss < 12 || roomsSpawnBoss >= 13 || roomsSpawnBoss < 18 || roomsSpawnBoss >= 19)
        {
            SpawnEnemy();
            ifBossExists = false;
        }
        if (roomsSpawnBoss == 6 || roomsSpawnBoss == 12 || roomsSpawnBoss == 18 && !ifBossExists)
        {
            DestroyEnemy();
            ifBossExists = true;
           // StartCoroutine(DelayBoss());
            bossInstance = Instantiate(boss, bossSpawn, Quaternion.identity);
        }
    }

   /* IEnumerator DelayBoss()
    {
        yield return new WaitForSeconds(2.5f);
        bossInstance = Instantiate(boss, bossSpawn, Quaternion.identity);
    }*/

    public void SpawnEnemy()
    {
        // randomEnemy = Random.Range(0, enemyPrefabs.Count);
        // Instantiate(enemyPrefabs[randomEnemy], spawnPoint, Quaternion.identity);
       // randomEnemy = Random.Range(0, activeList.Count);
       // enemyInstance = Instantiate(activeList[randomEnemy], spawnPoint, Quaternion.identity);
      //  spawnedList.Add(enemyInstance);

        if (activeList.Count > 0)
          {
            int randomEnemy = Random.Range(0, activeList.Count);
            enemyInstance = Instantiate(activeList[randomEnemy], spawnPoint, Quaternion.identity);
            spawnedList.Add(enemyInstance);
          }

          //activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
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
}