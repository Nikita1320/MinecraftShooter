using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRiffleWeapon : Weapon
{
    public override bool Shoot()
    {
        if (remainingTimeToReadyShoot <= 0 && remainingBulletInClip > 0)
        {
            remainingTimeToReadyShoot = 1 - (float)weaponData.GetRateOfFireValue(level) / 1000;
            shootParticle.Play();

            Vector3 directionBullet = new Vector3(Camera.main.transform.forward.x + Random.Range(-scatter, scatter),
                   Camera.main.transform.forward.y + Random.Range(-scatter, scatter),
                   Camera.main.transform.forward.z);

            Ray ray = new Ray(Camera.main.transform.position, directionBullet);

            RaycastHit[] hits =  Physics.RaycastAll(ray, 100f, bulletCollisionLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                var hitObject = hits[0].collider.gameObject;
                var particle = Instantiate(hitParticle, null);
                particle.transform.position = hits[0].point;

                if (damagableLayer == (damagableLayer | (1 << hitObject.gameObject.layer)))
                {
                    hitObject.GetComponent<Health>().TakeDamage(weaponData.GetDamageValue(level), hits[0].point);
                }
            }
            remainingBulletInClip--;
            return true;
        }
        return false;
    }
}
