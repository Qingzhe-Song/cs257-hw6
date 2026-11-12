// NavMeshAnimator.cs
// Maintainer: Qingzhe-Song
// Summary: Updates the Animator's Velocity parameter from the NavMeshAgent's movement speed.

using UnityEngine;
using UnityEngine.AI;

public class NavMeshAnimator : MonoBehaviour
{
    private const string VelocityParameterName = "Velocity";

    private NavMeshAgent navigationAgent;
    private Animator animator;

    private void Awake()
    {
        navigationAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat(VelocityParameterName, navigationAgent.velocity.magnitude);
    }
}
