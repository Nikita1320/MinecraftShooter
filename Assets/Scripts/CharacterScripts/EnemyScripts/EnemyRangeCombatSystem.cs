using UnityEngine;

public class EnemyRangeCombatSystem : EnemyCombatSystem
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform spawnBulletPoint;
    [SerializeField] private GameObject target;
    private EnemyBehaviour enemyBehaviour;
    private void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        target = enemyBehaviour.Target;
    }
    public override bool Attack()
    {
        if (currentTimeToReadyAttack == 0)
        {
            isAttacking = true;
            currentTimeToReadyAttack = attackSpeed;
            mayAttack = false;
            animator.SetTrigger("Attack");
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SendBullet()
    {
        isAttacking = false;
        mayAttack = true;
        var bullet = Instantiate(bulletPrefab, spawnBulletPoint);
        bullet.transform.localPosition = Vector3.zero;
        bullet.transform.parent = null;

        var direction = (transform.position - target.transform.position).normalized;
        bullet.Init(target.transform.position, playerLayer, damage);
    }
}
