using UnityEngine;
using System;

public class PlayerHealth : Health
{
    public Action initializedEvent;
    private bool isDead;

    public override void Die()
    {
        diedEvent?.Invoke();
    }
    public void InitializeStat(int healthPoint)
    {
        maxHealthPoint = healthPoint;
        this.healthPoint = maxHealthPoint;
        isDead = false;
        initializedEvent?.Invoke();
    }
    public override void TakeDamage(float damage)
    {
        healthPoint -= damage;
        changedHP?.Invoke();
        takedDamage?.Invoke();
        if (healthPoint <= 0 && isDead == false)
        {
            isDead = true;
            Die();
        }
    }
    public override void TakeDamage(float damage, Vector3 hitPosition)
    {
        TakeDamage(damage);
    }
}
