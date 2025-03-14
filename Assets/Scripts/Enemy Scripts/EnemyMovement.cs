// Contributors: Ethan Campbell
// Date Created: 2/9/2025

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float obstacleCheckRadius;

    [SerializeField]
    private float obstacleCheckDistance;

    [SerializeField]
    private LayerMask obstacleLayerMask;

    private Rigidbody2D rigidbody;
    private PlayerAwarenessController controller;
    private Vector2 targetDirection;
    private Animator animator;

    private bool isKnockedBack = false;
    private float knockbackTimer = 0f;
    private float knockbackDuration = 0.2f;
    private RaycastHit2D[] obstacleCollisions;
    private float cooldown;
    private Vector2 obstacleTargetDirection;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
        obstacleCollisions = new RaycastHit2D[10];
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
            HandleRandomDirectionChange();
            targetDirection = controller.directionToPlayer;
            HandleObstacles();
        }
        else
        {
            targetDirection = Vector2.zero;
        }
    }

    private void HandleRandomDirectionChange() 
    {
        cooldown -= Time.deltaTime;
        
        if(cooldown <= 0) 
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            targetDirection = rotation * targetDirection;
            cooldown = Random.Range(1f, 5f);
        }
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("InputX", targetDirection.x);
        animator.SetFloat("InputY", targetDirection.y);
        animator.SetBool("isMoving", targetDirection != Vector2.zero);
    }

    private void HandleObstacles() 
    {
        cooldown -= Time.deltaTime;
        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(obstacleLayerMask);

        int numberOfCollisions = Physics2D.CircleCast(
            transform.position,
            obstacleCheckRadius,
            transform.up,
            contactFilter,
            obstacleCollisions,
            obstacleCheckDistance);

        for (int i = 0; i < numberOfCollisions; i++) 
        {
            var obstacleCollision = obstacleCollisions[i];

            if (obstacleCollision.collider.gameObject == gameObject)
                continue;

            if (cooldown <= 0) 
            {
                obstacleTargetDirection = obstacleCollision.normal;
                cooldown = 0.5f;
            }

            var targetRotation = Quaternion.LookRotation(transform.forward, obstacleTargetDirection);
            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            targetDirection = rotation * Vector2.up;
            break;
        }
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