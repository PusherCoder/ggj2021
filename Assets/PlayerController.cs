using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10f;
    public float LookMult = 1f;
    public float JumpPower = 10f;
    public float Gravity = 15f;
    public float AirControl = 1.5f;
    public float AirSpeedBonus = 1.5f;
    public float GroundControl = 15f;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask notPlayerLayerMask;
    private Rigidbody rigidBody;

    private Vector3 moveVector = Vector3.zero;
    private float cameraPitch = 0f;
    private float cameraYaw = 0f;
    private float yVelocity = -1f;
    private bool isGrounded;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Look();
        Jump();
    }

    private void FixedUpdate()
    {
        Movement();
        isGrounded = IsGrounded();
    }

    private void Look()
    {
        cameraPitch -= Input.GetAxis("Mouse Y") * LookMult;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        cameraYaw += Input.GetAxis("Mouse X") * LookMult;

        cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, cameraYaw, 0f);
    }

    private void Movement()
    {
        Vector3 newVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) newVector.z++;
        if (Input.GetKey(KeyCode.S)) newVector.z--;
        if (Input.GetKey(KeyCode.D)) newVector.x++;
        if (Input.GetKey(KeyCode.A)) newVector.x--;
        if (isGrounded) newVector.Normalize();
        newVector = Quaternion.Euler(0f, cameraYaw, 0f) * newVector;

        if (isGrounded)
            moveVector = Vector3.Lerp(moveVector, newVector, Time.deltaTime * GroundControl);
        else
            moveVector = Vector3.Lerp(moveVector, newVector * AirSpeedBonus, Time.deltaTime * AirControl);

        rigidBody.velocity = (moveVector * Speed) + (Vector3.up * yVelocity);
    }

    private float lastJumpTime = -999f;
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            lastJumpTime = Time.time;

        float timeSinceLastJump = Time.time - lastJumpTime;
        bool pressedJump = timeSinceLastJump < .1f;

        if (pressedJump && isGrounded && yVelocity <= 0f)
            yVelocity = JumpPower;

        yVelocity -= Gravity * Time.deltaTime;
        if (yVelocity < -1 && IsGrounded()) yVelocity = -1f;
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * .1f), Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, .2f, notPlayerLayerMask))
            return true;

        return false;
    }
}
