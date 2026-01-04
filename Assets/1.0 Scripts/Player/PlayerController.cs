using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References (Drag & Drop)")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private CapsuleCollider capsule; 
    [SerializeField] private LayerMask groundLayer;     

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float groundCheckExtra = 0.1f;

    private bool isGround;
    private bool wasGround;

    private void Update()
    {
        isGround = IsGrounded();

        if (!wasGround && isGround && animator != null)
        {
            animator.SetBool("IsJumping", false);
        }

        if (SwipeInput.SwipeUpDetected && isGround)
        {
            Jump();
        }

        if (SwipeInput.SwipeDownDetected && isGround)
        {
            Roll();
        }

        wasGround = isGround;
    }

    private void Roll()
    {
        if (animator == null) return;

        animator.SetTrigger("Roll");
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        if (animator != null)
            animator.SetBool("IsJumping", true);
    }

    private bool IsGrounded()
    {
        float radius = capsule.radius * 0.9f;
        Vector3 origin = capsule.bounds.center;
        float distance = capsule.bounds.extents.y + groundCheckExtra;

        bool hit = Physics.SphereCast(
            origin,
            radius,
            Vector3.down,
            out _,
            distance,
            groundLayer
        );
        return hit;
    }
}