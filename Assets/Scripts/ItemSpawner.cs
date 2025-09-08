using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private int item;
    [SerializeField] private int poolSize;
    public List<GameObject> itemPrefabs;
    private int randomItem;

    //Variables that set the range for spawning
    private float minSpawnX = -4f;
    private float maxSpawnX = 4f;
    private float minSpawnZ = -7f;
    private float maxSpawnZ = 7f;

    void Start()
    {
        /* for (int i = 0; i < itemPrefabs.Count; i++)
         {
             // Debug.Log("test2: " + i);
             EnemyObjectPool.Instance.CreatePool(item, itemPrefabs[i], poolSize);
          }*/

        SpawnItem(3);
    }
    public void SpawnItem(int count)
    {
       // List<int> availableItems = new List<int>(itemPrefabs.Count);

        for (int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(minSpawnX, maxSpawnX);
            float randomZ = Random.Range(minSpawnZ, maxSpawnZ);
            //Debug.Log("test: " + i);
            //randomItem = availableItems[Random.Range(0, itemPrefabs.Count)];
            randomItem = Random.Range(0, itemPrefabs.Count);
            Vector3 randomSpawn = new Vector3(randomX, 0.744f, randomZ);
           // GameObject spawnItem = EnemyObjectPool.Instance.GetPooledObject(randomItem);
            Instantiate(itemPrefabs[randomItem], randomSpawn, Quaternion.identity);
            
          /*  if (spawnItem != null)
               {
                   spawnItem.transform.position = selectedSpawn.position;  
               } */
        }
    }
}
