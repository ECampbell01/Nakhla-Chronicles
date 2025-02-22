// Contributors: Ethan Campbell
// Date Created: 2/9/2025

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rigidbody;
    private PlayerAwarenessController controller;
    private Vector2 targetDirection;
    private Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
        UpdateAnimation();
    }

    private void UpdateTargetDirection() 
    {
        if (controller.playerInRange)
        {
            targetDirection = controller.directionToPlayer;
        }
        else 
        {
            targetDirection = Vector2.zero;
        }
    }
    private void UpdateAnimation()
    {
        animator.SetFloat("InputX", targetDirection.x);
        animator.SetFloat("InputY", targetDirection.y);
        animator.SetBool("isMoving", targetDirection != Vector2.zero);
    }

    private void SetVelocity()
    {
        if(targetDirection == Vector2.zero)
        {
            rigidbody.linearVelocity = Vector2.zero;
        }
        else
        {
            rigidbody.linearVelocity = targetDirection * speed;
        }
    }
}
