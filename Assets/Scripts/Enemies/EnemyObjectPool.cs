using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool Instance { get; private set; }
    private Dictionary<int, Queue<GameObject>> objectPools = new Dictionary<int, Queue<GameObject>>();

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
    public void CreatePool(int enemy, GameObject enemyPrefabs, int poolSize)
    {
        if (!objectPools.ContainsKey(enemy))
        {
            Queue<GameObject> newPool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(enemyPrefabs, transform);
                obj.SetActive(false);
                newPool.Enqueue(obj);
            }
            objectPools.Add(enemy, newPool);
        }

    }

    public GameObject GetPooledObject(int enemy)
    {
        if (objectPools.ContainsKey(enemy) && objectPools[enemy].Count > 0)
        {
            GameObject obj = objectPools[enemy].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return null;
    }

    public void ReturnPooledObjects(int enemy, GameObject obj)
    {
        if (objectPools.ContainsKey(enemy))
        {
            obj.SetActive(false);
            objectPools[enemy].Enqueue(obj);

        }
        else
        {
            Destroy(obj);
        }
    }
}
