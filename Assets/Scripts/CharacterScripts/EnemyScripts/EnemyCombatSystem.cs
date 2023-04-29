using UnityEngine;

public abstract class EnemyCombatSystem : MonoBehaviour
{
    [SerializeField] protected float rangeAttack;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float damage;
    protected float currentTimeToReadyAttack = 0;
    protected bool isAttacking = false;
    protected bool mayAttack = true;
    public bool MayAttack => mayAttack;
    public float RangeAttack => rangeAttack;
    public bool IsAttacking => isAttacking;
    private void Update()
    {
        if (currentTimeToReadyAttack > 0)
        {
            currentTimeToReadyAttack -= Time.deltaTime;
            if (currentTimeToReadyAttack <0)
            {
                currentTimeToReadyAttack = 0;
            }
        }
    }
    public abstract bool Attack();
}
