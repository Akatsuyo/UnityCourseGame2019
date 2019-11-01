using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public Vector2 from;
    public Vector2 to;
    public float moveSpeed;
    public float maxHealth;
    public GameObject healthBarSpawner;
    public GameObject coinPrefab;
    public int coins;
    public float coinForce;

    bool headingTo = true;
    bool facingRight = false;

    float currHealth;

    new Rigidbody2D rigidbody;
    Animator animator;
    GameObject healthBar;
    ParticleSystem bloodParticles;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        healthBar = healthBarSpawner.GetComponent<HealthBarSpawner>().CreateHealthBar(gameObject, new Vector3(0, 1, 0));
        currHealth = maxHealth;

        bloodParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth < maxHealth && !healthBar.activeSelf)
            healthBar.SetActive(true);
        
        if (currHealth <= 0) {
            for (int i = 0; i < coins; i++)
            {
                Vector2 dir = new Vector2(Random.Range(-0.4f, 0.4f), 1);
                Debug.Log(dir);
                GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().AddForce(dir.normalized * coinForce, ForceMode2D.Impulse);
            }

            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 diff;
        if (headingTo && Vector2.Distance(to, transform.position) < 0.1f)
            headingTo = false;

        if (!headingTo && Vector2.Distance(from, transform.position) < 0.1f)
            headingTo = true;

        if (headingTo)
            diff = to - (Vector2)transform.position;
        else
            diff = from - (Vector2)transform.position;

        rigidbody.velocity = diff.normalized * moveSpeed * Time.fixedDeltaTime;



        if (!facingRight && rigidbody.velocity.x > 0) {
            flip();
        } else if (facingRight && rigidbody.velocity.x < 0) {
            flip();
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        animator.SetBool("FacingRight", facingRight);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.transform.parent != null && 
            other.gameObject.transform.parent.name == "Bullets")
        {
            float damage = other.gameObject.GetComponent<Bullet>().damage;
            currHealth -= damage;

            bloodParticles.Emit(20);

            healthBar.GetComponent<HealthBar>().SetHealth(currHealth / maxHealth);
        }
    }

    private void OnDestroy() {
        Destroy(healthBar);
    }
}
