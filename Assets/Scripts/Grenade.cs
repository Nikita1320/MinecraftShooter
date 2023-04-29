using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] protected float maxDamage;
    [SerializeField] private float radiusDamage;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private float delayExplosion;
    [SerializeField] private LayerMask damageLayer;
    [SerializeField] private Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody;

    public void Activate()
    {
        StartCoroutine(DelayExplosionCororutine());
    }
    private IEnumerator DelayExplosionCororutine()
    {
        yield return new WaitForSeconds(delayExplosion);
        Explosion();
    }
    public void Explosion()
    {
        Collider[] detectedCols = Physics.OverlapSphere(transform.position, radiusDamage, damageLayer);
        foreach (var col in detectedCols)
        {
            col.GetComponent<EnemyHealth>().TakeDamage(maxDamage - (maxDamage * (Vector3.Distance(transform.position, col.transform.position) / radiusDamage)));
        }
        var particles = Instantiate(explosionParticle);
        particles.transform.position = transform.position;
        particles.transform.rotation = Quaternion.identity;
        explosionParticle.Play();
        Destroy(gameObject);
    }
}
