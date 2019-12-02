using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    // Movement
    public bool canMove;
    public Vector2 from;
    public Vector2 to;
    public float moveSpeed;

    new Rigidbody2D rigidbody;
    bool headingTo = true;
    bool facingRight = false;

    // Stats
    public float damage;
    public float knockbackForce;

    // Health
    public Vector2 healthBarDelta;
    public GameObject healthBarPrefab;

    GameObject healthBarGo;
    Health health;
    
    // Death
    public int coinDrops;
    public float coinForce;
    public GameObject coinPrefab;
    public GameObject deathParticles;

    Animator animator = null;
    bool animated = false;

    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        if (canMove) {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.freezeRotation = true;
        }

        animated = TryGetComponent(typeof(Animator), out Component animatorComponent);
        if (animated)
            animator = (Animator)animatorComponent;

        health = GetComponent<Health>();
        health.Empty += OnDeath;
        
        healthBarGo = Factory.CreateFollowHealthBar(transform, healthBarPrefab, healthBarDelta);
        health.AttachHealthBar(healthBarGo.GetComponent<FollowHealthBar>());

        initialPos = transform.position;

        OnStart();
    }

    protected virtual void OnStart() {}

    void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate() {}

    void FixedUpdate()
    {
        if (canMove) {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Utils.TryInflictDamage(other.gameObject, damage);
    }

    public void Respawn() {
        transform.position = initialPos;
        health.HealFull();
        gameObject.SetActive(true);
    }

    void OnDeath(object sender, EventArgs e)
    {
        SpawnCoins();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        healthBarGo.SetActive(false);

        if (gameObject.name.StartsWith("Slime")) {
            Global.GameController.AchievementSystem.AddProgress("Kill 5 slimes");
        }
    }

    void SpawnCoins()
    {
        for (int i = 0; i < coinDrops; i++)
        {
            Vector2 dir = new Vector2(UnityEngine.Random.Range(0, 100) * 0.006f - 0.3f, UnityEngine.Random.Range(0.8f, 1));
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(dir.normalized * coinForce, ForceMode2D.Impulse);
        }
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

        FlipByVelocity();
    }

    void FlipByVelocity()
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
