using UnityEngine;
using UnityEngine.AI;

public class SkeletonMissingArm : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        navMeshAgent.SetDestination(PlayerController.Position);
        animator.SetFloat("Speed", Mathf.Clamp01(navMeshAgent.velocity.magnitude));
    }
}
