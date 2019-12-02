using System;
using UnityEngine;

public class Pooling : MonoBehaviour {
    public Pool CreatePool(int maxObjects, GameObject prefab, string groupName)
    {
        GameObject go = new GameObject(groupName);
        go.transform.parent = transform;

        Pool pool = go.AddComponent<Pool>();
        pool.Initialize(maxObjects, prefab);

        return pool;
    }
}