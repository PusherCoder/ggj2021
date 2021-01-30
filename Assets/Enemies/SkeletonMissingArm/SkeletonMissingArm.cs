using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMissingArm : MonoBehaviour, IDamagable
{
    public int Health = 100;

    public float TimeBetweenSwings = 2f;
    private float lastSwingTime = -999f;

    [SerializeField] private Renderer[] renderers;
    private float flashTime;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        navMeshAgent.SetDestination(PlayerController.Position);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        if (Vector3.Distance(transform.position, PlayerController.Position) < 2)
        {
            float timeSinceLastSwing = Time.time - lastSwingTime;
            if (timeSinceLastSwing >= TimeBetweenSwings)
            {
                lastSwingTime = Time.time;
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Attack");
                audioSource.Play();
            }
        }

        flashTime -= Time.deltaTime;
        if (flashTime > 0)
        {
            foreach (Renderer renderer in renderers)
                renderer.material.SetFloat("_WhiteAmount", 1f);
        }
        else
        {

            foreach (Renderer renderer in renderers)
                renderer.material.SetFloat("_WhiteAmount", 0f);
        }
    }

    public void TakeDamage(int amount)
    {
        if (Health <= 0) return;

        flashTime = .1f;
        Health -= amount;
        if (Health <= 0)
            StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        float dissolve = 0f;
        flashTime = 999f;

        while (dissolve < 1)
        {
            foreach (Renderer renderer in renderers)
                renderer.material.SetFloat("_DisolveAmount", dissolve);
            dissolve += Time.deltaTime * 2f;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
