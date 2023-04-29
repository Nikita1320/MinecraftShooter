using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime;
    public Action<GameObject> collidedEvent;
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected LayerMask layer;
    [SerializeField] protected LayerMask collisionLayer;
    [SerializeField] protected float damage;
    protected bool isInit = false;
    protected float remainingTimeLife;
    private void Update()
    {
        if (isInit)
        {
            transform.position += direction * speed;
            remainingTimeLife -= Time.deltaTime;
            if (remainingTimeLife <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public virtual void Init(Vector3 direction, LayerMask damagableLayer, float damage)
    {
        this.direction = direction - transform.position;
        this.damage = damage;
        layer = damagableLayer;
        transform.LookAt(direction);
        remainingTimeLife = lifeTime;
        isInit = true;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (collisionLayer == (collisionLayer | (1 << other.gameObject.layer)))
        {
            if (layer == (layer | (1 << other.gameObject.layer)))
            {
                other.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
