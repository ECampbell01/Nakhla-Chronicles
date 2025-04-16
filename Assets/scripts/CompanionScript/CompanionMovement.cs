// contributions: Chance Daigle
// date: 3/23/25

using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
    public Animator playerAnimator; // drag the player’s Animator in the Inspector
    private Animator foxAnimator;
    public Transform playerTransform;
    [SerializeField] public float followSpeed = 1f;
    public Vector3 followOffset = new Vector3(-0.5f, -0.5f, 0);

    void Start()
    {
        foxAnimator = GetComponent<Animator>();

    }

    void Update()
    {
        // Copy movement parameters
        foxAnimator.SetFloat("MoveX", playerAnimator.GetFloat("MoveX"));
        foxAnimator.SetFloat("MoveY", playerAnimator.GetFloat("MoveY"));
        foxAnimator.SetFloat("LastMoveX", playerAnimator.GetFloat("LastMoveX"));
        foxAnimator.SetFloat("LastMoveY", playerAnimator.GetFloat("LastMoveY"));
        foxAnimator.SetFloat("MoveMagnitude", playerAnimator.GetFloat("MoveMagnitude"));

        // Determine if player is moving (based on MoveMagnitude)
        bool isMoving = playerAnimator.GetFloat("MoveMagnitude") > 0f;
        foxAnimator.SetBool("IsMoving", isMoving);


    }

    void LateUpdate()
    {
        Vector3 targetPos = playerTransform.position + followOffset;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
