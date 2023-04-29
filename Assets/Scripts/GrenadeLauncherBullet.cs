using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherBullet : Grenade
{
    [SerializeField] private LayerMask collisionLayer;
    public void Init(float damage)
    {
        maxDamage = damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collisionLayer == (collisionLayer | (1 << collision.gameObject.layer)))
        {
            Explosion();
        }
    }
}
