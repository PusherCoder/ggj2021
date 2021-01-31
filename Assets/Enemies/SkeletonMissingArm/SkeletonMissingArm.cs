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

        if (Health <= 0) return; 

        navMeshAgent.SetDestination(PlayerController.Position);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude / 10);

        if (Vector3.Distance(transform.position, PlayerController.Position) < 2)
        {
            float timeSinceLastSwing = Time.time - lastSwingTime;
            if (timeSinceLastSwing >= TimeBetweenSwings)
            {
                lastSwingTime = Time.time;
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Attack");
                audioSource.Play();
                StartCoroutine(CheckIfDamagedPlayer());
            }
        }
    }

    private IEnumerator CheckIfDamagedPlayer()
    {
        yield return new WaitForSeconds(.33f);
        if (Vector3.Distance(transform.position, PlayerController.Position) < 1.5)
        {
            HUDText.Health -= 15;        
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
        navMeshAgent.speed = 1f;

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
