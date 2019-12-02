using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float remainingLifeTime;

    new Rigidbody2D rigidbody;
    Vector2 force;
    bool shoot;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingLifeTime > 0) {
            remainingLifeTime -= Time.deltaTime;
        } else {
            gameObject.SetActive(false); // Pooling
        }

        if (rigidbody.velocity != Vector2.zero) {
            transform.rotation.SetLookRotation(rigidbody.velocity);
        }
    }

    void FixedUpdate() {
        if (shoot) {
            rigidbody.AddForce(force);
            shoot = false;
        }
    }

    public void Shoot(float damage, float lifeTime, Vector2 direction, float speed)
    {
        this.damage = damage;
        remainingLifeTime = lifeTime;
        gameObject.SetActive(true);
        shoot = true;
        force = direction.normalized * speed * 100;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        gameObject.SetActive(false); // Pooling
        Utils.TryInflictDamage(other.gameObject, damage);
    }
}
