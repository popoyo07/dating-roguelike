using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private int groundItem;
    [SerializeField] private int wallItem;
    [SerializeField] private int ceilingItem;

    [SerializeField] private int poolSize;
    public List<GameObject> groundItemPrefabs;
    public List<GameObject> wallItemPrefabs;
    public List<GameObject> ceilingItemPrefabs;

    public GameObject torchLeftPrefab;
    public GameObject torchRightPrefab;

    public GameObject[] pooooooooop;

    private int groundRandomItem;
    private int wallRandomItem;
    private int ceilingRandomItem;

    public Vector3[] wallSpawnPoints;
    public Vector3[] ceilingSpawnPoints;

    // Ground spawn ranges
    private float minSpawnX1 = 1.6f;
    private float maxSpawnX1 = 4.50f;
    private float minSpawnX2 = -1.6f;
    private float maxSpawnX2 = -4.50f;
    private float minSpawnZ = -5f;
    private float maxSpawnZ = -1f;
    private float minSpawnZ2 = 0.5f;
    private float maxSpawnZ2 = 4f;

    private float rangeOption;

    private bool canSpawnArea0;
    private bool canSpawnArea1;
    private bool canSpawnArea2;
    private bool canSpawnArea3;

    BattleSystem battleSystem;
    bool itemSpawn;

    void Start()
    {
        // Hook BattleSystem reference
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        // Make sure itemSpawn starts false
        itemSpawn = false;

        // Spawn initial items
        SpawnItem(3);
    }

    void Update()
    {
        // Reset spawn flag when a new battle starts
        if (battleSystem.state != BattleState.WON && itemSpawn == true)
        {
            itemSpawn = false;
        }

        if (battleSystem.state == BattleState.REWARD) 
        {
            pooooooooop = GameObject.FindGameObjectsWithTag("Trash");
            foreach (GameObject obj in pooooooooop)
            {
                if (obj != null)
                    Destroy(obj);
            }
        }
        // When battle is won, destroy old items and spawn new ones
        if (battleSystem.state == BattleState.WON)
        {
         
            if (!itemSpawn)
            {
                StartCoroutine(DelayTrash());
                itemSpawn = true;
            }
        }
    }

    IEnumerator DelayTrash()
    {
        // Wait a bit so everything is destroyed before spawning
        Debug.LogWarning("Waiting to run spawn item");
        yield return new WaitForSeconds(2.5f);
        SpawnItem(3);
    }

    public void SpawnItem(int count)
    {
        Debug.LogWarning(" run spawn item");

        // Reset area availability
        canSpawnArea0 = true;
        canSpawnArea1 = true;
        canSpawnArea2 = true;
        canSpawnArea3 = true;

        // Spawn ceiling item
        if (ceilingItemPrefabs.Count > 0 && ceilingSpawnPoints.Length > 0)
        {
            ceilingRandomItem = Random.Range(0, ceilingItemPrefabs.Count);
            int randomCeilingSpawnPointIndex = Random.Range(0, ceilingSpawnPoints.Length);
            Vector3 selectedCeilingSpawnPoint = ceilingSpawnPoints[randomCeilingSpawnPointIndex];
            Instantiate(ceilingItemPrefabs[ceilingRandomItem], selectedCeilingSpawnPoint, Quaternion.identity).tag = "Trash";
        }

        // Spawn 3 ground items in random areas
        for (int i = 0; i < 3; i++)
        {
            wallRandomItem = Random.Range(0, wallItemPrefabs.Count);
            GameObject wallObject = wallItemPrefabs[wallRandomItem];

            groundRandomItem = Random.Range(0, groundItemPrefabs.Count);
            rangeOption = Random.Range(0, 4);

            float randomX = Random.Range(minSpawnX1, maxSpawnX1);
            float randomX2 = Random.Range(minSpawnX2, maxSpawnX2);
            float randomZ = Random.Range(minSpawnZ, maxSpawnZ);
            float randomZ2 = Random.Range(minSpawnZ2, maxSpawnZ2);

            bool spawned = false;

            switch (rangeOption)
            {
                case 0:
                    if (canSpawnArea0)
                    {
                        Debug.LogWarning(" run spawn item area 0");

                        Vector3 randomSpawn = new Vector3(randomX, 0.75f, randomZ);
                        Instantiate(groundItemPrefabs[groundRandomItem], randomSpawn, Quaternion.identity).tag = "Trash";
                        canSpawnArea0 = false;
                        spawned = true;

                        // Torch back left
                        Vector3 torchBackLeftSpawn = new Vector3(3.4f, 3.25f, -7.992f);
                        Instantiate(wallObject, torchBackLeftSpawn, Quaternion.identity).tag = "Trash";
                    }
                    break;

                case 1:
                    if (canSpawnArea1)
                    {
                        Debug.LogWarning(" run spawn item area 1");
                        Vector3 randomSpawn = new Vector3(randomX2, 0.75f, randomZ);
                        Instantiate(groundItemPrefabs[groundRandomItem], randomSpawn, Quaternion.identity).tag = "Trash";
                        canSpawnArea1 = false;
                        spawned = true;

                        // Torch left
                        Vector3 torchLeftSpawn = new Vector3(4.98f, 5.14f, 1.64f);
                        Instantiate(torchLeftPrefab, torchLeftSpawn, Quaternion.identity).tag = "Trash";
                    }
                    break;

                case 2:
                    if (canSpawnArea2)
                    {
                        Debug.LogWarning(" run spawn item area 2");
                        Vector3 randomSpawn = new Vector3(randomX, 0.75f, randomZ2);
                        Instantiate(groundItemPrefabs[groundRandomItem], randomSpawn, Quaternion.identity).tag = "Trash";
                        canSpawnArea2 = false;
                        spawned = true;

                        // Torch right
                        Vector3 torchRightSpawn = new Vector3(-4.98f, 5.14f, 1.64f);
                        Instantiate(torchRightPrefab, torchRightSpawn, Quaternion.identity).tag = "Trash";
                    }
                    break;

                case 3:
                    if (canSpawnArea3)
                    {
                        Debug.LogWarning(" run spawn item area 3");
                        Vector3 randomSpawn = new Vector3(randomX2, 0.75f, randomZ2);
                        Instantiate(groundItemPrefabs[groundRandomItem], randomSpawn, Quaternion.identity).tag = "Trash";
                        canSpawnArea3 = false;
                        spawned = true;

                        // Torch back right
                        Vector3 torchBackRightSpawn = new Vector3(-3.4f, 3.25f, -7.992f);
                        Instantiate(wallObject, torchBackRightSpawn, Quaternion.identity).tag = "Trash";
                    }
                    break;
            }

            if (!spawned)
                i--; // retry if area already used
        }

        pooooooooop = GameObject.FindGameObjectsWithTag("Trash");
    }
}
