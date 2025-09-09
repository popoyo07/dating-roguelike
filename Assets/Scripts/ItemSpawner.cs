using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public MoveRoomA moveRoomA;
    public MoveRoomB moveRoomB;
    [SerializeField] private int item;
    [SerializeField] private int poolSize;
    public List<GameObject> itemPrefabs;
    private int randomItem;
   // private GameObject test;

    //Variables that set the range for spawning
    private float minSpawnX1 = 1.6f;
    private float maxSpawnX1 = 5f;
    private float minSpawnX2 = -1.6f;
    private float maxSpawnX2 = -5f;
    private float minSpawnZ = -5f;
    private float maxSpawnZ = 7.90f;
    private float spawnRadius = 2f;
    private float rangeOption;
    void Start()
    {
        /* for (int i = 0; i < itemPrefabs.Count; i++)
         {
             // Debug.Log("test2: " + i);
             EnemyObjectPool.Instance.CreatePool(item, itemPrefabs[i], poolSize);
          }*/

        SpawnItem(3);
    }

     void Update()
     {
         if (moveRoomA.teleported == true)
         {
            Debug.Log("test:destroy ");
            foreach(GameObject test2 in itemPrefabs)
            {
                Destroy(test2);
            }
            itemPrefabs.Clear();
             //SpawnItem(3);
         }

        if (moveRoomB.teleported == true)
        {
            Debug.Log("test:destroy ");
            foreach (GameObject test2 in itemPrefabs)
            {
                Destroy(test2);
            }
            itemPrefabs.Clear();
            //SpawnItem(3);
        }
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
                    GameObject test = Instantiate(itemPrefabs[randomItem], randomSpawn, Quaternion.identity);
                    itemPrefabs.Add(test);
                }
            }
            else
            {
                Vector3 randomSpawn = new Vector3(randomX2, 0.75f, randomZ);
                Collider[] hitColliders = Physics.OverlapSphere(randomSpawn, spawnRadius);
                if (hitColliders.Length != 0)
                {
                    GameObject test = Instantiate(itemPrefabs[randomItem], randomSpawn, Quaternion.identity);
                    itemPrefabs.Add(test);
                }
            }


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


