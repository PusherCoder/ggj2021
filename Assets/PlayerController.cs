using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveVector.z++;
        if (Input.GetKey(KeyCode.S)) moveVector.z--;
        if (Input.GetKey(KeyCode.D)) moveVector.x++;
        if (Input.GetKey(KeyCode.A)) moveVector.x--;
        moveVector.Normalize();

        rigidBody.velocity = moveVector;
    }
}
