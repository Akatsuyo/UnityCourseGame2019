using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Factory
{
    public static GameObject CreateHealthBar(Transform parent, GameObject prefab, Vector3 delta)
    {
        GameObject healthBarGo = MonoBehaviour.Instantiate(prefab, parent.position, Quaternion.identity);
        HealthBar script = healthBarGo.GetComponent<HealthBar>();
        script.parent = parent;
        script.delta = delta;
        return healthBarGo;
    }
}
