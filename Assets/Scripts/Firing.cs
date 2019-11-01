using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public Transform flipTransform;
    public int maxBulletPoolSize;
    public Transform projectileStart;
    public GameObject bulletPrefab;
    public Transform bulletParent;
    public float projectileSpeed;
    public float fireCooldown;
    public float bulletDamage;
    public float bulletLifeTime;
    
    GameObject[] bullets;
    float remainingCooldown;
    

    // Start is called before the first frame update
    void Start()
    {
        bullets = new GameObject[maxBulletPoolSize];
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingCooldown > 0) {
            remainingCooldown -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        if (remainingCooldown > 0)
            return;
        
        Debug.Log("Firing");
        remainingCooldown = fireCooldown;

        bool spawned = false;
        int emptyIndex = -1;
        GameObject firedBullet = null;
        for (int i = 0; i < maxBulletPoolSize; i++)
        {
            if (bullets[i] == null) {
                if (emptyIndex == -1)
                    emptyIndex = i;
            } else if (!bullets[i].activeSelf) {
                firedBullet = bullets[i];
                bullets[i].transform.position = projectileStart.position;
                bullets[i].transform.rotation = transform.rotation;
                spawned = true;
            }
        }

        if (!spawned) {
            if (emptyIndex == -1) {
                Debug.Log("Warning: Bullet pool exhausted!");
            } else {
                bullets[emptyIndex] = Instantiate(bulletPrefab, projectileStart.position, transform.rotation, bulletParent);
                firedBullet = bullets[emptyIndex];
            }
        }

        if (firedBullet != null) {
            Bullet bullet = firedBullet.GetComponent<Bullet>();
            bullet.damage = bulletDamage;
            bullet.lifeTime = bulletLifeTime;
            bullet.Shoot();
            Vector2 force = transform.right * flipTransform.localScale.x * 2 * projectileSpeed; // local scale is [-0.5, 0.5], so multiply by 2
            firedBullet.GetComponent<Rigidbody2D>().AddForce(force);
        } else {
            Debug.LogError("No fired bullet!");
        }
    }
}
