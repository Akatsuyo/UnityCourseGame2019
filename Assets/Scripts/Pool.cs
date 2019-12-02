using System;
using UnityEngine;

public class Pool : MonoBehaviour {

    GameObject prefab;

    int maxObjects;
    GameObject[] pool;

    public void Initialize(int maxObjects, GameObject prefab)
    {
        this.maxObjects = maxObjects;
        pool = new GameObject[maxObjects];
        this.prefab = prefab;
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < maxObjects; i++)
        {
            if (pool[i] == null) {
                GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
                pool[i] = obj;
                return obj;
            } else if (!pool[i].activeSelf) {
                return pool[i];
            }
        }

        Debug.Log("Pool exhausted!");
        return null;
    }
}