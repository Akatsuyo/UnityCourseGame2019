using UnityEngine;

public static class Utils
{
    public static float Clamp(float value, float min, float max) 
    {
        if (value < min) {
            return min;
        } else if (value > max) {
            return max;
        } else {
            return value;
        }
    }

    public static int GetDirection(float axis) {
        if (axis > 0) {
            return 1;
        } else if (axis < 0) {
            return -1;
        } else {
            return 0;
        }
    }

    public static bool TryInflictDamage(GameObject other, float damage)
    {
        if (other.name == "Trigger")
            other = other.transform.parent.gameObject;
        
        bool hasHealth = other.TryGetComponent<Health>(out Health health);
        if (hasHealth) {
            health.InflictDamage(damage);
        }
        return hasHealth;
    }
}