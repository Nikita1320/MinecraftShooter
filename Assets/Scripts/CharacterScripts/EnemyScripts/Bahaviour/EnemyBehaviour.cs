using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyCombatSystem combatSystem;
    [SerializeField] private EnemyMovementController movementController;
    [SerializeField] private GameObject target;

    public GameObject Target { get => target;}

    private void Update()
    {
        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < combatSystem.RangeAttack && combatSystem.MayAttack)
            {
                movementController.StopMoving();
                movementController.RotateToTarget(target);
                combatSystem.Attack();
            }
            else
            {
                if (!combatSystem.IsAttacking)
                {
                    if (Vector3.Distance(target.transform.position, transform.position) > combatSystem.RangeAttack)
                    {
                        movementController.Move(target.transform.position);
                    }
                    else
                    {
                        movementController.StopMoving();
                    }
                }
            }
        }
    }
    public void SetTarget(GameObject gameObject)
    {
        target = gameObject;
    }
}
