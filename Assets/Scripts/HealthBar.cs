using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject parentEntity;
    public Vector3 delta;

    Transform health;

    // Start is called before the first frame update
    void Start()
    {
        health = transform.Find("Health");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parentEntity.transform.position + delta;
    }

    public void SetHealth(float healthPercent)
    {
        if (!gameObject.activeSelf && healthPercent < 1) {
            gameObject.SetActive(true);
        }
        health.localScale = new Vector2(healthPercent, 1);
    }
}
