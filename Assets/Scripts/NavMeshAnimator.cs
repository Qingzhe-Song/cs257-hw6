using UnityEngine;
using UnityEngine.AI;

public class NavMeshAnimator : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Animator animator;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Velocity", navMesh.velocity.magnitude);
    }
}