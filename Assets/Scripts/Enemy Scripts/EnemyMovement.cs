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

    private bool isKnockedBack = false;
    private float knockbackTimer = 0f;
    private float knockbackDuration = 0.2f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;
            }
        }
        else
        {
            UpdateTargetDirection();
            SetVelocity();
            UpdateAnimation();
        }
    }

    public void ApplyKnockback(Vector2 force)
    {
        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.AddForce(force, ForceMode2D.Impulse);
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
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
        if (targetDirection == Vector2.zero)
        {
            rigidbody.linearVelocity = Vector2.zero;
        }
        else
        {
            rigidbody.linearVelocity = targetDirection * speed;
        }
    }
}