using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool Instance { get; private set; }
    private Dictionary<string, Queue<GameObject>> objectPools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreatePool(string spawn, GameObject enemyPrefabs, int poolSize)
    {
        if (!objectPools.ContainsKey(spawn))
        {
            Queue<GameObject> newPool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(enemyPrefabs, transform);
                obj.SetActive(false);
                newPool.Enqueue(obj);
            }
            objectPools.Add(spawn, newPool);
        }

    }

    public GameObject GetPooledObject(string spawn)
    {
        if (objectPools.ContainsKey(spawn) && objectPools[spawn].Count > 0)
        {
            GameObject obj = objectPools[spawn].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return null;
    }

    public void ReturnPooledObjects(string spawn, GameObject obj)
    {
        if (objectPools.ContainsKey(spawn))
        {
            obj.SetActive(false);
            objectPools[spawn].Enqueue(obj);

        }
        else
        {
            Destroy(obj);
        }
    }

}
