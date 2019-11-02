using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public float damage;

    float remainingLifeTime;

    new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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

        if (rigidbody.velocity != Vector2.zero) {
            transform.rotation.SetLookRotation(rigidbody.velocity);
        }
    }

    public void Shoot()
    {
        remainingLifeTime = lifeTime;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        gameObject.SetActive(false);
        remainingLifeTime = lifeTime;
        Utils.TryInflictDamage(other.gameObject, damage);
    }
}
