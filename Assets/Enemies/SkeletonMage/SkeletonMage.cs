using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMage : MonoBehaviour, IDamagable
{
    private const int MAX_HEALTH = 200;

    public int Health = MAX_HEALTH;

    [SerializeField] private LayerMask checkPlayerLayerMask;

    [SerializeField] private Fireball fireballPrefab;
    [SerializeField] private Transform spawnFireballTransform;
    public float TimeBetweenShots = 2f;
    private float lastShotTime = -999f;

    [SerializeField] private Renderer[] renderers;
    private float flashTime;

    private bool seenPlayer = false;

    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        AllEnemies.Enemies.Add(gameObject);
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

        if (huntingPlayer == false) return;

        navMeshAgent.SetDestination(PlayerController.Position);

        float timeSinceLastShot = Time.time - lastShotTime;
        if (timeSinceLastShot >= TimeBetweenShots)
        {
            Fireball fireball = Instantiate(fireballPrefab);
            fireball.transform.position = spawnFireballTransform.position;
            fireball.MoveVector = (PlayerController.Position + Vector3.up ) - spawnFireballTransform.position;
            fireball.MoveVector.Normalize();
            fireball.MoveVector *= 25f;

            lastShotTime = Time.time;
            audioSource.Play();
            StartCoroutine(CheckIfDamagedPlayer());
        }
    }

    private IEnumerator CheckIfDamagedPlayer()
    {
        yield return new WaitForSeconds(.33f);
        if (Vector3.Distance(transform.position, PlayerController.Position) < 1.5 && Health > 0)
            HUDText.TakeDamage(10);
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

        AllEnemies.Enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
