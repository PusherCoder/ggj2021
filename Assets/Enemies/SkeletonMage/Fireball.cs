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
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
            HUDText.TakeDamage(10);

        Destroy(gameObject);
    }
}
