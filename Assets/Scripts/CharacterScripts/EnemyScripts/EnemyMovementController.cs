using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private Coroutine rotateToCoroutine;
    private Coroutine rotateCororutine;
    public bool IsMoving => navMeshAgent.hasPath;

    private void Start()
    {
        navMeshAgent.updateRotation = false;
    }
    private void Update()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", IsMoving);
        }
        if (navMeshAgent.hasPath)
        {
            RotateTo();
        }
    }
    public void RotateTo()
    {
        //Debug.Log("RotateTo");
        var direction = navMeshAgent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
    }
    public void RotateToTarget(GameObject target)
    {
        if (rotateCororutine != null)
        {
            StopCoroutine(rotateCororutine);
        }
        rotateCororutine = StartCoroutine(RotateToTargetCororutine(target));
    }
    private IEnumerator RotateToTargetCororutine(GameObject target)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (navMeshAgent.hasPath)
            {
                break;
            }
            else
            {
                //Debug.Log("COROUTINE");
                var direction = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
            }
        }
    }
    public void Move(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
        /*if (rotateToCoroutine != null)
        {
            StopCoroutine(rotateToCoroutine);
        }
        rotateToCoroutine = StartCoroutine(RotateTo());*/

        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed.Value * Time.deltaTime, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDirection);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction - transform.position), rotateSpeed);
    }
    public void StopMoving()
    {
        navMeshAgent.ResetPath();
    }
    private void OnDisable()
    {
        if (rotateCororutine != null)
        {
            StopCoroutine(rotateCororutine);
        }
    }
}
