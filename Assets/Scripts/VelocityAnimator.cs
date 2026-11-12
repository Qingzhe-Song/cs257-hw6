using UnityEngine;

public class VelocityAnimator : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }
}