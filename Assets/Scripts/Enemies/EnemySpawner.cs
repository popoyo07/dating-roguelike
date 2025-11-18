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
    public bool isSiren;
    public GameObject vampireBoss;    // Vampire boss prefab
    public bool isVampire;
    public GameObject idkBoss;        // Unknown boss prefab
    public bool isKinnara;
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

    [Header("Dialogue Setting")]
    public DialogueActivator dialogueActivator;
    public DialogueProgression progression;
    bool endingDialogueTriggered = false;


    void Start()
    {
        // Randomly choose an enemy list
        chosenList = Random.Range(0, 3); // Note: Random.Range(2,2) always returns 2

        switch (chosenList)
        {
            case 0:
                activeList = sirenList;
                isSiren = true;
                Debug.Log("Siren boss");
                boss = sirenBoss;
                break;

            case 1:
                activeList = idkList;
                isKinnara = true;
                Debug.Log("Idk boss");
                boss = idkBoss;
                break;

            case 2:
                activeList = vampireList;
                isVampire = true;
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
       // SpawnEnemy();
    }

    void Update()
    {

        // Handle what happens when the battle is won
        if (battleSystem.state == BattleState.WON && enemySpawn)
        {
            enemySpawn = false;
            DestroyEnemy(); // Remove current enemy
            Debug.LogWarning("spawning destroy");


        }

        if (!enemySpawn && battleSystem.state == BattleState.PLAYERTURN)
        {
            enemySpawn = true;

            Debug.LogWarning("spawning");
            DelayTrash(); // Wait and spawn next enemy or boss
            roomsSpawnBoss++; // Increment rooms visited
        }
        // Reset enemySpawn when battle is not won
        if (battleSystem.state == BattleState.WON)
        {
        }
    }

    // Returns the currently active enemy list
    public List<GameObject> GetActiveList()
    {
        return activeList;
    }

    void DelayTrash()
    {
      

        //    yield return new WaitForSeconds(0f); // Wait 2.5 seconds before spawning

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

        // Spawn boss on specific rooms
        if ((roomsSpawnBoss == 6 || roomsSpawnBoss == 12 || roomsSpawnBoss == 18) && !ifBossExists)
        {
            DestroyEnemy(); // Clear normal enemies
            ifBossExists = true;
            bossInstance = Instantiate(boss, bossSpawn, Quaternion.identity); // Spawn boss
            dialogueActivator = bossInstance.GetComponent<DialogueActivator>();

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
        defultMusic.Stop();
        vampBossMusic.Stop();
        sirenBossMusic.Stop();
        karnaraBossMusic.Stop();

        if (boss == vampireBoss) vampBossMusic.Play();
        else if (boss == sirenBoss) sirenBossMusic.Play();
        else if (boss == idkBoss) karnaraBossMusic.Play();
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
    public void DestroyBoss(bool resumeDefault = true)
    {
        if (bossInstance != null)
        {
            Destroy(bossInstance);
            bossInstance = null;
            ifBossExists = false;

            // Stop all boss music
            vampBossMusic.Stop();
            sirenBossMusic.Stop();
            karnaraBossMusic.Stop();

            // Resume default music only if desired
            if (resumeDefault)
            {
                defultMusic.Play();
            }
        }
    }

    // Skip boss fight and mark battle as won
    public void skipBossFight()
    {
        DestroyBoss();
        battleSystem.state = BattleState.WON;
    }
}
