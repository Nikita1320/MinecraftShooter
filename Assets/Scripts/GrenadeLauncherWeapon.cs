using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherWeapon : Weapon
{
    [SerializeField] private GrenadeLauncherBullet grenadeBulletPref;
    [SerializeField] private Transform spawnBulletPoint;
    [SerializeField] private float powerShoot;
    public override bool Shoot()
    {
        if (remainingTimeToReadyShoot <= 0 && remainingBulletInClip > 0)
        {
            remainingTimeToReadyShoot = 1 - (float)weaponData.GetRateOfFireValue(level) / 1000;
            shootParticle.Play();
            var grenade = Instantiate(grenadeBulletPref, spawnBulletPoint);
            grenade.transform.localPosition = Vector3.zero;
            grenade.transform.localRotation = Quaternion.identity;
            grenade.transform.parent = null;

            Vector3 direction = Camera.main.transform.position + Camera.main.transform.forward * 15 - grenade.transform.position;
            grenade.Rigidbody.AddForce(direction * powerShoot, ForceMode.Impulse);
            grenade.Init(weaponData.GetDamageValue(level));
            grenade.Activate();
            remainingBulletInClip--;
            return true;
        }
        return false;
    }
}
