using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Factory
{
    public static GameObject CreateFollowHealthBar(Transform toFollow, GameObject prefab, Vector3 delta)
    {
        GameObject healthBarGo = MonoBehaviour.Instantiate(prefab, toFollow.position, Quaternion.identity);
        FollowHealthBar script = healthBarGo.GetComponent<FollowHealthBar>();
        script.toFollow = toFollow;
        script.delta = delta;
        return healthBarGo;
    }
}
