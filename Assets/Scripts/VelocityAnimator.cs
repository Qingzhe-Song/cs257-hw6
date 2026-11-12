// VelocityAnimator.cs
// Maintainer: Qingzhe-Song
// Summary: Updates the Animator's Velocity parameter from the Rigidbody's movement speed.

using UnityEngine;

public class VelocityAnimator : MonoBehaviour
{
    private const string VelocityParameterName = "Velocity";

    private Rigidbody bodyRigidbody;
    private Animator animator;

    private void Awake()
    {
        bodyRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat(VelocityParameterName, bodyRigidbody.velocity.magnitude);
    }
}
