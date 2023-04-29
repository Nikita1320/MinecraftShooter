using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Health : MonoBehaviour
{
    public delegate void TakedDamage();
    public delegate void ChangedHP();
    public delegate void Died();

    public TakedDamage takedDamage;
    public ChangedHP changedHP;
    public Died diedEvent;

    [SerializeField] protected float healthPoint;
    [SerializeField] protected float maxHealthPoint;
    public float MaxHP => maxHealthPoint;
    public float HP => healthPoint;

    private void Awake()
    {
        healthPoint = maxHealthPoint;
    }
    public abstract void Die();
    public abstract void TakeDamage(float damage);
    public abstract void TakeDamage(float damage, Vector3 hitPosition);
    public void Reset()
    {
        healthPoint = maxHealthPoint;
    }
}
