using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMissingArm : MonoBehaviour, IDamagable
{
    private const int MAX_HEALTH = 100; 

    public int Health = MAX_HEALTH;

    public float TimeBetweenSwings = 2f;
    private float lastSwingTime = -999f;

    [SerializeField] private LayerMask checkPlayerLayerMask;

    [SerializeField] private Renderer[] renderers;
    private float flashTime;

    private bool seenPlayer = false;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        AllEnemies.Enemies.Add(gameObject);

        GameController.CrossedThreshold.AddListener(LosePlayer);
    }

    private void LosePlayer()
    {
        seenPlayer = false;
        Health = MAX_HEALTH;
    }

    private void Update()
    {
        FlashWhenHit();
        if (Health <= 0) return;
        CheckSeenPlayer();
        HuntPlayer();
    }

    private void FlashWhenHit()
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
    }

    private void CheckSeenPlayer()
    {
        bool huntingPlayer = seenPlayer || Health < MAX_HEALTH;
        if (huntingPlayer) return;

        if (Vector3.Distance(transform.position, PlayerController.Position) > 40f) return;

        //Don't check every frame
        if (Random.Range(0f, 100f) < 10f)
        {
            Ray ray = new Ray();
            ray.origin = transform.position + Vector3.up;
            ray.direction = PlayerController.Position - transform.position;

            float playerDistance = Vector3.Distance(PlayerController.Position, transform.position);
            if (Physics.Raycast(ray, playerDistance, checkPlayerLayerMask) == false)
                seenPlayer = true;
        }
    }

    private void HuntPlayer()
    {
        bool huntingPlayer = seenPlayer || Health < MAX_HEALTH;

        if (huntingPlayer)
            navMeshAgent.SetDestination(PlayerController.Position);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude / 10);

        if (Vector3.Distance(transform.position, PlayerController.Position) < 2 && huntingPlayer)
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
        if (Vector3.Distance(transform.position, PlayerController.Position) < 1.5 && Health > 0)
            HUDText.TakeDamage(15);      
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

        GameController.CrossedThreshold.RemoveListener(LosePlayer);

        while (dissolve < 1)
        {
            foreach (Renderer renderer in renderers)
                renderer.material.SetFloat("_DisolveAmount", dissolve);
            dissolve += Time.deltaTime * 2f;
            yield return new WaitForEndOfFrame();
        }

        AllEnemies.Enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
