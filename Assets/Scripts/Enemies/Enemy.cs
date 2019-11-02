using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health))]
public class Enemy : MonoBehaviour
{
    public bool moving;
    public Vector2 from;
    public Vector2 to;
    public float moveSpeed;

    public Vector2 healthBarDelta;
    
    public int coins;
    public float coinForce;

    public float damage;
    public float knockbackForce;

    public GameObject coinPrefab;
    public GameObject healthBarPrefab;

    bool headingTo = true;
    bool facingRight = false;

    new Rigidbody2D rigidbody;

    Animator animator = null;
    bool animated = false;

    GameObject healthBarGo;
    HealthBar healthBar;
    Health health;
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        animated = TryGetComponent(typeof(Animator), out Component animatorComponent);
        if (animated)
            animator = (Animator)animatorComponent;
        
        healthBarGo = Factory.CreateHealthBar(transform, healthBarPrefab, healthBarDelta);
        healthBar = healthBarGo.GetComponent<HealthBar>();
        health = GetComponent<Health>();

        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    void FixedUpdate()
    {
        if (moving) {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Utils.TryInflictDamage(other.gameObject, damage);
    }

    public void Respawn() {
        Debug.Log("Respawning");
        transform.position = initialPos;
        health.HealFull();
        healthBar.UpdateBar(health.GetPercent());
        gameObject.SetActive(true);
    }

    void Move()
    {
        Vector2 diff;
        if (headingTo && Vector2.Distance(to, transform.position) < 0.1f ||
            !headingTo && Vector2.Distance(from, transform.position) < 0.1f) {
            headingTo = !headingTo;
        }

        if (headingTo) {
            diff = to - (Vector2)transform.position;
        } else {
            diff = from - (Vector2)transform.position;
        }

        rigidbody.velocity = diff.normalized * moveSpeed * Time.fixedDeltaTime * 10;

        FlipWithVelocity();
    }

    void OnDeath()
    {
        SpawnCoins();
        gameObject.SetActive(false);
        healthBarGo.SetActive(false);
    }
    
    void CheckHealth()
    {
        healthBar.UpdateBar(health.GetPercent());
        
        if (!health.HasHealth()) {
            OnDeath();
        }
    }

    void SpawnCoins()
    {
        for (int i = 0; i < coins; i++)
        {
            Vector2 dir = new Vector2(Random.Range(0, 100) * 0.006f - 0.3f, Random.Range(0.8f, 1));
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(dir.normalized * coinForce, ForceMode2D.Impulse);
        }
    }

    void FlipWithVelocity()
    {
        if (!facingRight && rigidbody.velocity.x > 0 ||
            facingRight && rigidbody.velocity.x < 0) {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        animator?.SetBool("FacingRight", facingRight);
    }
}
