using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float moveSmoothing;
    public float jumpForce;
    public float doubleJumpMultiplier;
    public float weaponRotationSpeed;

    public bool grounded;
    public bool doubleJumped;

    public LayerMask groundLayer;
    public Transform groundTopLeft;
    public Transform groundBottomRight;

    public Transform weapon;
    public Transform weaponProjectileStart;
    public bool hasWeapon;

    new Rigidbody2D rigidbody;
    new BoxCollider2D collider;
    Animator animator;
    bool facingRight = true;
    bool jumped;
    bool knockbacked;
    float horizontalMovement;
    Vector3 currVel = Vector3.zero;

    Transform upperRightArm;
    Transform lowerRightArm;
    Firing firing;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.freezeRotation = true;

        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        Transform torso = transform.Find("Torso");
        upperRightArm = torso.Find("UR_Arm");
        lowerRightArm = upperRightArm.Find("LR_Arm");

        health = GetComponent<Health>();

        // Temp testing
        hasWeapon = true;
        firing = weapon.GetComponent<Firing>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();

        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            jumped = true;
        }
        
        if (hasWeapon) {
            AimWeapon();
        } else {
            if (horizontalMovement > 0 && !facingRight || horizontalMovement < 0 && facingRight)
                Flip();
        }

        if (Input.GetMouseButton(0)) {
            firing.Fire();
        }

        animator.SetBool("HasWeapon", weaponProjectileStart != null);
        animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));
    }

    private void FixedUpdate()
    {
        if (!CheckKnockback()) {
            Move();
        }
        
        CheckJump();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        bool isEnemy = other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
        if (isEnemy) {
            Vector2 diff = other.gameObject.transform.position - transform.position;
            rigidbody.velocity = new Vector2(Utils.GetDirection(diff.x) * -1 * enemy.knockbackForce, 10);
            knockbacked = true;
        }
    }

    public void Respawn(Vector2 location)
    {
        jumped = false;
        knockbacked = false;
        currVel = Vector3.zero;
        health.HealFull();
        rigidbody.velocity = Vector2.zero;
        transform.position = location;
    }

    bool CheckKnockback() {
        if (!knockbacked)
            return false;

        rigidbody.velocity = rigidbody.velocity / (1 + Time.fixedDeltaTime);
        
        if (Mathf.Abs(rigidbody.velocity.x) < 3) {
            knockbacked = false;
            return false;
        }

        return true;
    }

    void CheckHealth()
    {
        if (!health.HasHealth())
            Utils.GetGameController().ResetLevel();
    }

    void AimWeapon()
    {
        upperRightArm.rotation = Quaternion.Euler(0, 0, 30);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vectorToTarget = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) > 100 && facingRight || Mathf.Abs(angle) < 100 && !facingRight)
            Flip();

        lowerRightArm.rotation = Quaternion.RotateTowards(lowerRightArm.rotation, Quaternion.AngleAxis(angle + 90, Vector3.forward), 360);
    }

    void Move()
    {
        Vector2 targetVel = new Vector2(horizontalMovement * moveSpeed * Time.fixedDeltaTime * 100, rigidbody.velocity.y);
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVel, ref currVel, moveSmoothing);
    }

    void CheckJump()
    {
        grounded = Physics2D.OverlapArea(groundTopLeft.position, groundBottomRight.position, groundLayer);
        if (grounded) {
            doubleJumped = false;
        }

        if (jumped) {
            if (grounded) {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            } else if (!doubleJumped) {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                rigidbody.AddForce(Vector2.up * jumpForce * doubleJumpMultiplier, ForceMode2D.Impulse);
                doubleJumped = true;
            }
            jumped = false;
        }
    }

    void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }
}
