using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMage : MonoBehaviour, IDamagable
{
    private const int MAX_HEALTH = 80;

    public int Health = MAX_HEALTH;

    [SerializeField] private LayerMask checkPlayerLayerMask;

    [SerializeField] private Transform visualsTransform;
    [SerializeField] private CapsuleCollider capsuleCollider;

    [SerializeField] private Fireball fireballPrefab;
    [SerializeField] private Transform spawnFireballTransform;
    public float TimeBetweenShots = 2f;
    private float lastShotTime = -999f;

    [SerializeField] private Renderer[] renderers;
    private float flashTime;

    private bool seenPlayer = false;

    private NavMeshAgent navMeshAgent;

    private float bobOffset;

    [SerializeField] private AudioSource attackAudio;
    [SerializeField] private AudioSource alertAudio;
    private bool beenAlerted = false;

    private void Awake()
    {
        bobOffset = Random.Range(0f, 999f);
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        Bob();
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

        if (huntingPlayer == false)
        {
            lastShotTime = Time.time;
            return;
        }

        if (beenAlerted == false)
        {
            beenAlerted = true;
            alertAudio.Play();
        }

        transform.LookAt(PlayerController.Position);
        transform.rotation = Quaternion.Euler(new Vector3(
            0f,
            transform.eulerAngles.y,
            0f));

        navMeshAgent.SetDestination(PlayerController.Position);

        float timeSinceLastShot = Time.time - lastShotTime;
        if (timeSinceLastShot >= TimeBetweenShots)
        {
            Fireball fireball = Instantiate(fireballPrefab);
            fireball.transform.position = spawnFireballTransform.position;
            fireball.MoveVector = (PlayerController.Position + Vector3.up ) - spawnFireballTransform.position;
            fireball.MoveVector.Normalize();
            fireball.MoveVector *= 35f;

            lastShotTime = Time.time;
            attackAudio.Play();
        }
    }

    private void Bob()
    {
        Vector3 position = new Vector3(0f, Mathf.Sin(Time.time * 2f + bobOffset) * .5f, 0f);
        visualsTransform.localPosition = position;
        capsuleCollider.center = position + (Vector3.up * 3f);
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
