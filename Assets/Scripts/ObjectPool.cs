using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool
{
    public static readonly ObjectPool Main = new ObjectPool();

    private Dictionary<string, Queue<GameObject>> _pools
        = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetObjectInstance(string poolName, Func<GameObject> factory)
    {
        Queue<GameObject> pool;
        if(_pools.TryGetValue(poolName, out pool) && pool.Any())
        {
            var obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return factory();
        }
    }

    public void PoolObject(string poolName, GameObject instance)
    {
        instance.SetActive(false);
        Queue<GameObject> pool;
        if (_pools.TryGetValue(poolName, out pool))
        {
            pool.Enqueue(instance);
        }
        else
        {
            _pools.Add(poolName, new Queue<GameObject>(new[] { instance }));
        }
    }
}
