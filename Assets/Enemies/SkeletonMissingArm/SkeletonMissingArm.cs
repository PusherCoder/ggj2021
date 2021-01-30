using UnityEngine;
using UnityEngine.AI;

public class SkeletonMissingArm : MonoBehaviour, IDamagable
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
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    public void DoDamage(int amount)
    {
    }
}
