using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 input;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInputs();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * movementSpeed;
    }

    void HandleInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();
    }
}

