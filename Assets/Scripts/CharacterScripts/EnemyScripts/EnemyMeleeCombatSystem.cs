using UnityEngine;

public class EnemyMeleeCombatSystem : EnemyCombatSystem
{
    [SerializeField] private float radiusAttack;
    public float RadiusAttack => radiusAttack;
    public override bool Attack()
    {
        //Debug.Log("TryAttack");
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
    public void GetDamage()
    {
        isAttacking = false;
        mayAttack = true;
        Vector3 pointSphere = transform.position + transform.forward * rangeAttack;
        Collider[] collided = Physics.OverlapSphere(pointSphere, radiusAttack, playerLayer);
        foreach (var col in collided)
        {
            col.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
