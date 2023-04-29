using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;

    [SerializeField] protected int remainingBulletInClip;
    [SerializeField] protected float remainingTimeToReadyShoot;

    [SerializeField] protected float scatter = 0.01f;
    [SerializeField] protected LayerMask bulletCollisionLayer;
    [SerializeField] protected LayerMask damagableLayer;
    [SerializeField] protected ParticleSystem shootParticle;
    [SerializeField] protected int level;
    [SerializeField] protected ParticleSystem hitParticle;
    [SerializeField] protected float delayDestroingParticle;
    public int MaxLevel => 3;
    public WeaponData WeaponData { get => weaponData; }
    public int SizeClip => weaponData.GetClipValue(level);
    public int RemainingBulletInClip { get => remainingBulletInClip; }
    public virtual int MaxBulletInClip { get => weaponData.GetClipValue(level); }

    private void Update()
    {
        if (remainingTimeToReadyShoot > 0)
        {
            remainingTimeToReadyShoot -= Time.deltaTime;
        }
    }
    public virtual bool Shoot()
    {
        if (remainingTimeToReadyShoot <= 0 && remainingBulletInClip > 0)
        {
            remainingTimeToReadyShoot = 1 - (float)weaponData.GetRateOfFireValue(level) / 1000;
            shootParticle.Play();
            RaycastHit hitInfo;
            //Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 directionBullet = new Vector3(Camera.main.transform.forward.x + Random.Range(-scatter, scatter),
                   Camera.main.transform.forward.y + Random.Range(-scatter, scatter),
                   Camera.main.transform.forward.z);
            Ray ray = new Ray(Camera.main.transform.position, directionBullet);
            if (Physics.Raycast(ray, out hitInfo, 100f, bulletCollisionLayer))
            {
                var hitObject = hitInfo.collider.gameObject;
                Debug.Log($"hit = {hitInfo.collider.gameObject.name}");
                var particle = Instantiate(hitParticle, null);
                particle.transform.position = hitInfo.point;

                if (hitObject != null)
                {
                    if (damagableLayer == ( damagableLayer | (1 << hitObject.gameObject.layer)))
                    {
                        Debug.Log("ItsDamageLayer");
                        hitObject.GetComponent<Health>().TakeDamage(weaponData.GetDamageValue(level), hitInfo.point);
                        Debug.Log("SENDDAMAGE");
                    }
                }
            }
            remainingBulletInClip--;
            return true;
        }
        return false;
    }
    public virtual void Reload()
    {
        remainingBulletInClip = weaponData.GetClipValue(level);
    }
    public void Initialize(int level, LayerMask enemyLayer)
    {
        this.level = level;
        damagableLayer = enemyLayer;
    }
    public virtual void Reset()
    {
        remainingBulletInClip = weaponData.GetClipValue(level);
    }
    public void OnEnable()
    {
        remainingTimeToReadyShoot = 0;
    }
}
