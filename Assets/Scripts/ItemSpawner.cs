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

    public GameObject[] pooooooooop;

    private int groundRandomItem;
    private int wallRandomItem;
    private int ceilingRandomItem;

    public Transform[] wallSpawnPoints;
    public Transform[] ceilingSpawnPoints;

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

        List<Transform> availableCeilingSpawnPoints = new List<Transform>(ceilingSpawnPoints);

        ceilingRandomItem = Random.Range(0, ceilingItemPrefabs.Count);

        int randomCeilingSpawnPointIndex = Random.Range(0, availableCeilingSpawnPoints.Count);
        Transform selectedCeilingSpawnPoint = availableCeilingSpawnPoints[randomCeilingSpawnPointIndex];

        Instantiate(ceilingItemPrefabs[ceilingRandomItem], selectedCeilingSpawnPoint.position, selectedCeilingSpawnPoint.rotation);
        availableCeilingSpawnPoints.RemoveAt(randomCeilingSpawnPointIndex);

        List<Transform> availableWallSpawnPoints = new List<Transform>(wallSpawnPoints);

        //For loop that spawns random itmes in random areas. The i-- makes sure the loop will run until 3 items have been spawned in 3 different areas

        for (int i = 0; i < 3; i++)
        {
            GameObject wallObject = wallItemPrefabs[wallRandomItem];

            int randomWallSpawnPointIndex = Random.Range(0, availableWallSpawnPoints.Count);
            Transform selectedWallSpawnPoint = availableWallSpawnPoints[randomWallSpawnPointIndex];

            groundRandomItem = Random.Range(0, groundItemPrefabs.Count); //Chooses a random item to spawn from the itemPrefabs
            rangeOption = Random.Range(0, 4); //Chooses a random spawn area

            float randomX = Random.Range(minSpawnX1, maxSpawnX1);
            float randomX2 = Random.Range(minSpawnX2, maxSpawnX2);

            float randomZ = Random.Range(minSpawnZ, maxSpawnZ);
            float randomZ2 = Random.Range(minSpawnZ2, maxSpawnZ2);

            if (rangeOption == 0)
            {
                if (canSpawnArea0 == true)
                {
                    Debug.Log("range 0");
                    Vector3 randomSpawn = new Vector3(randomX, 0.75f, randomZ);
                    Instantiate(groundItemPrefabs[groundRandomItem], randomSpawn, Quaternion.identity);
                    canSpawnArea0 = false;

                    Instantiate(wallObject, selectedWallSpawnPoint.position, selectedWallSpawnPoint.rotation);
                    availableWallSpawnPoints.RemoveAt(randomWallSpawnPointIndex);
                }
                else
                {
                    i--;
                }
            }
            if (rangeOption == 1)
            {
                if (canSpawnArea1 == true)
                {
                    Debug.Log("range 1");
                    Vector3 randomSpawn = new Vector3(randomX2, 0.75f, randomZ);
                    Instantiate(groundItemPrefabs[groundRandomItem], randomSpawn, Quaternion.identity);
                    canSpawnArea1 = false;

                    Instantiate(wallObject, selectedWallSpawnPoint.position, selectedWallSpawnPoint.rotation);
                    availableWallSpawnPoints.RemoveAt(randomWallSpawnPointIndex);
                }
                else
                {
                    i--;
                }
            }
            if (rangeOption == 2)
            {
                if (canSpawnArea2 == true)
                {
                    Debug.Log("range 2");
                    Vector3 randomSpawn = new Vector3(randomX, 0.75f, randomZ2);
                    Instantiate(groundItemPrefabs[groundRandomItem], randomSpawn, Quaternion.identity);
                    canSpawnArea2 = false;

                    Instantiate(wallObject, selectedWallSpawnPoint.position, selectedWallSpawnPoint.rotation);
                    availableWallSpawnPoints.RemoveAt(randomWallSpawnPointIndex);
                }
                else
                {
                    i--;
                }
            }
            if (rangeOption == 3)
            {
                if (canSpawnArea3 == true)
                {
                    Debug.Log("range 3");
                    Vector3 randomSpawn = new Vector3(randomX2, 0.75f, randomZ2);
                    Instantiate(groundItemPrefabs[groundRandomItem], randomSpawn, Quaternion.identity);
                    canSpawnArea3 = false;

                    Instantiate(wallObject, selectedWallSpawnPoint.position, selectedWallSpawnPoint.rotation);
                    availableWallSpawnPoints.RemoveAt(randomWallSpawnPointIndex);
                }
                else
                {
                    i--;
                }
            }

            pooooooooop = GameObject.FindGameObjectsWithTag("Trash");
        }
    }
}


