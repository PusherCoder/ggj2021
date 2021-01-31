using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector3 MoveVector;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = MoveVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SkeletonMage>() != null) return;

        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (playerController != null)
            HUDText.TakeDamage(10);
        if (damagable != null)
            damagable.TakeDamage(50);

        Destroy(gameObject);
    }
}
