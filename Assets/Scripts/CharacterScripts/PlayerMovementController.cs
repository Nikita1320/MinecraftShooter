using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float checkSphereRadius = 0.4f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Animator animator;
    public bool IsGrounded => Physics.CheckSphere(groundCheckPoint.position, checkSphereRadius, groundMask);
    private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (IsGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    public void Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
        Vector3 moveDirection = transform.right * direction.x + transform.forward * direction.y;

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
    public void Jump()
    {
        if (IsGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * gravity * -2f);
        }
    }
    public void Initialize(int speed)
    {
        moveSpeed = speed;
    }
}
