using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSpawner : MonoBehaviour
{
    public GameObject healthBarPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateHealthBar(GameObject entity, Vector3 delta)
    {
        GameObject healthBar = 
            Instantiate(healthBarPrefab, entity.transform.position, Quaternion.identity, transform);
        HealthBar script = healthBar.GetComponent<HealthBar>();
        script.parentEntity = entity;
        script.delta = delta;
        return healthBar;
    }
}
