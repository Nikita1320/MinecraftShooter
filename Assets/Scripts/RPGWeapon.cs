using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGWeapon : Weapon
{
    [SerializeField] private Bullet bulletPref;
    [SerializeField] private Transform spawnRocketPoint;
    [SerializeField] private Bullet currentBullet;
    public override int MaxBulletInClip { get => 1; }
    public override bool Shoot()
    {
        if (remainingTimeToReadyShoot <= 0 && remainingBulletInClip > 0 && currentBullet != null)
        {
            remainingTimeToReadyShoot = 1 - (float)weaponData.GetRateOfFireValue(level) / 1000;
            currentBullet.transform.parent = null;
            currentBullet.Init(Camera.main.transform.position + Camera.main.transform.forward * 15, damagableLayer, weaponData.GetDamageValue(level));
            remainingBulletInClip--;
            return true;
        }
        return false;
    }
    public override void Reload()
    {
        currentBullet = Instantiate(bulletPref, spawnRocketPoint);
        currentBullet.transform.localPosition = Vector3.zero;
        currentBullet.transform.Rotate(0, 180, 0);
        remainingBulletInClip = 1;
    }
    public override void Reset()
    {
        remainingBulletInClip = 1;
    }
}
