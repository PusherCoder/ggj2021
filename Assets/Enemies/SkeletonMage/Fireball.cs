using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector3 MoveVector;

    private void Update()
    {
        transform.position += MoveVector * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
