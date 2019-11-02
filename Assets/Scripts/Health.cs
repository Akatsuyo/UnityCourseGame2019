using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;

    float currHealth;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InflictDamage(float damage)
    {
        if (currHealth > 0) {
            currHealth -= damage;
        }
    }

    public void Heal(float amount)
    {
        if (currHealth + amount > maxHealth) {
            currHealth = maxHealth;
        } else {
            currHealth += amount;
        }
    }

    public float GetPercent()
    {
        return currHealth / maxHealth;
    }

    public bool HasHealth() 
    {
        return currHealth > 0;
    }

    public void HealFull()
    {
        currHealth = maxHealth;
    }
}
