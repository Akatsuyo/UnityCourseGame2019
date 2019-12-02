using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHealthBar : MonoBehaviour, IHealthBar
{
    public Transform toFollow;
    public Vector3 delta;

    Transform health;

    // Start is called before the first frame update
    void Awake()
    {
        health = transform.Find("Health");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = toFollow.position + delta;
    }

    public void UpdateBar(float healthPercent)
    {
        gameObject.SetActive(healthPercent != 1);
        health.localScale = new Vector2(healthPercent, 1);
    }
}
