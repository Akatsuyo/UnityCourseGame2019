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
    float horizontalMovement;
    Vector3 currVel = Vector3.zero;

    Transform upperRightArm;
    Transform lowerRightArm;
    Firing firing;

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

        // Temp testing
        hasWeapon = true;
        firing = weapon.GetComponent<Firing>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            jumped = true;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vectorToTarget = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        if (hasWeapon) {
            if (Mathf.Abs(angle) > 100 && facingRight || Mathf.Abs(angle) < 100 && !facingRight)
                flip();
        } else {
            if (horizontalMovement > 0 && !facingRight || horizontalMovement < 0 && facingRight)
                flip();
        }

        if (weapon) {
            if (facingRight) {
                upperRightArm.rotation = Quaternion.Euler(0, 0, 30);
            } else {
                upperRightArm.rotation = Quaternion.Euler(0, 0, -30);
            }
        }

        lowerRightArm.rotation = Quaternion.RotateTowards(lowerRightArm.rotation, Quaternion.AngleAxis(angle + 90, Vector3.forward), 360);

        if (Input.GetMouseButton(0)) {
            firing.Fire();
        }

        animator.SetBool("HasWeapon", weaponProjectileStart != null);
    }

    private void FixedUpdate()
    {
        Vector2 vel = rigidbody.velocity;
        Vector2 targetVel = new Vector2(horizontalMovement * moveSpeed * Time.fixedDeltaTime, vel.y);
        rigidbody.velocity = Vector3.SmoothDamp(vel, targetVel, ref currVel, moveSmoothing);

        grounded = Physics2D.OverlapArea(groundTopLeft.position, groundBottomRight.position, groundLayer);
        if (grounded) {
            doubleJumped = false;
        }

        if (jumped) {
            if (grounded) {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            } else if (!doubleJumped) {
                rigidbody.AddForce(Vector2.up * jumpForce * doubleJumpMultiplier, ForceMode2D.Impulse);
                doubleJumped = true;
            }
            jumped = false;
        }
        

        animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));
    }

    void flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    void pickupWeapon() {

    }
}
