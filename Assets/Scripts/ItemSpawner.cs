using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    private MoveRoomTest moveRoomTest;
    [SerializeField] private int item;
    [SerializeField] private int poolSize;
    public List<GameObject> itemPrefabs;

    public GameObject[] pooooooooop;
    private int randomItem;

    //Variables that set the range for spawning
    private float minSpawnX1 = 1.6f;
    private float maxSpawnX1 = 5f;
    private float minSpawnX2 = -1.6f;
    private float maxSpawnX2 = -5f;
    private float minSpawnZ = -5f;
    private float maxSpawnZ = 7.90f;
    private float spawnRadius = 2f;
    private float rangeOption;

    BattleSystem battleSystem;

    void Start()
    {
        moveRoomTest = GameObject.FindWithTag("MoveRoomTesting").GetComponent<MoveRoomTest>();
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        /* for (int i = 0; i < itemPrefabs.Count; i++)
         {
             // Debug.Log("test2: " + i);
             EnemyObjectPool.Instance.CreatePool(item, itemPrefabs[i], poolSize);
          }*/

        SpawnItem(3);
    }

    void Update()
    {
        if (battleSystem.moveC == true)
        {
            Debug.Log("test:destroy ");
            foreach (GameObject obj in pooooooooop)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }

            }

            StartCoroutine(DelayTrash());
        }
    }

    IEnumerator DelayTrash()
    {
        yield return new WaitForSeconds(2.5f);
        SpawnItem(3);
    }
    public void SpawnItem(int count)
    {
        // List<int> availableItems = new List<int>(itemPrefabs.Count);

        for (int i = 0; i < 3; i++)
        {
            randomItem = Random.Range(0, itemPrefabs.Count);
            rangeOption = Random.Range(0, 2);

            float randomX = Random.Range(minSpawnX1, maxSpawnX1);
            float randomX2 = Random.Range(minSpawnX2, maxSpawnX2);
            float randomZ = Random.Range(minSpawnZ, maxSpawnZ);

            if (rangeOption == 0)
            {
                Vector3 randomSpawn = new Vector3(randomX, 0.75f, randomZ);
                Collider[] hitColliders = Physics.OverlapSphere(randomSpawn, spawnRadius);
                if (hitColliders.Length != 0)
                {
                    Instantiate(itemPrefabs[randomItem], randomSpawn, Quaternion.identity);
                }
            }
            else
            {
                Vector3 randomSpawn = new Vector3(randomX2, 0.75f, randomZ);
                Collider[] hitColliders = Physics.OverlapSphere(randomSpawn, spawnRadius);
                if (hitColliders.Length != 0)
                {
                    Instantiate(itemPrefabs[randomItem], randomSpawn, Quaternion.identity);
                }

            }

            pooooooooop = GameObject.FindGameObjectsWithTag("Trash");


            //Debug.Log("test: " + i);
            //randomItem = availableItems[Random.Range(0, itemPrefabs.Count)];
            // GameObject spawnItem = EnemyObjectPool.Instance.GetPooledObject(randomItem);

            /*  if (spawnItem != null)
                 {
                     spawnItem.transform.position = selectedSpawn.position;  
                 } */
        }
    }
}


