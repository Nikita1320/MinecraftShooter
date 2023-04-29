using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    [SerializeField] private ParticleSystem particlePref;
    [SerializeField] private float delayDestroingParticle;
    [SerializeField] private Ragdoll ragdoll;
    private EnemyMovementController enemyMovementController;
    private NavMeshAgent navMeshAgent;
    private EnemyBehaviour enemyBehaviour;
    private Collider collider;
    private Animator animator;

    private void Start()
    {
        healthPoint = maxHealthPoint;
        enemyMovementController = GetComponent<EnemyMovementController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }
    public override void Die()
    {
        diedEvent?.Invoke();
        enemyMovementController.enabled = false;
        navMeshAgent.enabled = false;
        collider.enabled = false;
        enemyBehaviour.enabled = false;
        animator.enabled = false;
        ragdoll.Activate();
    }
    public override void TakeDamage(float damage)
    {
        healthPoint -= damage;
        changedHP?.Invoke();
        if (healthPoint <= 0)
        {
            Die();
        }
    }
    public override void TakeDamage(float damage, Vector3 hitPosition)
    {
        var particle = Instantiate(particlePref, transform);
        particle.gameObject.transform.position = hitPosition;
        TakeDamage(damage);
        Debug.Log("TakeDamage");
    }
}
