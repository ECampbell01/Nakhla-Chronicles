using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float lerpSpeed = 10;
    private Rigidbody2D rb;
    private Vector2 input;

    Animator anim;
    public Vector2 lastMoveDirection;

    public Transform aimPoint;
    bool isWalking = false;

    public bool canMove;

    void Start()
    {
        // Get the components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "TutorialNew")
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    void Update()
    {
        if (!canMove) return;
        Move();
        HandleInputs();
        Animate();
    }

    void Move()
    {
        // Move the player
        input = input * StatsManager.Instance.agility;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, input, Time.deltaTime * lerpSpeed);

        if (isWalking)
        {
            Vector3 vector3 = Vector3.left * input.x + Vector3.down * input.y;
            aimPoint.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        else
        {
            // Make sure the aim point stays in the direction the player last moved when not moving
            Vector3 vector3 = Vector3.left * lastMoveDirection.x + Vector3.down * lastMoveDirection.y;
            aimPoint.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }

    void HandleInputs()
    {
        // Store the last move direction when the play stops moving.

        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveDirection == Vector2.zero && input != Vector2.zero)
        {
            isWalking = false;
            lastMoveDirection = input;
            Vector3 vector3 = Vector3.left * lastMoveDirection.x + Vector3.down * lastMoveDirection.y;
            aimPoint.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        else if (moveDirection != Vector2.zero)
        {
            isWalking = true;
        }

        // Get the input from the player

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();
    }

    void Animate()
    {
        // Update the animator parameters

        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    public void StopMovement()
    {
        input = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
    }
}

