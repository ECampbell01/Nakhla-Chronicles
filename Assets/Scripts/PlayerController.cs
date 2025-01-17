using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 input;

    Animator anim;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;


    void Start()
    {
        // Get the components

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInputs();
        Animate();

        // Flip the player sprite if the player is moving in the opposite direction on the x-axis

        if (input.x > 0 && !facingLeft || input.x < 0 && facingLeft)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // Move the player

        rb.linearVelocity = input * movementSpeed;
    }

    void HandleInputs()
    {
        // Store the last move direction when the play stops moving.

        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveDirection == Vector2.zero && input != Vector2.zero)
        {
            lastMoveDirection = input;
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

    void Flip()
    {
        // Flip the player sprite on the x-axis

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }
}

