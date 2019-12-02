using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;

    float _currHealth;
    public float HealthValue
    {
        get { return _currHealth; }
        set 
        {
            _currHealth = value;

            UpdateHealthBar();

            if (_currHealth <= 0)
                Empty?.Invoke(this, null);
        }
    }

    IHealthBar healthBar;

    public event EventHandler Empty;

    // Start is called before the first frame update
    void Start()
    {
        HealthValue = maxHealth;
    }

    public void AttachHealthBar(IHealthBar healthBar) {
        this.healthBar = healthBar;
    }

    public void InflictDamage(float damage)
    {
        if (HealthValue > 0) {
            HealthValue -= damage;
        }
    }

    public void Heal(float amount)
    {
        if (HealthValue + amount > maxHealth) {
            HealthValue = maxHealth;
        } else {
            HealthValue += amount;
        }
    }

    public float GetPercent()
    {
        return HealthValue / maxHealth;
    }

    public bool HasHealth() 
    {
        return HealthValue > 0;
    }

    public void HealFull()
    {
        HealthValue = maxHealth;
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
            this.healthBar.UpdateBar(GetPercent());
    }
}
