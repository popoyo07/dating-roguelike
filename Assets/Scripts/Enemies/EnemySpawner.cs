using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rewards;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Point")]
    private Vector3 spawnPoint; // Default spawn position for regular enemies

    [Header("Boss Settings")]
    public int roomsSpawnBoss;        // Counter for rooms visited to determine boss spawn
    public GameObject boss;          // Current boss prefab reference
    public GameObject sirenBoss;      // Siren boss prefab
    public GameObject vampireBoss;    // Vampire boss prefab
    public GameObject idkBoss;        // Unknown boss prefab
    private GameObject bossInstance;  // Instance of the spawned boss
    private Vector3 bossSpawn;        // Spawn position for bosses
    private bool ifBossExists;        // Tracks if a boss is already spawned

    [Header("Enemy Prefabs")]
    public List<GameObject> sirenList;   // List of regular enemies for Siren type
    public List<GameObject> vampireList; // List of regular enemies for Vampire type
    public List<GameObject> idkList;     // List of regular enemies for "idk" type

    private List<GameObject> activeList;           // Current active enemy list
    private List<GameObject> spawnedList = new List<GameObject>(); // Keeps track of spawned enemies
    private int chosenList;                        // Randomly chosen enemy list index
    private bool enemySpawn;                       // Tracks if enemy has been spawned after battle
    private GameObject enemyInstance;              // Current enemy instance

    private BattleSystem battleSystem;             // Reference to BattleSystem script
    private Rewards rewards;

    private GameObject queuedEnemyPrefab;          // Next enemy chosen by player
    private bool spawnSpecificNext;                // Flag to spawn the queued enemy next

    [Header("Audio")]
    public AudioSource sirenBossMusic;
    public AudioSource karnaraBossMusic;
    public AudioSource vampBossMusic;
    public AudioSource defultMusic;

    void Start()
    {
        // Randomly choose an enemy list
        chosenList = Random.Range(0, 3); // Note: Random.Range(2,2) always returns 2

        switch (chosenList)
        {
            case 0:
                activeList = sirenList;
                Debug.Log("Siren boss");
                boss = sirenBoss;
                break;

            case 1:
                activeList = idkList;
                Debug.Log("Idk boss");
                boss = idkBoss;
                break;

            case 2:
                activeList = vampireList;
                Debug.Log("Vampire boss");
                boss = vampireBoss;
                break;
        }

        Debug.Log("Chosen list: " + chosenList);

        // Get BattleSystem reference
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        rewards = GameObject.FindWithTag("RewardsM").GetComponent<Rewards>();

        // Set boss and enemy spawn positions
        bossSpawn = new Vector3(0f, 1.5f, 1.94f);
        roomsSpawnBoss = 0;

        spawnPoint = new Vector3(0f, 1.3f, 1.94f);

        // Spawn the first enemy
        SpawnEnemy();
    }

    void Update()
    {
        // Handle what happens when the battle is won
        if (battleSystem.state == BattleState.WON)
        {
            DestroyEnemy(); // Remove current enemy

            if (!enemySpawn)
            {
                StartCoroutine(DelayTrash()); // Wait and spawn next enemy or boss
                enemySpawn = true;
                roomsSpawnBoss++; // Increment rooms visited
            }

            DestroyBoss(); // Remove boss if exists
        }

        // Reset enemySpawn when battle is not won
        if (battleSystem.state != BattleState.WON && enemySpawn)
        {
            enemySpawn = false;
        }
    }

    // Returns the currently active enemy list
    public List<GameObject> GetActiveList()
    {
        return activeList;
    }

    IEnumerator DelayTrash()
    {
        yield return new WaitForSeconds(2.5f); // Wait 2.5 seconds before spawning

        // Check if next spawn is a normal enemy or boss
        if (roomsSpawnBoss < 2 || roomsSpawnBoss >= 3 || roomsSpawnBoss < 4 || roomsSpawnBoss >= 5 || roomsSpawnBoss < 6 || roomsSpawnBoss >= 7)
        {
            // Spawn queued enemy if player chose one
            if (spawnSpecificNext && queuedEnemyPrefab != null)
            {
                SpawnSpecificEnemy(queuedEnemyPrefab);
                spawnSpecificNext = false;
                queuedEnemyPrefab = null;
            }
            else
            {
                SpawnEnemy(); // Spawn a random enemy from the active list
            }

            ifBossExists = false;
        }

        // Spawn boss on specific rooms
        if ((roomsSpawnBoss == 2 || roomsSpawnBoss == 4 || roomsSpawnBoss == 6) && !ifBossExists)
        {
            DestroyEnemy(); // Clear normal enemies
            ifBossExists = true;
            bossInstance = Instantiate(boss, bossSpawn, Quaternion.identity); // Spawn boss

            // Play corresponding boss music
            PlayBossMusic(boss);
        }
    }

    // Spawn a random enemy from the active list
    public void SpawnEnemy()
    {
        if (activeList.Count > 0)
        {
            int randomEnemy = Random.Range(0, activeList.Count);
            enemyInstance = Instantiate(activeList[randomEnemy], spawnPoint, Quaternion.identity);
            spawnedList.Add(enemyInstance);

            // Link the spawned enemy’s health to the Rewards system
            if (rewards == null)
                rewards = GameObject.FindWithTag("RewardsM").GetComponent<Rewards>();

            SimpleHealth enemyHp = enemyInstance.GetComponent<SimpleHealth>();
            if (enemyHp != null)
            {
                rewards.SetEnemy(enemyHp);
                Debug.Log("Linked new enemy to Rewards: " + enemyHp.name);
            }
            else
            {
                Debug.LogWarning("Spawned enemy has no SimpleHealth component!");
            }
        }
    }

    // Spawn a specific enemy prefab
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

    // Queue an enemy to be spawned next
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

    // Play boss music corresponding to the spawned boss
    private void PlayBossMusic(GameObject boss)
    {
        // Stop all music first
        defultMusic.Pause();
        vampBossMusic.Pause();
        sirenBossMusic.Pause();
        karnaraBossMusic.Pause();

        if (boss == vampireBoss) vampBossMusic.UnPause();
        else if (boss == sirenBoss) sirenBossMusic.UnPause();
        else if (boss == idkBoss) karnaraBossMusic.UnPause();
    }

    // Destroy the current enemy instance
    public void DestroyEnemy()
    {
        if (spawnedList.Contains(enemyInstance))
        {
            spawnedList.Remove(enemyInstance);
            Destroy(enemyInstance);
        }
    }

    // Destroy the boss and stop boss music
    public void DestroyBoss()
    {
        if (bossInstance != null)
        {
            Destroy(bossInstance);
            bossInstance = null;
            ifBossExists = false;

            // Stop all boss music
            vampBossMusic.Pause();
            sirenBossMusic.Pause();
            karnaraBossMusic.Pause();

            // Resume default music
            defultMusic.UnPause();
        }
    }

    // Skip boss fight and mark battle as won
    public void skipBossFight()
    {
        DestroyBoss();
        battleSystem.state = BattleState.WON;
    }
}
