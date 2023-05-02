using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private float radiusDamage;
    [SerializeField] private ParticleSystem flyingParticle;
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
    protected override void OnTriggerEnter(Collider other)
    {
        if (isInit)
        {
            if (collisionLayer == (collisionLayer | (1 << other.gameObject.layer)))
            {
                GetDamage();
                Destroy(gameObject);
            }
        }
    }
    public override void Init(Vector3 direction, LayerMask damagableLayer, float damage)
    {
        this.direction = direction - transform.position;
        this.damage = damage;
        layer = damagableLayer;
        remainingTimeLife = lifeTime;
        flyingParticle.gameObject.SetActive(true);
        flyingParticle.Play();
        isInit = true;
    }
    private void GetDamage()
    {
        Collider[] detectedCols = Physics.OverlapSphere(transform.position, radiusDamage, layer);
        foreach (var col in detectedCols)
        {
            col.GetComponent<EnemyHealth>().TakeDamage(damage - (damage * (Vector3.Distance(transform.position, col.transform.position) / radiusDamage)));
        }
        var particles = Instantiate(explosionParticle);
        particles.transform.position = transform.position;
        particles.transform.rotation = Quaternion.identity;
        particles.Play();
        Destroy(gameObject);
    }
}
