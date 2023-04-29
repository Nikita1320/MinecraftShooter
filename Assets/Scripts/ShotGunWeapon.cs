using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunWeapon : Weapon
{
    [SerializeField] private float numberOfBullets;
    [SerializeField] private float flightRangeBullet = 50;
    public override bool Shoot()
    {
        if (remainingTimeToReadyShoot <= 0 && remainingBulletInClip > 0)
        {
            remainingTimeToReadyShoot = 1 - (float)weaponData.GetRateOfFireValue(level) / 1000;
            shootParticle.Play();

            for (int i = 0; i < numberOfBullets; i++)
            {
                Vector3 directionBullet = new Vector3(Camera.main.transform.forward.x + Random.Range(-scatter, scatter),
                    Camera.main.transform.forward.y + Random.Range(-scatter, scatter),
                    Camera.main.transform.forward.z);
                if (Physics.Raycast(Camera.main.transform.position, directionBullet, out RaycastHit hitInfo, flightRangeBullet, bulletCollisionLayer))
                {
                    if (damagableLayer == (damagableLayer | (1 << hitInfo.collider.gameObject.layer)))
                    {
                        hitInfo.collider.GetComponent<EnemyHealth>().TakeDamage(weaponData.GetDamageValue(level), hitInfo.point);
                    }
                    var particle = Instantiate(hitParticle, null);
                    particle.transform.position = hitInfo.point;
                }
            }

            /*Collider[] detected = Physics.OverlapBox(Camera.main.transform.forward * halfSizeBoxDetection.z, halfSizeBoxDetection, Camera.main.transform.rotation, damagableLayer);

            for (int i = 0; i < detected.Length; i++)
            {
                if (Physics.Raycast(Camera.main.transform.position, detected[i].transform.position, out RaycastHit hitInfo))
                {
                    var particle = Instantiate(hitParticle, null);
                    particle.transform.position = hitInfo.point;
                    if (hitInfo.collider == detected[i])
                    {
                        detected[i].GetComponent<EnemyHealth>()
                            .TakeDamage(weaponData.GetDamageValue(level) - (weaponData.GetDamageValue(level) * hitInfo.distance / halfSizeBoxDetection.z * 2), hitInfo.point);
                    }
                }
            }*/
            remainingBulletInClip--;
            return true;

        }
        return false;
    }
}
