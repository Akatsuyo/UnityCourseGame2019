using UnityEngine;

public class TowerEnemy : Enemy {

    public Transform target;

    Transform aim;
    Transform bulletStart;

    public int range;

    public GameObject bulletPrefab;
    public float firingSpeed;
    public float bulletLifeTime;
    public float bulletSpeed;
    public AudioClip shootAudio;

    float fireTimer = 0;
    Pool bulletPool;

    new AudioSource audio;

    protected override void OnStart() {
        aim = transform.Find("Aim");
        bulletStart = aim.Find("BulletStart");
        
        bulletPool = Global.Pooling.CreatePool(20, bulletPrefab, "TowerBullets");

        audio = GetComponent<AudioSource>();
    }

    protected override void OnUpdate() {
        float dist = Vector2.Distance(target.position, transform.position);
        if (dist < range && IsFacingTarget()) {
            Vector2 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

            float clampedAngle = angle;
            float correctedAngle = angle;
            if (transform.localScale.x > 0) {
                clampedAngle = Mathf.Sign(angle) * Utils.Clamp(Mathf.Abs(angle), 120, 180);
                correctedAngle = clampedAngle - 180;
            } else {
                clampedAngle = Mathf.Sign(angle) * Utils.Clamp(Mathf.Abs(angle), 0, 60);
                correctedAngle = clampedAngle;
            }
            
            aim.rotation = Quaternion.RotateTowards(aim.rotation, Quaternion.AngleAxis(correctedAngle, Vector3.forward), 360);

            fireTimer += Time.deltaTime;
            if (fireTimer > 10 / firingSpeed) {
                fireTimer = 0;
                if (clampedAngle == angle)
                    FireBullet();
            }
        }
    }

    void FireBullet() 
    {
        GameObject bulletGo = bulletPool.GetObject();
        if (bulletGo != null) {
            bulletGo.transform.position = bulletStart.position;
            bulletGo.transform.rotation = aim.rotation;
            bulletGo.transform.localScale = new Vector3(2, 2, 2);

            Bullet bullet = bulletGo.GetComponent<Bullet>();
            bullet.Shoot(damage, bulletLifeTime, transform.localScale.x > 0 ? -aim.right : aim.right, bulletSpeed);
            audio.PlayOneShot(shootAudio, 0.5f);
        }
    }

    bool IsFacingTarget() {
        if (transform.localScale.x > 0) {
            return target.position.x < transform.position.x;
        } else {
            return target.position.x > transform.position.x;
        }
    }
}