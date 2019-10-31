using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;

    float remainingLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingLifeTime < 0) {
            gameObject.SetActive(false);
            remainingLifeTime = lifeTime;
        } else {
            remainingLifeTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        gameObject.SetActive(false);
        remainingLifeTime = lifeTime;
    }
}
