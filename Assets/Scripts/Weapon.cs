using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform flipTransform;
    public Transform projectileStart;
    public GameObject bulletPrefab;

    public AudioClip shootAudio;
    public AudioClip reloadAudio;

    // Weapon variables
    public float fireCooldown;
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletLifeTime;

    public int magSize;
    public float reloadTime;

    // Pooling
    public int maxBulletPoolSize;
    Pool bulletPool;

    float fireTimer;
    float reloadTimer;
    bool reloading;
    int _loadedBullets;

    int LoadedBullets {
        get {
            return _loadedBullets;
        }
        set {
            _loadedBullets = value;

            Global.HudController.BulletDisplay.SetBullets(_loadedBullets);
        }
    }
    
    new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        LoadedBullets = magSize;
        Global.HudController.BulletDisplay.SetMagSize(magSize);

        bulletPool = Global.Pooling.CreatePool(maxBulletPoolSize, bulletPrefab, "Bullets");

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer > 0) {
            fireTimer -= Time.deltaTime;
        }

        if (reloadTimer > 0) {
            reloadTimer -= Time.deltaTime;
        } else if (reloading) {
            LoadedBullets = magSize;
            reloading = false;
        }
    }

    public void Fire()
    {
        if (fireTimer > 0)
            return;

        if (LoadedBullets == 0 || reloading) {
            // Reload
            return;
        }
        
        fireTimer = fireCooldown;
        LoadedBullets -= 1;

        GameObject bulletGo = bulletPool.GetObject();
        if (bulletGo != null) {
            bulletGo.transform.position = projectileStart.position;
            bulletGo.transform.rotation = transform.rotation;

            Bullet bullet = bulletGo.GetComponent<Bullet>();
            Vector2 direction = transform.right * flipTransform.localScale.x;

            bullet.Shoot(bulletDamage, bulletLifeTime, transform.right * flipTransform.localScale.x, bulletSpeed);
            audio.PlayOneShot(shootAudio, 0.5f);
        }
    }

    public void Reload()
    {
        if (reloading)
            return;
         
        reloadTimer = reloadTime;
        reloading = true;
        audio.PlayOneShot(reloadAudio);
    }
}
