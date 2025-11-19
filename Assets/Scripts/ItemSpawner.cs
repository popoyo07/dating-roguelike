using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

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

    //Variables that set the range for spawning for ground items

    //top left
    private float minSpawnX1 = 1.6f;
    private float maxSpawnX1 = 4.50f;

    //top right
    private float minSpawnX2 = -1.6f;
    private float maxSpawnX2 = -4.50f;

    //Bottom left
    private float minSpawnZ = -5f;
    private float maxSpawnZ = -1f;

    //Bottom right
    private float minSpawnZ2 = 0.5f;
    private float maxSpawnZ2 = 4f;

    private float rangeOption;

    //Bools for if an object can spawn in the area
    private bool canSpawnArea0;
    private bool canSpawnArea1;
    private bool canSpawnArea2;
    private bool canSpawnArea3;

    BattleSystem battleSystem;
    bool itemSpawn;

    void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        SpawnItem(3);
    }

    void Update()
    {
        if (battleSystem.state == BattleState.WON) //battleSystem.moveC == true
        {
            foreach (GameObject obj in pooooooooop)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }

            }

            if (!itemSpawn)
            {
                Debug.LogWarning("Should spawn");
                StartCoroutine(DelayTrash());
                itemSpawn = true;

            }
        }

        if (battleSystem.state != BattleState.WON && itemSpawn == true)
        {
            itemSpawn = false;
        }
    }

    IEnumerator DelayTrash()
    {
        yield return new WaitForSeconds(2.5f);
        SpawnItem(3);
    }
    public void SpawnItem(int count)
    {
        canSpawnArea0 = true;
        canSpawnArea1 = true;
        canSpawnArea2 = true;
        canSpawnArea3 = true;

        List<Vector3> availableCeilingSpawnPoints = new List<Vector3>(ceilingSpawnPoints);

        ceilingRandomItem = Random.Range(0, ceilingItemPrefabs.Count);

        int randomCeilingSpawnPointIndex = Random.Range(0, availableCeilingSpawnPoints.Count);
        Vector3 selectedCeilingSpawnPoint = availableCeilingSpawnPoints[randomCeilingSpawnPointIndex];

        Instantiate(ceilingItemPrefabs[ceilingRandomItem], selectedCeilingSpawnPoint, Quaternion.identity);
        availableCeilingSpawnPoints.RemoveAt(randomCeilingSpawnPointIndex);

        // List<Vector3> availableWallSpawnPoints = new List<Vector3>(wallSpawnPoints);

        //For loop that spawns random itmes in random areas. The i-- makes sure the loop will run until 3 items have been spawned in 3 different areas

        for (int i = 0; i < 3; i++)
        {
            // set wall item each iteration (IMPORTANT)
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
                        Vector3 pos = new Vector3(randomX, 0.75f, randomZ);
                        Instantiate(groundItemPrefabs[groundRandomItem], pos, Quaternion.identity);
                        canSpawnArea0 = false;
                        spawned = true;
                    }
                    break;

                case 1:
                    if (canSpawnArea1)
                    {
                        Vector3 pos = new Vector3(randomX2, 0.75f, randomZ);
                        Instantiate(groundItemPrefabs[groundRandomItem], pos, Quaternion.identity);
                        canSpawnArea1 = false;
                        spawned = true;
                    }
                    break;

                case 2:
                    if (canSpawnArea2)
                    {
                        Vector3 pos = new Vector3(randomX, 0.75f, randomZ2);
                        Instantiate(groundItemPrefabs[groundRandomItem], pos, Quaternion.identity);
                        canSpawnArea2 = false;
                        spawned = true;
                    }
                    break;

                case 3:
                    if (canSpawnArea3)
                    {
                        Vector3 pos = new Vector3(randomX2, 0.75f, randomZ2);
                        Instantiate(groundItemPrefabs[groundRandomItem], pos, Quaternion.identity);
                        canSpawnArea3 = false;
                        spawned = true;
                    }
                    break;
            }

            if (!spawned)
                i--; // retry
        }

        pooooooooop = GameObject.FindGameObjectsWithTag("Trash");
    }
}


